using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [HideInInspector] public bool bKeepInfo = false;
    [SerializeField] GameObject goParticles;
    [SerializeField] float fMaxDistance;
    float fPlayerDistance;
    AudioSource asFire;
    bool bCrash = false;
    float asFireVolume;

    private void Start()
    {
        asFire = AudioManager.Instance.asFireLoop;
        asFireVolume = asFire.volume;
    }

    public void Crash()
    {
        GameManager.Instance.KeepInfo(this);
        goParticles.SetActive(false);
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        bCrash = true;
    }

    public void Reset()
    {
        if (!bKeepInfo)
        {
            goParticles.SetActive(true);
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }          
    }

    private void Update()
    {
        fPlayerDistance = Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position);
        if (fPlayerDistance < fMaxDistance)
        {
            asFire.volume = asFireVolume * ((fPlayerDistance * (-0.036f)) + 1.18f);
            if (!asFire.isPlaying && !bCrash)
            {
                AudioManager.Instance.PlayMechFx("Fire");
                
            }
            else if(bCrash)
            {
                asFire.Stop();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = false;

            if (GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable != null)
            {
                GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable.GetComponent<Collectable>().bCollecting = false;
            }

            GameManager.Instance.ScriptPlayer.BoolDeathDone();
            GameManager.Instance.Death1();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fMaxDistance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Godess : MonoBehaviour
{
    [SerializeField] GameObject goBallSlot;
    [SerializeField] GameObject goDestiny;
    [SerializeField] ParticleSystem[] ps;

    [HideInInspector] public bool bEvent = false;
    bool bStart = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bEvent)
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
            GameManager.Instance.ScriptPlayer.bInteracting = true;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

            GameManager.Instance.ScriptPlayer.CatchBall();
            goBallSlot.transform.position = GameManager.Instance.GoBall.transform.position;
            GameManager.Instance.GoBall.transform.parent = goBallSlot.transform;

            bEvent = false;
            bStart = true;
        }

        if(bStart)
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
            GameManager.Instance.ScriptPlayer.bInteracting = true;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

            goBallSlot.transform.position = Vector3.MoveTowards(goBallSlot.transform.position, goDestiny.transform.position, 0.2f);

            if (Vector3.Distance(goBallSlot.transform.position, goDestiny.transform.position) <= 0.1f)
            {
                Particles();
                StartCoroutine(EndEndEnd());
                bStart = false;
            }
        }
    }

    IEnumerator EndEndEnd()
    {
        yield return new WaitForSeconds(10);

        StartCoroutine(InGameCanvas.Instance.InTransition("Credits"));
    }
    

    public void Particles()
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Play();
        }
    }

}

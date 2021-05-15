using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneKey : MonoBehaviour
{
    [SerializeField] RuneDoor runeDoor;
    [SerializeField] float fSpeed;
    [SerializeField] float fDistance;
    [SerializeField] ParticleSystem psKey;

    [HideInInspector] public bool bFollowing;
    Vector3 v3Direction;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (bFollowing)
        {
            v3Direction = GameManager.Instance.GoPlayer.transform.position - transform.position;

            if (Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) >= fDistance)
            {
                transform.position += v3Direction * fSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            psKey.Play();
            AudioManager.Instance.PlayMechFx("RuneKey");
            bFollowing = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

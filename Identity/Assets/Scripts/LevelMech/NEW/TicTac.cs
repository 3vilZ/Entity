using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTac : MonoBehaviour
{
    [SerializeField] GameObject goSpikes;
    [SerializeField] Transform tSpikesTop;
    [SerializeField] Transform tSpikesBot;
    [SerializeField] float fPlayerDistance;
    [SerializeField] public float fCooldown;
    [SerializeField] float fSpeed;
    //[SerializeField] public bool bMidTerm;

    [HideInInspector] public bool bActivate;

    float fPlayerDistanceControl;
    float fCooldownControl;
    bool bTop;
    



    void Start()
    {
        goSpikes.transform.position = tSpikesTop.position;
        fCooldownControl = fCooldown;
        bTop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //fPlayerDistanceControl = Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position);

        //if (fPlayerDistanceControl <= fPlayerDistance)
        if(bActivate)
        {
            fCooldownControl -= Time.deltaTime;

            if(fCooldownControl <= 0)
            {
                if(bTop)
                {
                    goSpikes.transform.position = Vector3.MoveTowards(goSpikes.transform.position, tSpikesBot.position, fSpeed);
                    if(Vector3.Distance(goSpikes.transform.position, tSpikesBot.position) <= 0.05f)
                    {
                        fCooldownControl = fCooldown;
                        bTop = false;
                    }
                    
                }
                else
                {
                    goSpikes.transform.position = Vector3.MoveTowards(goSpikes.transform.position, tSpikesTop.position, fSpeed);
                    if (Vector3.Distance(goSpikes.transform.position, tSpikesTop.position) <= 0.05f)
                    {
                        fCooldownControl = fCooldown;
                        bTop = true;
                    }
                }
                
            }
        }
        else
        {
            goSpikes.transform.position = tSpikesTop.position;
            fCooldownControl = fCooldown;
            bTop = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fPlayerDistance);
    }
}

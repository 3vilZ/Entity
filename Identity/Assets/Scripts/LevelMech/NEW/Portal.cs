using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform tPortalA;
    [SerializeField] Transform tPortalB;
    [SerializeField] float fDistance;

    bool bPlayerA = false;
    bool bPlayerB = false;

    bool bBallA = false;
    bool bBallB = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //PLAYER
        if (Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, tPortalA.position) <= fDistance && !bPlayerB)
        {
            bPlayerA = true;
            GameManager.Instance.LookAheadSmoothing(true);
            GameManager.Instance.GoPlayer.transform.position = tPortalB.position;
        }

        if (Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, tPortalB.position) <= fDistance && !bPlayerA)
        {
            bPlayerB = true;
            GameManager.Instance.LookAheadSmoothing(true);
            GameManager.Instance.GoPlayer.transform.position = tPortalA.position;
        }

        if(bPlayerA)
        {
            if(Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, tPortalB.position) >= fDistance)
            {
                GameManager.Instance.LookAheadSmoothing(false);
                bPlayerA = false;
            }
        }

        if (bPlayerB)
        {
            if (Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, tPortalA.position) >= fDistance)
            {
                GameManager.Instance.LookAheadSmoothing(false);
                bPlayerB = false;
            }
        }

        //BALL
        if (Vector3.Distance(GameManager.Instance.GoBall.transform.position, tPortalA.position) <= fDistance && !bBallB && !GameManager.Instance.ScriptPlayer.bBallOn)
        {
            bBallA = true;
            GameManager.Instance.LookAheadSmoothing(true);
            GameManager.Instance.GoBall.transform.position = tPortalB.position;
        }

        if (Vector3.Distance(GameManager.Instance.GoBall.transform.position, tPortalB.position) <= fDistance && !bBallA && !GameManager.Instance.ScriptPlayer.bBallOn)
        {
            bBallB = true;
            GameManager.Instance.LookAheadSmoothing(true);
            GameManager.Instance.GoBall.transform.position = tPortalA.position;
        }

        if (bBallA)
        {
            if (Vector3.Distance(GameManager.Instance.GoBall.transform.position, tPortalB.position) >= fDistance)
            {
                GameManager.Instance.LookAheadSmoothing(false);
                bBallA = false;
            }
        }

        if (bBallB)
        {
            if (Vector3.Distance(GameManager.Instance.GoBall.transform.position, tPortalA.position) >= fDistance)
            {
                GameManager.Instance.LookAheadSmoothing(false);
                bBallB = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(tPortalA.position, fDistance);
        Gizmos.DrawWireSphere(tPortalB.position, fDistance);
    }
}

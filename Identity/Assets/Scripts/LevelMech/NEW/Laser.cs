using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float fPlayerDistance;
    [SerializeField] GameObject goBase;

    [SerializeField] LayerMask layerWithBall;
    [SerializeField] LayerMask layerWithNoBall;

    [Header("LineRenderer")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Texture[] textures;
    int animationStep;
    [SerializeField] float fps = 30;
    float fpsCounter;

    Vector2 v2LaserDirection;
    float fPlayerDistanceControl;
    bool bCrash = false;

    void Start()
    {
        v2LaserDirection = goBase.transform.right;
        //v2LaserDirection.Normalize();
        
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    public void Crash()
    {
        bCrash = true;
    }

    void Update()
    {
        fPlayerDistanceControl = Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position);

        if (fPlayerDistanceControl <= fPlayerDistance && !bCrash)
        {
            lineRenderer.enabled = true;
            RaycastHit2D hit = Physics2D.Raycast(goBase.transform.position, v2LaserDirection, Mathf.Infinity, layerWithNoBall);

            if(GameManager.Instance.GoBall.GetComponent<Ball>().currentPower == 2)
            {
                hit = Physics2D.Raycast(goBase.transform.position, v2LaserDirection, Mathf.Infinity, layerWithBall);
            }

            if (hit.collider != null)
            {
                lineRenderer.SetPosition(0, goBase.transform.position);
                lineRenderer.SetPosition(1, hit.point);
                
                //print(hit.collider.gameObject.name);

                /*
                
                */
                fpsCounter += Time.deltaTime;
                if (fpsCounter >= 1f / fps)
                {
                    animationStep++;
                    if (animationStep == textures.Length)
                    {
                        animationStep = 0;
                    }

                    lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

                    fpsCounter = 0f;
                }

                if (hit.collider.gameObject.tag == "Player")
                {
                    GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = false;

                    if (GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable != null)
                    {
                        GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().goCurrentCollectable.GetComponent<Collectable>().bCollecting = false;
                    }

                    GameManager.Instance.ScriptPlayer.BoolDeathDone();
                    GameManager.Instance.Death1();
                }
                if(hit.collider.gameObject.tag == "Ball")
                {
                    GameManager.Instance.ScriptPlayer.BubbleForceReload();
                }
            }
            
        }
        else
        {
            lineRenderer.enabled = false;
        }


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(goBase.transform.position, fPlayerDistance);
    }
}



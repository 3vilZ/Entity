using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInOut : MonoBehaviour
{
    [SerializeField] float fTimeToOut;
    [SerializeField] float fTimeToIn;
    [SerializeField] [Range(0, 1)] float alpha;

    WallJump scriptWallJump;
    Collider2D colGround;

    //Anim
    Color sprColor;

    float fTimeToOutControl = 0;
    float fTimeToIncontrol = 0;

    bool bOut = false;

    void Start()
    {
        scriptWallJump = GetComponent<WallJump>();
        colGround = GetComponent<Collider2D>();

        fTimeToOutControl = fTimeToOut;
        fTimeToIncontrol = fTimeToIn;

        //Anim
        sprColor = GetComponent<SpriteRenderer>().color;
    }

    
    void Update()
    {
        if(bOut)
        {
            fTimeToOutControl -= Time.deltaTime;

            if(fTimeToOutControl <= 0)
            {
                fTimeToIncontrol -= Time.deltaTime;

                colGround.enabled = false;
                scriptWallJump.colLeft.enabled = false;
                scriptWallJump.colRight.enabled = false;

                //Anim
                sprColor.a = alpha;
                GetComponent<SpriteRenderer>().color = sprColor;

                if (fTimeToIncontrol <= 0)
                {
                    colGround.enabled = true;
                    scriptWallJump.colLeft.enabled = true;
                    scriptWallJump.colRight.enabled = true;

                    //Anim
                    sprColor.a = 1f;
                    GetComponent<SpriteRenderer>().color = sprColor;

                    fTimeToOutControl = fTimeToOut;
                    fTimeToIncontrol = fTimeToIn;

                    bOut = false;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            bOut = true;
        }
    }
}

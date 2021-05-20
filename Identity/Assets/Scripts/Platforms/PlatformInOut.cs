using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInOut : MonoBehaviour
{
    //[SerializeField] SpriteRenderer sprModel;
    [SerializeField] float fTimeToOut;
    [SerializeField] float fTimeToIn;
    public MeshRenderer meshModel;
    public Material[] matNormal;
    public Material matOff;
    Material[] matOffArray;
    //Color colorIn;
    //[SerializeField] Color colorOut;

    WallJump scriptWallJump;
    Collider2D colGround;

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
        //colorIn = sprModel.GetComponent<SpriteRenderer>().color;

        matOffArray = new Material[3];
        for (int i = 0; i < 3; i++)
        {
            matOffArray[i] = matOff;
        }
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
                //sprModel.GetComponent<SpriteRenderer>().color = colorOut;
                meshModel.materials = matOffArray;

                

                if (fTimeToIncontrol <= 0)
                {
                    colGround.enabled = true;
                    scriptWallJump.colLeft.enabled = true;
                    scriptWallJump.colRight.enabled = true;

                    //Anim
                    //sprModel.GetComponent<SpriteRenderer>().color = colorIn;
                    meshModel.materials = matNormal;

                    fTimeToOutControl = fTimeToOut;
                    fTimeToIncontrol = fTimeToIn;

                    AudioManager.Instance.PlayMechFx("InOutOn");
                    bOut = false;
                }
            }
        }
    }

    IEnumerator SoundOff()
    {
        yield return new WaitForSeconds(fTimeToOut);
        AudioManager.Instance.PlayMechFx("InOutOff");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball" || other.gameObject.tag == "Bullet")
        {
            StartCoroutine(SoundOff());
            bOut = true;
        }
    }
}

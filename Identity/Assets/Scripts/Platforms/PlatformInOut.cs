using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInOut : MonoBehaviour
{
    //[SerializeField] SpriteRenderer sprModel;
    [SerializeField] float fTimeToOut;
    [SerializeField] float fTimeToIn;
    [SerializeField] GameObject goModel;
    MeshRenderer[] meshModel;
    public Material[] matNormal;
    public Material[] matOff;

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

        meshModel = new MeshRenderer[goModel.transform.childCount];

        for (int i = 0; i < meshModel.Length; i++)
        {
            meshModel[i] = goModel.transform.GetChild(i).GetComponent<MeshRenderer>();
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
                for (int i = 0; i < meshModel.Length; i++)
                {
                    meshModel[i].materials = matOff;
                }
                

                

                if (fTimeToIncontrol <= 0)
                {
                    colGround.enabled = true;
                    scriptWallJump.colLeft.enabled = true;
                    scriptWallJump.colRight.enabled = true;

                    //Anim
                    //sprModel.GetComponent<SpriteRenderer>().color = colorIn;
                    for (int i = 0; i < meshModel.Length; i++)
                    {
                        meshModel[i].materials = matNormal;
                    }

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

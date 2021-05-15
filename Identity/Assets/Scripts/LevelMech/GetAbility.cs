using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    [Tooltip("0 = Lanzar, 1 = Recoger, 2 = Dash")]
    [SerializeField] int iValue;
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;
    [SerializeField] GameObject goBallSlot;
    [SerializeField] GameObject goBallHalo;
    [SerializeField] GameObject goAnimPlace;
    [SerializeField] GameObject[] goParts;
    [SerializeField] ParticleSystem[] ps;
    [SerializeField] float fSoundDistance;

    Animator animAbility;
    bool bOnce = false;
    bool bStart = false;
    bool bEnd = false;

    AudioSource asAbility;
    float asAbilityVolume;
    bool bAbilityStart = false;
    bool bAbilityEnd = false;

    private void Start()
    {
        goY.SetActive(false);
        animAbility = GetComponent<Animator>();

        for (int i = 0; i < goParts.Length; i++)
        {
            goParts[i].SetActive(false);
        }

        goParts[iValue].SetActive(true);

        if(!GameManager.Instance.BSkill[0])
        {
            GameManager.Instance.GoBall.transform.position = goBallSlot.transform.position;
            GameManager.Instance.GoBall.transform.parent = goBallSlot.transform;
            goBallHalo.SetActive(true);
        }
        else
        {
            goParts[0].SetActive(true);
            goBallHalo.SetActive(false);
        }

        asAbility = AudioManager.Instance.asAbility;
    }

    private void Update()
    {
        if (bAbilityStart)
        {
            if(!asAbility.isPlaying && !bAbilityEnd)
            {
                asAbility.Play();
            }
        }
        else
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fSoundDistance)
            {
                //Audio
                asAbilityVolume = asAbility.volume;
                asAbility.volume = 0;
                asAbility.Play();
                StartCoroutine(AudioManager.Instance.FadeSound(asAbility, 2, asAbilityVolume));
                AudioManager.Instance.FadeOut();
                //Audio
                bAbilityStart = true;
            }
        }

        if(!bOnce)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
            {
                goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
                    GameManager.Instance.ScriptPlayer.bInteracting = true;
                    GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

                    if (GameManager.Instance.BSkill[0])
                    {
                        GameManager.Instance.ScriptPlayer.CatchBall();
                        goBallSlot.transform.position = GameManager.Instance.GoBall.transform.position;
                        GameManager.Instance.GoBall.transform.parent = goBallSlot.transform;
                    }

                    

                    bStart = true;
                    goY.SetActive(false);
                    bOnce = true;
                }
            }
            else
            {
                goY.SetActive(false);
            }
        }
        

        if(bStart)
        {
            goBallSlot.transform.position = Vector3.MoveTowards(goBallSlot.transform.position, goAnimPlace.transform.position, 0.04f);

            if(Vector3.Distance(goBallSlot.transform.position, goAnimPlace.transform.position) <= 0.1f)
            {
                if(iValue > 0)
                {
                    GameManager.Instance.GoBall.GetComponent<Animator>().SetTrigger("Shoot");
                }
                
                animAbility.SetTrigger("Start");
                bStart = false;
            }
        }

        if(bEnd)
        {
            goBallSlot.transform.position = Vector3.MoveTowards(goBallSlot.transform.position, GameManager.Instance.GoPlayer.transform.position, 0.04f);

            if (Vector3.Distance(goBallSlot.transform.position, GameManager.Instance.GoPlayer.transform.position) <= 0.1f)
            {
                GameManager.Instance.ScriptPlayer.CatchBall();                
                GameManager.Instance.GetSkill(iValue);
                GameManager.Instance.ICoreStart = iValue +1;

                InGameCanvas.Instance.CoreFadeIn(iValue);

                //Audio
                StartCoroutine(AudioManager.Instance.FadeSound(asAbility, 2, 0, asAbilityVolume));
                AudioManager.Instance.FadeIn();
                bAbilityEnd = true;
                //Audio


                /*
                //
                CanvasManager.Instance.goCore[iValue].SetActive(true);

                switch (iValue)
                {
                    case 0:
                        CanvasManager.Instance.txtLeft.text = "Pulsa (X) para lanzar la esencia del Dios.\nTe verás impulsado en la dirección opuesta al lanzamiento.";
                        CanvasManager.Instance.txtRight.text = "Pulsa (RT) para recuperar la esencia del Dios.\nPodrás lanzar la esencia las veces que desees, hasta que sea recuperada.";
                        break;
                    case 1:
                        CanvasManager.Instance.txtCenter.text = "Pulsa (RT) para recuperar la esencia del Dios.\nAhora, al recuperar la esencia recibirás un impulso vertical.";
                        break;
                    case 2:
                        //
                        break;
                    default:
                        print("PdroP");
                        break;
                }
                CanvasManager.Instance.goCoreDisplay.SetActive(true);
                CanvasManager.Instance.canvasAnim.SetTrigger("Core");
                //
                */

                bEnd = false;
            }
        }
    }


    public void Particles()
    {
        for (int i = 0; i < ps.Length; i++)
        {
            ps[i].Play();
        }

        /*
        if (!ps[7].IsAlive())
        {
            GameManager.Instance.ScriptPlayer.bAbility = false;
            bStart = false;
        }
        */
    }

    public void EndAnim()
    {
        bEnd = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fSoundDistance);
    }
}

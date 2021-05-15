using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacStarter : MonoBehaviour
{
    public float fTimer;
    public TicTac[] tictacNormal;
    public TicTac[] tictacMidTerm;
    
    float fTimerControl;
    bool bOn = false;
    bool bOff = false;

    private void Start()
    {
        //fTimer = tictacNormal[0].fCooldown * 0.5f;
        fTimerControl = fTimer;
        tictacNormal[0].bIsAudio = true;
    }

    private void Update()
    {
        if(bOn)
        {
            fTimerControl -= Time.deltaTime;
            if (fTimerControl <= 0)
            {
                for (int i = 0; i < tictacMidTerm.Length; i++)
                {
                    tictacMidTerm[i].bActivate = true;
                }

                fTimerControl = fTimer;
                bOn = false;
            }
        }

        if(bOff)
        {
            for (int i = 0; i < tictacMidTerm.Length; i++)
            {
                tictacMidTerm[i].bActivate = false;
            }
            bOff = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.ScriptPlayer.bDead && !GameManager.Instance.ScriptPlayer.bDeadDone)
        {
            if (other.gameObject.tag == "Player")
            {
                bOn = true;
                for (int i = 0; i < tictacNormal.Length; i++)
                {
                    tictacNormal[i].bActivate = true;
                }
            }
        }
            
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(!GameManager.Instance.ScriptPlayer.bDead && !GameManager.Instance.ScriptPlayer.bDeadDone)
        {
            if (other.gameObject.tag == "Player")
            {
                bOff = true;
                for (int i = 0; i < tictacNormal.Length; i++)
                {
                    tictacNormal[i].bActivate = false;
                }
            }
        }
        
    }
}

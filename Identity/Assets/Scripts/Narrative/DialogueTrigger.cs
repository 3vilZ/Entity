using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;
    [SerializeField] int iEvent;
    [Space(20)]
    public SerializeDialogue sDialogue;

    bool bOnce = false;


    private void Update()
    {
        if (!bOnce)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
            {
                goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.ScriptPlayer.bInteracting = true;
                    GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

                    /*
                    if (GameManager.Instance.BSkill[0])
                    {
                        GameManager.Instance.ScriptPlayer.CatchBall();
                    }
                    */

                    goY.SetActive(false);
                    TriggerDialogue();
                    bOnce = true;
                }
            }
            else
            {
                goY.SetActive(false);
            }
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(sDialogue);
        FindObjectOfType<DialogueManager>().SelectEvent(iEvent);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
    }
}

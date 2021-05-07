using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;
    [SerializeField] int iEvent;
    [Space(20)]
    public NarrativeDialogue sDialogue;

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
                    GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
                    GameManager.Instance.ScriptPlayer.bInteracting = true;
                    GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);

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
        InGameCanvas.Instance.NarrativeFadeIn(sDialogue);
        InGameCanvas.Instance.SelectEvent(iEvent);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
    }
}

[System.Serializable]
public class NarrativeDialogue
{
    public string strName;

    [TextArea(3, 10)]
    public string[] strSentence;

}

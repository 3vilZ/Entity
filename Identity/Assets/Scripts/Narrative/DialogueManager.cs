using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    TextMeshProUGUI txtName;
    TextMeshProUGUI txtSentence;
    GameObject goNarrativeDisplay;

    private Queue<string> queue = new Queue<string>();

    bool bStart = false;
    bool bTyping = false;
    bool bOnce = false;
    string strCurrentSentence;
    int iEvent;
    public GameObject[] goEvent;

    void Start()
    {
        txtName = CanvasManager.Instance.txtName;
        txtSentence = CanvasManager.Instance.txtSentence;
        goNarrativeDisplay = CanvasManager.Instance.goNarrativeDisplay;

        //goNarrativeDisplay.SetActive(false);

        txtName.text = "";
        txtSentence.text = "";
    }

    private void Update()
    {
        if(bStart)
        {
            if(bTyping)
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Shoot") || Input.GetButtonDown("Y"))
                {
                    StopCoroutine("DisplaySentence");
                    txtSentence.text = strCurrentSentence;
                    CanvasManager.Instance.canvasAnim.SetTrigger("On");
                    bTyping = false;
                }
            }
            else
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Shoot") || Input.GetButtonDown("Y"))
                {
                    StopCoroutine("DisplaySentence");
                    txtSentence.text = "";
                    strCurrentSentence = "";
                    if(!bOnce)
                    {
                        bOnce = true;
                    }
                    else
                    {
                        CanvasManager.Instance.canvasAnim.SetTrigger("Off");
                    }
                    
                    DisplayNextSentence();
                }
            }
        }
    }

    public void StartDialogue(SerializeDialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.strName);

        goNarrativeDisplay.SetActive(true);
        txtName.text = dialogue.strName;

        queue.Clear();

        foreach (string sentence in dialogue.strSentence)
        {
            queue.Enqueue(sentence);
        }

        bStart = true;
        bOnce = false;
    }

    public IEnumerator DisplaySentence()
    {
        foreach (char c in strCurrentSentence)
        {
            txtSentence.text += c;

            yield return new WaitForSeconds(0.1f);
        }
        bTyping = false;
        CanvasManager.Instance.canvasAnim.SetTrigger("On");
    }

    public void DisplayNextSentence()
    {
        if (queue.Count == 0)
        {
            EndDialogue();
            return;
        }
        strCurrentSentence = queue.Dequeue().ToString();
        StartCoroutine("DisplaySentence");
        bTyping = true;
    }

    void EndDialogue()
    {
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        goNarrativeDisplay.SetActive(false);
        bStart = false;

        if(goEvent.Length > 0)
        {
            if(iEvent >= 0)
            {
                goEvent[iEvent].GetComponent<Animator>().SetTrigger("Start");
            }
            
        }
        
        Debug.Log("End of conversation. ");
    }

    public void SelectEvent(int dialogueTriggerEvent)
    {
        iEvent = dialogueTriggerEvent;
    }

    /*
    void StartEvent()
    {
        switch (iEvent)
        {
            case 0:
                print("No Event");
                break;
            case 1:
                //
                break;
            case 2:
                //
                break;
            default:
                print("PdroP");
                break;
        }
    }
    */
}

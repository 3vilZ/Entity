using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentence;
    public GameObject goNarrativeDisplay;

    private Queue<string> queue = new Queue<string>();

    bool bStart = false;

    void Start()
    {
        goNarrativeDisplay.SetActive(false);
    }

    private void Update()
    {
        if(bStart)
        {
            if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Shoot") || Input.GetButtonDown("Y"))
            {
                DisplayNextSentence();
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
        DisplayNextSentence();
        //StartCoroutine("DisplaySentence");
    }

    /*
    public IEnumerator DisplaySentence()
    {
        string sentence = queue.ToString();

        foreach (char c in sentence)
        {
            txtSentence.text += c;

            yield return new WaitForSeconds(0.25f);
        }

        DisplayNextSentence();
    }
    */

    public void DisplayNextSentence()
    {
        if (queue.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = queue.Dequeue();
        txtSentence.text = sentence;
    }


    void EndDialogue()
    {
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        goNarrativeDisplay.SetActive(false);
        bStart = false;
        Debug.Log("End of conversation. ");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InGameCanvas : MonoBehaviour
{
    public static InGameCanvas Instance;

    [Header("PauseMenu")]
    public GameObject goPanel;
    public GameObject goScreenParent;
    public GameObject[] goScreens;
    public GameObject[] goFirstSelect;
    public InGameSettingsSelection[] sSelection;
    int iScreen;
    Animator animator;
    Animator panelAnimator;
    bool bTransition = false;
    bool bPause = false;

    [Header("CoreDisplay")]
    public GameObject goCoreDisplay;
    public GameObject[] imgCore;
    public TextMeshProUGUI txtTitle;
    public TextMeshProUGUI txtLeft;
    public TextMeshProUGUI txtRight;
    public GameObject btnCore;
    bool bButtonCore = false;
    Animator coreAnimator;

    [Header("NarrativeDisplay")]
    public GameObject goNarrativeDisplay;
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentence;
    public GameObject btnNarrative;
    public GameObject[] goEvent;
    private Queue<string> queue = new Queue<string>();
    bool bStart = false;
    bool bTyping = false;
    string strCurrentSentence;
    int iEvent;
    Animator narrativeAnimator;
    NarrativeDialogue dialogue;

    [Header("Collectable")]
    public GameObject goCollectableDisplay;
    public TextMeshProUGUI txtCollectable;
    Animator collectableAnimator;

    [Header("Transition")]
    public GameObject goTransition;
    bool bOnce = false;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Pause
        goScreenParent.SetActive(true);
        animator = GetComponent<Animator>();
        panelAnimator = goPanel.GetComponent<Animator>();

        for (int i = 0; i < goScreens.Length; i++)
        {
            goScreens[i].SetActive(false);
        }
        goPanel.SetActive(false);

        sSelection[0].goSelection.GetComponent<Slider>().value = GameManager.Instance.fVolumeMultiplier;

        //Core
        goCoreDisplay.SetActive(false);
        for (int i = 0; i < imgCore.Length; i++)
        {
            imgCore[i].SetActive(false);
        }
        coreAnimator = goCoreDisplay.GetComponent<Animator>();

        //Narrative
        goNarrativeDisplay.SetActive(false);
        narrativeAnimator = goNarrativeDisplay.GetComponent<Animator>();
        txtName.text = "";
        txtSentence.text = "";

        //Collectable
        goCollectableDisplay.SetActive(false);
        collectableAnimator = goCollectableDisplay.GetComponent<Animator>();

        goTransition.SetActive(true);
        StartCoroutine(OutTransition());
    }

    IEnumerator OutTransition()
    {
        GameManager.Instance.ScriptPlayer.bInteracting = true;
        yield return new WaitForSeconds(2.2f);
        goTransition.SetActive(false);
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        StopCoroutine(OutTransition());
    }

    public IEnumerator InTransition(string strNextLevel)
    {
        GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
        GameManager.Instance.ScriptPlayer.bInteracting = true;
        goTransition.SetActive(true);
        goTransition.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.LoadLevel(strNextLevel);
        StopCoroutine(InTransition(strNextLevel));
    }

    #region PauseMenu

    public void FadeOut(int i)
    {
        bTransition = true;
        EventSystem.current.SetSelectedGameObject(null);
        animator.SetTrigger("FadeOut");
        iScreen = i;
    }

    public void FadeIn()
    {
        for (int i = 0; i < goScreens.Length; i++)
        {
            goScreens[i].SetActive(false);
        }

        goScreens[iScreen].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(goFirstSelect[iScreen]);
        animator.SetTrigger("FadeIn");
        bTransition = false;
    }



    public void PauseGame()
    {
        if(bPause)
        {
            animator.SetTrigger("Resume");
            panelAnimator.SetTrigger("FadeOut");
        }
        else
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerAnim>().Interact();
            GameManager.Instance.ScriptPlayer.bInteracting = true;
            GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameManager.Instance.GoPlayer.GetComponent<Rigidbody2D>().velocity.y);
            iScreen = 0;
            FadeIn();
            goPanel.SetActive(true);
            panelAnimator.SetTrigger("FadeIn");
        }

        bPause = !bPause;
    }

    public void ResumeGame()
    {
        for (int i = 0; i < goScreens.Length; i++)
        {
            goScreens[i].SetActive(false);
        }
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        goPanel.SetActive(false);
    }

    public void MainMenu()
    {
        if(!bOnce)
        {
            GameManager.Instance.SaveGame();
            StartCoroutine(InTransition("MainMenu"));
            bOnce = true;
        }
    }

    public void VolumeMultiplier(Slider slider)
    {
        GameManager.Instance.fVolumeMultiplier = slider.value;
    }

    #endregion

    #region Core

    public void CoreFadeIn(int value)
    {
        goCoreDisplay.SetActive(true);
        imgCore[value].SetActive(true);

        switch (value)
        {
            case 0:
                txtTitle.text = "You have obtained the essence of Kamu's heart";
                txtLeft.text = "Press (X) to release the sphere\n\nOnce the sphere is released, you can launch it all the times you want";
                txtRight.text = "Press (RT) to call the sphere back\n\nOnce the sphere is collected, you can not throw it again until you hit the ground";
                break;
            case 1:
                txtTitle.text = "You have obtained the left fragment of Kamu's heart";
                txtLeft.text = "Collecting the sphere using (RT) now will give you a small vertical boost";
                txtRight.text = "This ability will allow you to reach higher or further grounds";
                break;
            case 2:
                txtTitle.text = "You have obtained the right fragment of Kamu's heart";
                txtLeft.text = "Press (LT) to dash towards the sphere.\n\nYou can only use this ability once the sphere has been released";
                txtRight.text = "If you reach the sphere using this ability, you will collect it and you will be able to release it again";
                break;
            default:
                print("PdroP");
                break;
        }

        goCoreDisplay.GetComponent<Animator>().SetTrigger("FadeIn");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnCore);
    }
    public void CoreFadeOut()
    {
        if(!bButtonCore)
        {
            goCoreDisplay.GetComponent<Animator>().SetTrigger("FadeOut");
            bButtonCore = true;
        }
    }

    public void CoreOff()
    {
        goCoreDisplay.SetActive(false);
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        bButtonCore = false;
    }
    #endregion

    #region Narrative

    public void NarrativeFadeIn(NarrativeDialogue nD)
    {
        dialogue = nD;
        txtName.text = dialogue.strName;
        goNarrativeDisplay.SetActive(true);
        narrativeAnimator.SetTrigger("FadeIn");
    }

    public void ButtonNextSentence()
    {
        if (bStart)
        {
            if (bTyping)
            {
                StopCoroutine("DisplaySentence");
                txtSentence.text = strCurrentSentence;
                bTyping = false;
            }
            else
            {
                StopCoroutine("DisplaySentence");
                txtSentence.text = "";
                strCurrentSentence = "";
                //narrativeAnimator.SetTrigger("Base");
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnNarrative);

        queue.Clear();

        foreach (string sentence in dialogue.strSentence)
        {
            queue.Enqueue(sentence);
        }

        bStart = true;
        DisplayNextSentence();
    }

    public IEnumerator DisplaySentence()
    {
        foreach (char c in strCurrentSentence)
        {
            txtSentence.text += c;

            yield return new WaitForSeconds(0.06f);
        }
        narrativeAnimator.SetTrigger("Idle");
        bTyping = false;
    }

    public void DisplayNextSentence()
    {
        if (queue.Count == 0)
        {
            EndDialogue();
            return;
        }
        narrativeAnimator.SetTrigger("Base");
        strCurrentSentence = queue.Dequeue().ToString();
        StartCoroutine("DisplaySentence");
        bTyping = true;
    }

    void EndDialogue()
    {
        narrativeAnimator.SetTrigger("FadeOut");
        bStart = false;

        if (goEvent.Length > 0)
        {
            if (iEvent >= 0)
            {
                goEvent[iEvent].GetComponent<Animator>().SetTrigger("Start");
                AudioManager.Instance.PlayMechFx("ButtonOpen");
            }

        }
    }

    public void NarrativeOff()
    {
        dialogue = null;
        goNarrativeDisplay.SetActive(false);
        GameManager.Instance.ScriptPlayer.bInteracting = false;
        
    }

    public void SelectEvent(int dialogueTriggerEvent)
    {
        iEvent = dialogueTriggerEvent;
    }

    #endregion

    public void CollectableFadeIn()
    {
        goCollectableDisplay.SetActive(true);
        txtCollectable.text = GameManager.Instance.ICollectables.ToString();
        collectableAnimator.SetTrigger("FadeIn");
    }

    public void CollectableFadeOut()
    {
        txtCollectable.text = GameManager.Instance.ICollectables.ToString();
        collectableAnimator.SetTrigger("FadeOut");
    }

    public void CollectableOff()
    {
        goCollectableDisplay.SetActive(false);
    }


    void Update()
    {
        if(bPause)
        {
            if (EventSystem.current.currentSelectedGameObject == sSelection[0].goSelection)
            {
                sSelection[0].imgSelection.SetActive(true);
            }
            else
            {
                sSelection[0].imgSelection.SetActive(false);
            }

            if (Input.GetButtonDown("Cancel") && iScreen != 0)
            {
                FadeOut(0);
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }

            if (!bTransition)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(goFirstSelect[iScreen]);
                }
            }
        }

        if (Input.GetButtonDown("Pause"))
        {
            PauseGame();
        }


    }

    [System.Serializable]
    public class InGameSettingsSelection
    {
        public GameObject goSelection;
        public GameObject imgSelection;
    }

}

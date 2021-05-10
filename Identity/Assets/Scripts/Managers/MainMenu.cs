using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject[] goScreens;
    public GameObject[] goFirstSelect;
    public SettingsSelection[] sSelection;
    public GameObject goTransition;

    int iScreen;
    Animator animator;
    bool bTransition = false;
    bool bOnce = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        goTransition.SetActive(true);
        StartCoroutine(OutTransition());

        GameManager.Instance.LoadGame();
        sSelection[0].goSelection.GetComponent<Slider>().value = GameManager.Instance.fVolumeMultiplier;
    }

    IEnumerator OutTransition()
    {
        yield return new WaitForSeconds(2.1f);
        goTransition.SetActive(false);
        iScreen = 0;
        FadeIn();
        StopCoroutine(OutTransition());
    }

    IEnumerator InTransition(int iAction)
    {
        goTransition.SetActive(true);
        goTransition.GetComponent<Animator>().SetTrigger("In");
        yield return new WaitForSeconds(2.5f);
        switch(iAction)
        {
            case 0:
                GameManager.Instance.QuitGame();
                break;
            case 1:
                GameManager.Instance.NewGame();
                break;
            case 2:
                GameManager.Instance.ContinueGame();
                break;
            default:
                print("PdroP");
                break;
        }
        StopCoroutine(InTransition(3));
    }

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

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == sSelection[0].goSelection)
        {
            sSelection[0].imgSelection.SetActive(true);
        }
        else
        {
            sSelection[0].imgSelection.SetActive(false);
        }

        if(Input.GetButtonDown("Cancel") && iScreen != 0)
        {
            FadeOut(0);
        }
        else if(Input.GetButtonDown("Cancel"))
        {
            FadeOut(3);
        }

        if(!bTransition)
        {
            if(EventSystem.current.currentSelectedGameObject == null)
            {
                EventSystem.current.SetSelectedGameObject(goFirstSelect[iScreen]);
            }
        }
    }

    public void VolumeMultiplier(Slider slider)
    {
        GameManager.Instance.fVolumeMultiplier = slider.value;
    }

    public void QuitGame()
    {
        if(!bOnce)
        {
            StartCoroutine(InTransition(0));
            bOnce = true;
        }
        
        //GameManager.Instance.QuitGame();
    }

    public void NewGame()
    {
        if (!bOnce)
        {
            StartCoroutine(InTransition(1));
            bOnce = true;
        }
        //GameManager.Instance.NewGame();
    }

    public void ContinueGame()
    {
        if (!bOnce)
        {
            StartCoroutine(InTransition(2));
            bOnce = true;
        }
        //GameManager.Instance.ContinueGame();
    }

    [System.Serializable]
    public class SettingsSelection
    {
        public GameObject goSelection;
        public GameObject imgSelection;
    }



}

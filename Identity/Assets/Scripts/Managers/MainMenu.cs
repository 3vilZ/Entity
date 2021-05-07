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

    int iScreen;
    Animator animator;
    bool bTransition = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        iScreen = 0;
        FadeIn();

        GameManager.Instance.LoadGame();
        sSelection[0].goSelection.GetComponent<Slider>().value = GameManager.Instance.fVolumeMultiplier;
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
        //EventSystem.current.SetSelectedGameObject(null);
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

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void VolumeMultiplier(Slider slider)
    {
        GameManager.Instance.fVolumeMultiplier = slider.value;
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void NewGame()
    {
        GameManager.Instance.NewGame();
    }

    [System.Serializable]
    public class SettingsSelection
    {
        public GameObject goSelection;
        public GameObject imgSelection;
    }



}

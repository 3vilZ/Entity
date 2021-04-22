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



    void Start()
    {
        animator = GetComponent<Animator>();
        iScreen = 0;

        FadeIn();
    }

    public void FadeOut(int i)
    {
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

        if (EventSystem.current.currentSelectedGameObject == sSelection[1].goSelection)
        {
            sSelection[1].imgSelection.SetActive(true);
        }
        else
        {
            sSelection[1].imgSelection.SetActive(false);
        }
    }

    [System.Serializable]
    public class SettingsSelection
    {
        public GameObject goSelection;
        public GameObject imgSelection;
    }



}

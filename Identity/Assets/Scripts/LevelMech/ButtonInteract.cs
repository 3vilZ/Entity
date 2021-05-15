using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField] GameObject goBlock;
    [SerializeField] float fSpeed;
    [SerializeField] Transform tDestiny;
    [SerializeField] ParticleSystem psBlock;
    [SerializeField] ButtonPoint[] arrayButtons;


    Vector3 v3Initial;
    bool[] bTotalButtons;
    bool bActivated = false;
    bool bReverse = false;
    [HideInInspector] public bool bKeepInfo = false;
    

    void Start()
    {
        v3Initial = goBlock.transform.position;
        

        bTotalButtons = new bool[arrayButtons.Length];

        for (int i = 0; i < bTotalButtons.Length; i++)
        {
            bTotalButtons[i] = false;
        }
    }

    public void SetButton()
    {
        for (int i = 0; i < bTotalButtons.Length; i++)
        {
            if(bTotalButtons[i] == false)
            {
                bTotalButtons[i] = true;
                GameManager.Instance.KeepInfo(this);
                psBlock.Play();
                CheckAllTrue();
                return;
            }
        }
    }

    public void Reset()
    {
        if(!bKeepInfo)
        {
            bActivated = false;
            bReverse = true;

            for (int i = 0; i < arrayButtons.Length; i++)
            {
                bTotalButtons[i] = false;
                arrayButtons[i].GetComponent<Animator>().SetTrigger("Reverse");
                arrayButtons[i].psClick.Play();
                arrayButtons[i].GetComponent<Collider2D>().enabled = true;
            }

            AudioManager.Instance.PlayMechFx("ButtonReset");
            AudioManager.Instance.PlayMechFx("ButtonOpen");
        }     
    }

    private void CheckAllTrue()
    {
        for (int i = 0; i < bTotalButtons.Length; i++)
        {
            if(bTotalButtons[i] == false)
            {
                bActivated = false;
                return;
            }
        }
        bActivated = true;
        AudioManager.Instance.PlayMechFx("ButtonOpen");
        return;
    }

    void Update()
    {

        if(bActivated)
        {
            goBlock.transform.position = Vector3.MoveTowards(goBlock.transform.position, tDestiny.position, fSpeed * Time.deltaTime);

            if(Vector3.Distance(goBlock.transform.position, tDestiny.position) <= 0.01)
            {
                GameManager.Instance.CheckIfLobby(gameObject);
                bActivated = false;
            }
        }

        if(bReverse)
        {
            goBlock.transform.position = Vector3.MoveTowards(goBlock.transform.position, v3Initial, fSpeed * Time.deltaTime);

            if (Vector3.Distance(goBlock.transform.position, v3Initial) <= 0.01)
            {
                bReverse = false;
            }
        }
    }
}

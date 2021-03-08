using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField] GameObject goBlock;
    [SerializeField] float fSpeed;
    [SerializeField] Transform tDestiny;

    public bool[] bTotalButtons;
    bool bActivated = false;
    

    void Start()
    {
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
                CheckAllTrue();
                return;
            }
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
        return;
    }

    void Update()
    {

        if(bActivated)
        {
            goBlock.transform.position = Vector3.MoveTowards(goBlock.transform.position, tDestiny.position, fSpeed * Time.deltaTime);

            if(Vector3.Distance(goBlock.transform.position, tDestiny.position) <= 0.01)
            {
                bActivated = false;
            }
        }
    }
}

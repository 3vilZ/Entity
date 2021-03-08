using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTrue : MonoBehaviour
{
    public Second second;
    [SerializeField] GameObject[] goClock;

    void Update()
    {
        if (second.timerControl > second.fSectionTime * 2)
        {
            goClock[0].SetActive(true);
            goClock[1].SetActive(true);
            goClock[2].SetActive(true);
        }
        else if (second.timerControl > second.fSectionTime)
        {
            goClock[0].SetActive(true);
            goClock[1].SetActive(true);
            goClock[2].SetActive(false);
        }
        else if (second.timerControl > 0)
        {
            goClock[0].SetActive(true);
            goClock[1].SetActive(false);
            goClock[2].SetActive(false);
        }
        else
        {
            goClock[0].SetActive(false);
            goClock[1].SetActive(false);
            goClock[2].SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (second.timerControl > second.fSectionTime)
            {
                second.timerControl = second.fSectionTime * 3;
            }
            else if (second.timerControl <= second.fSectionTime && second.timerControl > 0)
            {
                second.timerControl = second.fSectionTime * 2;
            }
            else
            {
                second.timerControl = second.fSectionTime;
            }
        }
    }
}

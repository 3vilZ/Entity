using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fountain : MonoBehaviour
{
    public int power;
    public GameObject[] colours;

    void Start()
    {
        if (power == 0)
        {
            colours[0].SetActive(true);
        }
        else if (power== 1)
        {
            colours[1].SetActive(true);
        }
        else if (power == 2)
        {
            colours[2].SetActive(true);
        }
    }

    
    
}

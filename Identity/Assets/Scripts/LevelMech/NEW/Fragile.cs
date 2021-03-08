using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragile : MonoBehaviour
{


    public void Destroy()
    {
        Destroy(gameObject);
    }
}

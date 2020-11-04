using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationMovementPltf : MonoBehaviour
{

    [SerializeField]MovingPlatform MPcmp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        transform.Find("B").transform.localScale = new Vector3(1, 0.3f, 1);
        MPcmp.ActivatePltf();
    }

    
}

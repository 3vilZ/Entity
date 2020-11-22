using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    [SerializeField] Collider2D colButton;
    [SerializeField] GameObject goBlock;
    [SerializeField] float fSpeed;
    [SerializeField] Transform tDestiny;

    bool bActivated = false;

    void Start()
    {
        
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            bActivated = true;
            colButton.enabled = false;
        }
    }
}

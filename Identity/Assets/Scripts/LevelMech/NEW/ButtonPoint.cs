using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPoint : MonoBehaviour
{
    public ButtonInteract buttonInteract;
    [SerializeField] ParticleSystem psClick;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            GetComponent<Animator>().SetTrigger("Click");
            psClick.Play();
            buttonInteract.SetButton();
            GetComponent<Collider2D>().enabled = false;
            /*
            bActivated = true;
            colButton.enabled = false;
            */
        }
    }
}

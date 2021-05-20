using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPoint : MonoBehaviour
{
    public ButtonInteract buttonInteract;
    [SerializeField] public ParticleSystem psClick;
    public MeshRenderer meshClick;
    public Material matNormal;
    public Material matPressed;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            GetComponent<Animator>().SetTrigger("Click");
            psClick.Play();
            buttonInteract.SetButton();
            GetComponent<Collider2D>().enabled = false;
            meshClick.material = matPressed;

            AudioManager.Instance.PlayMechFx("ButtonClick");

            /*
            bActivated = true;
            colButton.enabled = false;
            */
        }
    }
}

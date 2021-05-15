using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePortionBubbles : MonoBehaviour
{
    GameObject[] goChilds;

    void Start()
    {
        goChilds = new GameObject[transform.childCount];

        for (int i = 0; i < goChilds.Length; i++)
        {
            goChilds[i] = transform.GetChild(i).gameObject;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            AudioManager.Instance.PlayMechFx("HidePortion");
            for (int i = 0; i < goChilds.Length; i++)
            {
                goChilds[i].GetComponent<Animator>().SetTrigger("FadeOut");
            }
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}

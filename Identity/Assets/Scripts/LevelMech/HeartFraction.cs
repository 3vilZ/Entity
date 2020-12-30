using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartFraction : MonoBehaviour
{
    [SerializeField] int iValue;
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;

    private void Start()
    {
        goY.SetActive(false);
    }

    private void Update()
    {
        if(Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
        {
            goY.SetActive(true);

            if (Input.GetButtonDown("Y"))
            {
                GameManager.Instance.GetSkill(iValue);
            }
        }
        else
        {
            goY.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fDistance);
    }
}

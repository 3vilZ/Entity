using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCell : MonoBehaviour
{
    [SerializeField] float fReaparition;
    [SerializeField] AllPowers currentPower;
    [SerializeField] GameObject[] goPowers;

    float fReaparitionControl;
    bool bTaken = false;
    bool bOnce = false;
    Transform t1;
    Transform t2;

    void Start()
    {
        fReaparitionControl = fReaparition;

        for (int i = 0; i < goPowers.Length; i++)
        {
            goPowers[i].SetActive(false);
        }
        goPowers[(int)currentPower].SetActive(true);
    }

    
    void Update()
    {
        

        if(bTaken)
        {
            fReaparitionControl -= Time.deltaTime;

            /*
            if ((int)currentPower == 3)
            {
                if (Vector3.Distance(transform.position, GameManager.Instance.GoBall.transform.position) >= 1)
                {
                    if (!bOnce)
                    {
                        t1.position = GameManager.Instance.GoBall.transform.position;
                        bOnce = true;
                    }
                }
            }

            if (fReaparitionControl <= 2)
            {

            }
            */
            if(fReaparitionControl <= 0)
            {
                goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color = new Color(goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.r,
                goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.g,
                goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.b, 1f);

                GetComponent<Collider2D>().enabled = true;
                fReaparitionControl = fReaparition;
                bTaken = false;
            }

            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ball")
        {
            /*
            switch ((int)currentPower)
            {
                case 0:
                    print("0");
                    break;
                case 1:
                    print("1");
                    break;
                case 2:
                    print("2");
                    break;
                case 3:
                    print("3");
                    break;
                default:
                    print("PowerBug");
                    break;
            }
            */

            GameManager.Instance.GoBall.GetComponent<Ball>().GetPowerID((int)currentPower);

            goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color = new Color(goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.r,
            goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.g,
            goPowers[(int)currentPower].GetComponent<SpriteRenderer>().color.b, 0.2f);

            GetComponent<Collider2D>().enabled = false;

            AudioManager.Instance.PlayMechFx("PowerCell");

            bTaken = true;
            
        }
    }

    public enum AllPowers
    {
        Destroy, Double, Bubble, Iron
    }

    private void OnDrawGizmos()
    {
        if((int)currentPower == 0)
        {
            Gizmos.color = Color.red;
        }
        else if((int)currentPower == 1)
        {
            Gizmos.color = Color.yellow;
        }
        else if ((int)currentPower == 2)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.gray;
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
    }


}

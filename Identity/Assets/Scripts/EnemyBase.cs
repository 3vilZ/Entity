using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    [SerializeField] int iMaxHealth;
    [SerializeField] bool bMove;
    /*
    [SerializeField] GameObject goModel;
    [SerializeField] float fSpeed;
    [SerializeField] Transform tPointA;
    [SerializeField] Transform tPointB;
    */
    
    int iCurrentHealth;
    Transform tDestiny;

    void Start()
    {
        iCurrentHealth = iMaxHealth;
        //tDestiny = tPointA;
        if (!bMove)
        {
            GetComponent<Animator>().enabled = false;
        }
    }


    void Update()
    {
        

        /*
        if(bMove)
        {
            Vector3 v3Direction = tDestiny.position - goModel.transform.position;
            v3Direction.Normalize();
            goModel.transform.position += v3Direction * fSpeed * Time.deltaTime;

            Vector3 v3Distance = tDestiny.position - goModel.transform.position;
            
            if (v3Distance.x <= 0.2)
            {
                if(tDestiny == tPointA)
                {
                    tDestiny = tPointB;
                }
                else
                {
                    tDestiny = tPointA;
                }
            }
        }
        */
    }

    public void GetDamage(int fDamage)
    {
        iCurrentHealth -= fDamage;

        if (iCurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}

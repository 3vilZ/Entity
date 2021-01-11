﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float fBulletSpeed;
    [SerializeField] float fBulletCd;
    [SerializeField] float fPlayerDistance;
    [SerializeField] Transform tAttackPos;
    [SerializeField] GameObject goBullet;
    [SerializeField] GameObject goCarcaj;
    Vector3 v3BulletDirection;
    float fPlayerDistanceControl;
    public float fbulletCdControl = 0;
    //GameObject[] goArray = new GameObject[30];
    public List<GameObject> goBulletList = new List<GameObject>();
    public List<GameObject> goCarcajList = new List<GameObject>();

    void Start()
    {
        v3BulletDirection = tAttackPos.position - transform.position;
        v3BulletDirection.Normalize();
        
        //goBullet.GetComponent<ProjectileBullet>().v3Direction = v3BulletDirection;
        //goBullet.GetComponent<ProjectileBullet>().fSpeed = fBulletSpeed;
    }

    void Update()
    {
        fPlayerDistanceControl = Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position);

        foreach (GameObject go in goBulletList)
        {
            go.transform.position += v3BulletDirection * fBulletSpeed * Time.deltaTime;
        }

        if (fPlayerDistanceControl <= fPlayerDistance)
        {
            fbulletCdControl -= Time.deltaTime;

            if (fbulletCdControl <= 0)
            {
                if(goCarcajList.Count != 0)
                {
                    goCarcajList[0].SetActive(true);
                    goBulletList.Add(goCarcajList[0]);
                    goCarcajList.Remove(goCarcajList[0]);
                    fbulletCdControl = fBulletCd;
                }
                else
                {
                    goBulletList.Add(Instantiate(goBullet, tAttackPos.position, tAttackPos.rotation, goCarcaj.transform));
                    fbulletCdControl = fBulletCd;
                }
            }
        }
        else
        {
            fbulletCdControl = 0;
        }
    }

    public void RemoveGameObject(GameObject go)
    {
        goBulletList.Remove(go);
        go.SetActive(false);
        go.transform.position = goCarcaj.transform.position;
        goCarcajList.Add(go);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fPlayerDistance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public float fBulletSpeed;
    [SerializeField] float fBulletCd;
    [SerializeField] Transform tAttackPos;
    [SerializeField] GameObject goBullet;
    [SerializeField] GameObject goCarcaj;
    Vector3 v3BulletDirection;
    float fPlayerDistance;
    float fbulletCdControl = 0;


    void Start()
    {
        
        v3BulletDirection = tAttackPos.position - transform.position;
        v3BulletDirection.Normalize();
        goBullet.GetComponent<ProjectileBullet>().v3Direction = v3BulletDirection;
        goBullet.GetComponent<ProjectileBullet>().fSpeed = fBulletSpeed;
    }


    void Update()
    {
        fPlayerDistance = Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position);

        if(fPlayerDistance <= 30)
        {
            fbulletCdControl -= Time.deltaTime;

            if(fbulletCdControl <= 0)
            {
                Instantiate(goBullet, tAttackPos.position, tAttackPos.rotation, goCarcaj.transform);
                fbulletCdControl = fBulletCd;
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    public Projectile projectile;

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Destroy(gameObject);
        projectile.RemoveGameObject(gameObject);
        //AudioManager.Instance.PlayMechFx("BulletDestroy");
    }
}

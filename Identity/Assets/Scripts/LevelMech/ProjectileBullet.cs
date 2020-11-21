using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    [HideInInspector] public float fSpeed;
    [HideInInspector] public Vector3 v3Direction;

    void Update()
    {
        transform.position += v3Direction * fSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}

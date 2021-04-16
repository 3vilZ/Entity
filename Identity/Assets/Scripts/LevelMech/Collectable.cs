﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Color colorBase1, colorBase2;
    [SerializeField] Color colorCollecting1, colorCollecting2;
    [SerializeField] ParticleSystem ps;
    [SerializeField] float fSpeed;
    [SerializeField] float fDistance;

    [Tooltip("Tres números: mundo + nivel + #coleccionable del nivel. ej: 311")]
    public int collectableID;

    Vector3 v3StartPos;
    Vector3 v3Direction;
    [HideInInspector] public bool bCollecting = false;
    [HideInInspector] public bool bRDY1 = false;
    [HideInInspector] public bool bRDY2 = false;
    bool b1Once = false;
    bool b2Once = false;

    ParticleSystem.MainModule psMain;
    ParticleSystem.RotationOverLifetimeModule psRot;

    private void Start()
    {
        GameManager.Instance.CheckCollectable(collectableID, this);

        v3StartPos = transform.position;
        psMain = ps.main;
        psRot = ps.rotationOverLifetime;

    }

    private void Update()
    {
        if (bCollecting)
        {
            v3Direction = GameManager.Instance.GoPlayer.transform.position - transform.position;

            if(Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) >= fDistance)
            {
                transform.position += v3Direction * fSpeed * Time.deltaTime;
            }
        }
        else
        {           
            psMain.startColor = new ParticleSystem.MinMaxGradient(colorBase1, colorBase2);

            v3Direction = v3StartPos - transform.position;

            if (Vector3.Distance(v3StartPos, transform.position) >= 0.1f)
            {
                transform.position += v3Direction * fSpeed * Time.deltaTime;
            }

            bRDY1 = false;
            bRDY2 = false;
            b1Once = false;
            b2Once = false;
        }

        if(bRDY1)
        {
            if(Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
            {
                bRDY2 = true;
            }
        }

        if (bRDY2)
        {
            if(!b1Once)
            {
                CanvasManager.Instance.goCollectableDisplay.SetActive(true);
                CanvasManager.Instance.txtCollectable.text = GameManager.Instance.ICollectables.ToString();
                CanvasManager.Instance.canvasAnim.SetTrigger("Collectable1");
                b1Once = true;
            }

            psMain.startSize = new ParticleSystem.MinMaxCurve(2, 5);
            psRot.zMultiplier = 18f;
            psMain.loop = false;

            if (!ps.IsAlive())
            {
                GameManager.Instance.GetCollectable(collectableID);
                GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = false;

                if (!b2Once)
                {
                    CanvasManager.Instance.txtCollectable.text = GameManager.Instance.ICollectables.ToString();
                    CanvasManager.Instance.canvasAnim.SetTrigger("Collectable2");
                    b2Once = true;
                }

                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            bCollecting = true;

            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().SetCollectable(gameObject);
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = true;

            psMain.startColor = new ParticleSystem.MinMaxGradient(colorCollecting1, colorCollecting2);
        }
    }

    /*
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = colorBase;
    }

    private void Update()
    {
        if (GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting == false)
        {
            GetComponent<SpriteRenderer>().color = colorBase;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ball")
        {
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().SetCollectable(gameObject);
            GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting = true;
            GetComponent<SpriteRenderer>().color = colorCollecting;
        }
    }

    GameManager.Instance.GoPlayer.GetComponent<PlayerControllerV3>().bCollecting == true
    */
}

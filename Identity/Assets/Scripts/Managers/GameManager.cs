﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject goPlayer;
    GameObject goBall;
    GameObject goVirtualCamera;
    bool[] bSkill = new bool[3];
    int iCollectables;
    Vector2 tCurrentCheckPointPos;
    PlayerControllerV3 scriptPlayer;
    Vector2 tDeathPos;
    

    public bool[] BSkill { get => bSkill; set => bSkill = value; }
    public Vector2 TCurrentCheckPointPos { get => tCurrentCheckPointPos; set => tCurrentCheckPointPos = value; }
    public GameObject GoPlayer { get => goPlayer; set => goPlayer = value; }
    public GameObject GoBall { get => goBall; set => goBall = value; }
    public GameObject GoVirtualCamera { get => goVirtualCamera; set => goVirtualCamera = value; }
    public int ICollectables { get => iCollectables; set => iCollectables = value; }
    public PlayerControllerV3 ScriptPlayer { get => scriptPlayer; set => scriptPlayer = value; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        goPlayer = GameObject.FindGameObjectWithTag("Player");
        goBall = GameObject.FindGameObjectWithTag("Ball");
        goVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");

        scriptPlayer = goPlayer.GetComponent<PlayerControllerV3>();

        tCurrentCheckPointPos = goPlayer.transform.position;

        for (int i = 0; i < bSkill.Length; i++)
        {
            bSkill[i] = false;
        }
    }

    private void Start()
    {
        goBall.SetActive(false);
    }

    public void GetSkill(int value)
    {
        bSkill[value] = true;

        if(bSkill[0])
        {
            goBall.SetActive(true);
            scriptPlayer.CatchBall();

            scriptPlayer.lineRenderer = goBall.GetComponent<LineRenderer>();
        }
    }

    public void GetCollectable()
    {
        iCollectables++;
    }

    public void SetCheckPoint(Vector2 newPosition)
    {
        tCurrentCheckPointPos = newPosition;
    }

    public void Death1()
    {
        goPlayer.GetComponent<Animator>().SetTrigger("Death");
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        tDeathPos = goPlayer.transform.position;
    }
    public void Death2()
    {
        //Vector3 delta = tCurrentCheckPointPos - tDeathPos;
        //goVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().OnTargetObjectWarped(goVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow, delta);
        goVirtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        goVirtualCamera.transform.position = tCurrentCheckPointPos;
        goPlayer.transform.position = tCurrentCheckPointPos;
        
        //
        

        goPlayer.GetComponent<Animator>().SetTrigger("Revive");
    }
    public void Death3()
    {
        goVirtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = true;
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        if (bSkill[0])
        {
            scriptPlayer.CatchBall();
        }
    }
}

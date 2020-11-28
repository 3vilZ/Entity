﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int iCoreStart;
    GameObject goPlayer;
    GameObject goBall;
    GameObject goCurrentVirtualCamera;
    GameObject goOldVirtualCamera;
    GameObject goCheckPointCamera;
    bool[] bSkill = new bool[3];
    int iCollectables;
    Vector2 tCurrentCheckPointPos;
    PlayerControllerV3 scriptPlayer;
    Vector2 tDeathPos;
    

    public bool[] BSkill { get => bSkill; set => bSkill = value; }
    public Vector2 TCurrentCheckPointPos { get => tCurrentCheckPointPos; set => tCurrentCheckPointPos = value; }
    public GameObject GoPlayer { get => goPlayer; set => goPlayer = value; }
    public GameObject GoBall { get => goBall; set => goBall = value; }
    public GameObject GoCurrentVirtualCamera { get => goCurrentVirtualCamera; set => goCurrentVirtualCamera = value; }
    public GameObject GoOldVirtualCamera { get => goOldVirtualCamera; set => goOldVirtualCamera = value; }
    public GameObject GoCheckPointCamera { get => goCheckPointCamera; set => goCheckPointCamera = value; }
    public int ICollectables { get => iCollectables; set => iCollectables = value; }
    public int ICoreStart { get => iCoreStart; set => iCoreStart = value; }
    public PlayerControllerV3 ScriptPlayer { get => scriptPlayer; set => scriptPlayer = value; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        goPlayer = GameObject.FindGameObjectWithTag("Player");
        goBall = GameObject.FindGameObjectWithTag("Ball");

        //***************** Hecho en ConfinerTransition.cs ****************************
        //goCurrentVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");
        //goOldVirtualCamera = goCurrentVirtualCamera;
        //goCheckPointCamera = goCurrentVirtualCamera;

        scriptPlayer = goPlayer.GetComponent<PlayerControllerV3>();

        tCurrentCheckPointPos = goPlayer.transform.position;


        
        /*
        for (int i = 0; i < bSkill.Length; i++)
        {
            bSkill[i] = false;
        }
        */
    }

    private void Start()
    {
        /*
        if(bSkill[0])
        {
            
            scriptPlayer.CatchBall();
            scriptPlayer.lineRenderer = goBall.GetComponent<LineRenderer>();
            
        }
        else
        {
            goBall.SetActive(false);
        }
        */

        /*
        switch (iCoreStart)
        {
            case 0:
                goBall.SetActive(false);
                break;
            case 1:
                GetSkill(0);
                break;
            case 2:
                GetSkill(0);
                GetSkill(1);
                break;
            case 3:
                GetSkill(0);
                GetSkill(1);
                GetSkill(2);
                break;
            default:
                print("PdroP");
                break;
        }
        */

    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            print(bSkill[0]);
            print(bSkill[1]);
            print(bSkill[2]);
        }
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
        goCheckPointCamera = goCurrentVirtualCamera;
    }

    public void Death1()
    {
        goPlayer.GetComponent<Animator>().SetTrigger("Death");
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        tDeathPos = goPlayer.transform.position;
    }
    public void Death2()
    {
        goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 0;
        goPlayer.transform.position = tCurrentCheckPointPos;
        
        if(goCheckPointCamera != goCurrentVirtualCamera)
        {
            goCurrentVirtualCamera.SetActive(false);
            goOldVirtualCamera.SetActive(false);
            goCheckPointCamera.SetActive(true);

            goCurrentVirtualCamera = goCheckPointCamera;
            goOldVirtualCamera = goCheckPointCamera;
        }

        goPlayer.GetComponent<Animator>().SetTrigger("Revive");

        //Vector3 delta = tCurrentCheckPointPos - tDeathPos;
        //goVirtualCamera.GetComponent<CinemachineVirtualCamera>().enabled = false;
        //goVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().OnTargetObjectWarped(goVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow, delta);
    }
    public void Death3()
    {
        goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 0.4f;
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        if (bSkill[0])
        {
            scriptPlayer.CatchBall();
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}

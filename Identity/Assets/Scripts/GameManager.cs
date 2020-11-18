using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject goPlayer;
    GameObject goBall;
    GameObject goVirtualCamera;
    bool[] bSkill = new bool[3];
    Vector2 tCurrentCheckPointPos;

    public bool[] BSkill { get => bSkill; set => bSkill = value; }
    public Vector2 TCurrentCheckPointPos { get => tCurrentCheckPointPos; set => tCurrentCheckPointPos = value; }
    public GameObject GoPlayer { get => goPlayer; set => goPlayer = value; }
    public GameObject GoBall { get => goBall; set => goBall = value; }
    public GameObject GoVirtualCamera { get => goVirtualCamera; set => goVirtualCamera = value; }

    private void Awake()
    {
        if (Instance == null) { Instance = this; }

        goPlayer = GameObject.FindGameObjectWithTag("Player");
        goBall = GameObject.FindGameObjectWithTag("Ball");
        goVirtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");

        goBall.SetActive(false);

        tCurrentCheckPointPos = goPlayer.transform.position;

        for (int i = 0; i < bSkill.Length; i++)
        {
            bSkill[i] = false;
        }
    }

    public void GetSkill(int value)
    {
        bSkill[value] = true;

        if(bSkill[0])
        {
            goBall.SetActive(true);
            goPlayer.GetComponent<PlayerControllerV3>().CatchBall();
        }
    }

    public void SetCheckPoint(Vector2 newPosition)
    {
        tCurrentCheckPointPos = newPosition;
    }

    public void Death()
    {
        goVirtualCamera.transform.position = tCurrentCheckPointPos;
        goPlayer.transform.position = tCurrentCheckPointPos;
        goPlayer.GetComponent<PlayerControllerV3>().CatchBall();
    }
}

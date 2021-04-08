using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    

    [Tooltip("0 = Nada, 1 = Lanzar, 2 = Recoger, 3 = Dash")]
    [SerializeField] int iCoreStart;

    public bool[] goLobby1;
    public bool[] goLobby2;
    public bool[] goLobby3;

    GameObject goPlayer;
    GameObject goBall;
    GameObject goCurrentVirtualCamera;
    GameObject goOldVirtualCamera;
    GameObject goCheckPointCamera;
    bool[] bSkill = new bool[3];
    int iCollectables;
    Vector2 tCurrentCheckPointPos;
    PlayerControllerV3 scriptPlayer;
    int iSpawn;
    //Vector2 tDeathPos;

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
    public int ISpawn { get => iSpawn; set => iSpawn = value; }

    private void OnLevelWasLoaded(int level)
    {
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        goBall = GameObject.FindGameObjectWithTag("Ball");
        scriptPlayer = goPlayer.GetComponent<PlayerControllerV3>();
        tCurrentCheckPointPos = goPlayer.transform.position;

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_1"))
        {
            for (int i = 0; i < goLobby1.Length; i++)
            {
                if(goLobby1[i] == true)
                {
                    Destroy(FindObjectOfType<LobbyManager>().goLobby[i].gameObject);
                }
            }
        }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_2"))
        {
            for (int i = 0; i < goLobby2.Length; i++)
            {
                if (goLobby2[i] == true)
                {
                    Destroy(FindObjectOfType<LobbyManager>().goLobby[i].gameObject);
                }
            }
        }
        else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_3"))
        {
            for (int i = 0; i < goLobby3.Length; i++)
            {
                if (goLobby3[i] == true)
                {
                    Destroy(FindObjectOfType<LobbyManager>().goLobby[i].gameObject);
                }
            }
        }
        else
        {
            
        }

        /*
        for (int i = 0; i < bSkill.Length; i++)
        {
            bSkill[i] = false;
        }
        */
    }

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

        DontDestroyOnLoad(gameObject);

    
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
        Cursor.visible = false;

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
        

        
        switch (iCoreStart)
        {
            case 0:
                //goBall.SetActive(false);
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
        if (Input.GetKey(KeyCode.O))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                iSpawn = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        } 
    }
    
    public void GetSkill(int value)
    {
        bSkill[value] = true;

        if (bSkill[0])
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
        if(newPosition != TCurrentCheckPointPos)
        {
            tCurrentCheckPointPos = newPosition;
            goCheckPointCamera = goCurrentVirtualCamera;

            //KeepInfo
            {
                for (int i = 0; i < listButtonInteract.Count; i++)
                {
                    listButtonInteract[i].bKeepInfo = true;
                }
                for (int i = 0; i < listFragile.Count; i++)
                {
                    listFragile[i].bKeepInfo = true;
                }

                for (int i = 0; i < listPlatformMove.Count; i++)
                {
                    listPlatformMove[i].bKeepInfo = true;
                }
                for (int i = 0; i < listMagma.Count; i++)
                {
                    listMagma[i].bKeepInfo = true;
                }
                for (int i = 0; i < listFire.Count; i++)
                {
                    listFire[i].bKeepInfo = true;
                }
                for (int i = 0; i < listPlatformFall.Count; i++)
                {
                    listPlatformFall[i].bKeepInfo = true;
                }
            }
        }
        
    }

    public void LookAheadSmoothing (bool bDisable)
    {
        
        if(bDisable)
        {
            //goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0;
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 0;
        }
        else
        {
            //goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0.4f;
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 10;
        }
        
    }

    public void Death1()
    {
        //goPlayer.GetComponent<Animator>().
        //scriptPlayer.BoolDeath();
        Time.timeScale = 1;
        goPlayer.GetComponent<Animator>().SetTrigger("Death");
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        goPlayer.GetComponent<BoxCollider2D>().enabled = false;
        //tDeathPos = goPlayer.transform.position;

        
    }
    public void Death2()
    {
        LookAheadSmoothing(true);
        

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
        LookAheadSmoothing(false);
        

        goPlayer.GetComponent<BoxCollider2D>().enabled = true;
        goPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        
        if (bSkill[0])
        {
            scriptPlayer.CatchBall();
        }

        KeepInfo();
    }

    public void LoadLevel(string levelName)
    {
        if(levelName == "Quit")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }



    [HideInInspector] public List<ButtonInteract> listButtonInteract = new List<ButtonInteract>();
    [HideInInspector] public List<Fragile> listFragile = new List<Fragile>();
    [HideInInspector] public List<PlatformMove> listPlatformMove = new List<PlatformMove>();
    [HideInInspector] public List<Magma> listMagma = new List<Magma>();
    [HideInInspector] public List<Fire> listFire = new List<Fire>();
    [HideInInspector] public List<PlatformFall> listPlatformFall = new List<PlatformFall>();

    public void KeepInfo()
    {
        for (int i = 0; i < listButtonInteract.Count; i++)
        {
            listButtonInteract[i].Reset();
        } 
        for (int i = 0; i < listFragile.Count; i++)
        {
            listFragile[i].Reset();
        }
        for (int i = 0; i < listPlatformMove.Count; i++)
        {
            listPlatformMove[i].Reset();
        }
        for (int i = 0; i < listMagma.Count; i++)
        {
            listMagma[i].Reset();
        }
        for (int i = 0; i < listFire.Count; i++)
        {
            listFire[i].Reset();
        }
        for (int i = 0; i < listPlatformFall.Count; i++)
        {
            listPlatformFall[i].Reset();
        }

        listButtonInteract.Clear();
        listFragile.Clear();
        listPlatformMove.Clear();
        listMagma.Clear();
        listFire.Clear();
        listPlatformFall.Clear();
    }
    public void KeepInfo(ButtonInteract info)
    {
        if(!listButtonInteract.Contains(info))
        {
            listButtonInteract.Add(info);
        }
    }
    public void KeepInfo(Fragile info)
    {
        if (!listFragile.Contains(info))
        {
            listFragile.Add(info);
        }
    }
    public void KeepInfo(PlatformMove info)
    {
        if (!listPlatformMove.Contains(info))
        {
            listPlatformMove.Add(info);
        }
    }
    public void KeepInfo(Magma info)
    {
        if (!listMagma.Contains(info))
        {
            listMagma.Add(info);
        }
    }
    public void KeepInfo(Fire info)
    {
        if (!listFire.Contains(info))
        {
            listFire.Add(info);
        }
    }
    public void KeepInfo(PlatformFall info)
    {
        if (!listPlatformFall.Contains(info))
        {
            listPlatformFall.Add(info);
        }
    }

    public void CheckIfLobby(GameObject go)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_1"))
        {
            for (int i = 0; i < FindObjectOfType<LobbyManager>().goLobby.Length; i++)
            {
                if(go == FindObjectOfType<LobbyManager>().goLobby[i])
                {
                    goLobby1[i] = true;
                    return;
                }
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_2"))
        {
            for (int i = 0; i < FindObjectOfType<LobbyManager>().goLobby.Length; i++)
            {
                if (go == FindObjectOfType<LobbyManager>().goLobby[i])
                {
                    goLobby2[i] = true;
                    return;
                }
            }
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Lobby_1"))
        {
            for (int i = 0; i < FindObjectOfType<LobbyManager>().goLobby.Length; i++)
            {
                if (go == FindObjectOfType<LobbyManager>().goLobby[i])
                {
                    goLobby3[i] = true;
                    return;
                }
            }
        }
    }
}

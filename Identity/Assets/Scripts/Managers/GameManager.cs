using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    [HideInInspector] public int iSceneIndex;
    [HideInInspector] public float fVolumeMultiplier = 1;
    [HideInInspector] public bool bStandardAim = true;
    //Vector2 tDeathPos;


    //Keep Info Lists
    [HideInInspector] public List<ButtonInteract> listButtonInteract = new List<ButtonInteract>();
    [HideInInspector] public List<Fragile> listFragile = new List<Fragile>();
    [HideInInspector] public List<PlatformMove> listPlatformMove = new List<PlatformMove>();
    //[HideInInspector] public List<Magma> listMagma = new List<Magma>();
    [HideInInspector] public List<MagmaNew> listMagmaNew = new List<MagmaNew>();
    [HideInInspector] public List<Fire> listFire = new List<Fire>();
    [HideInInspector] public List<PlatformFall> listPlatformFall = new List<PlatformFall>();

    //Collectable Lists
    [HideInInspector] public List<int> listCollectableID = new List<int>();

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

        iSceneIndex = level;

        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.bMainMenu = true;
        }
        else
        {
            AudioManager.Instance.bMainMenu = false;
            SaveGame();
        }
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

        scriptPlayer = goPlayer.GetComponent<PlayerControllerV3>();
        tCurrentCheckPointPos = goPlayer.transform.position;

        
    }

    private void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.Instance.bMainMenu = true;
        }
        else
        {
            AudioManager.Instance.bMainMenu = false;
        }
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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < goLobby1.Length; i++)
            {
                print("goLobby1: " + goLobby1[i]);
            }
            for (int i = 0; i < goLobby2.Length; i++)
            {
                print("goLobby2: " + goLobby2[i]);
            }
            for (int i = 0; i < goLobby3.Length; i++)
            {
                print("goLobby3: " + goLobby3[i]);
            }

            print("ISpawn: " + ISpawn);

            print("ICoreStart: " + ICoreStart);

            print("ICollectables: " + ICollectables);

            for (int i = 0; i < listCollectableID.Count; i++)
            {
                print("listCollectableID: " + listCollectableID[i]);

            }

            print("iSceneIndex: " + iSceneIndex);

            print("fVolumeMultiplier: " + fVolumeMultiplier);

            print("bStandardAim: " + bStandardAim);
        }                                                                                                  
    }

    public void ChangeAim()
    {
        if (bStandardAim)
        {
            bStandardAim = false;
            scriptPlayer.bStandardAim = false;
        }
        else
        {
            bStandardAim = true;
            scriptPlayer.bStandardAim = true;
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

    public void GetCollectable(int ID)
    {
        iCollectables++;

        if (!listCollectableID.Contains(ID))
        {
            listCollectableID.Add(ID);
        }
    }

    public void CheckCollectable(int ID, Collectable info)
    {
        if (listCollectableID.Contains(ID))
        {
            Destroy(info.gameObject);
        }
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
                /*
                for (int i = 0; i < listMagma.Count; i++)
                {
                    listMagma[i].bKeepInfo = true;
                }
                */
                for (int i = 0; i < listMagmaNew.Count; i++)
                {
                    listMagmaNew[i].bKeepInfo = true;
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
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 0;
        }
        else
        {
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 10;
        }
    }

    public void LookAheadTime(bool bDisable)
    {
        if (bDisable)
        {
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0f;
        }
        else
        {
            goCurrentVirtualCamera.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0.4f;
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
        AudioManager.Instance.PlaySound("Death");

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
        //AudioManager.Instance.PlaySound("Revive");

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
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

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
        /*
        for (int i = 0; i < listMagma.Count; i++)
        {
            listMagma[i].Reset();
        }
        */
        for (int i = 0; i < listMagmaNew.Count; i++)
        {
            listMagmaNew[i].Reset();
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
        //listMagma.Clear();
        listMagmaNew.Clear();
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
    /*
    public void KeepInfo(Magma info)
    {
        if (!listMagma.Contains(info))
        {
            listMagma.Add(info);
        }
    }
    */
    public void KeepInfo(MagmaNew info)
    {
        if (!listMagmaNew.Contains(info))
        {
            listMagmaNew.Add(info);
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

    public void VolumeMultiplier(Slider slider)
    {
        fVolumeMultiplier = slider.value;
    }

    public void ContinueGame()
    {
        LoadLevel(iSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        for (int i = 0; i < goLobby1.Length; i++)
        {
            goLobby1[i] = false;
        }
        for (int i = 0; i < goLobby2.Length; i++)
        {
            goLobby2[i] = false;
        }
        for (int i = 0; i < goLobby3.Length; i++)
        {
            goLobby3[i] = false;
        }

        ISpawn = 0;

        ICoreStart = 0;

        ICollectables = 0;

        listCollectableID.Clear();

        iSceneIndex = 1;

        //fVolumeMultiplier = 1;

        bStandardAim = true;

        LoadLevel(iSceneIndex);
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();

        for (int i = 0; i < data.goLobby1.Length; i++)
        {
            goLobby1[i] = data.goLobby1[i];
        }
        for (int i = 0; i < data.goLobby2.Length; i++)
        {
            goLobby2[i] = data.goLobby2[i];
        }
        for (int i = 0; i < data.goLobby3.Length; i++)
        {
            goLobby3[i] = data.goLobby3[i];
        }

        ISpawn = data.iSpawn;

        ICoreStart = data.iCoreStart;

        ICollectables = data.iCollectables;

        listCollectableID.Clear();

        for (int i = 0; i < data.listCollectableID.Count; i++)
        {
            listCollectableID.Add(data.listCollectableID[i]);
        }

        iSceneIndex = data.iSceneIndex;

        fVolumeMultiplier = data.fVolumeMultiplier;

        bStandardAim = data.bStandardAim;
    }
}

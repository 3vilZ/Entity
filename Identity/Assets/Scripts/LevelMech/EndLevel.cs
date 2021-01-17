using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    [SerializeField] float fDistance;
    public ScriptableEndLvl[] module;
    

    private void Start()
    {
        for (int i = 0; i < module.Length; i++)
        {
            module[i].goY.SetActive(false);
        }

        GameManager.Instance.GoPlayer.transform.position = module[GameManager.Instance.ISpawn].tSpawnPoint.position;
    }

    private void Update()
    {
        if (module.Length > 0)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, module[0].tSpawnPoint.transform.position) <= fDistance)
            {
                module[0].goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.ISpawn = module[0].iNextSpawn;
                    GameManager.Instance.LoadLevel(module[0].strNextLevel);
                }
            }
            else
            {
                module[0].goY.SetActive(false);
            }
        }
        if (module.Length > 1)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, module[1].tSpawnPoint.transform.position) <= fDistance)
            {
                module[1].goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.ISpawn = module[1].iNextSpawn;
                    GameManager.Instance.LoadLevel(module[1].strNextLevel);
                }
            }
            else
            {
                module[1].goY.SetActive(false);
            }
        }
        if (module.Length > 2)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, module[2].tSpawnPoint.transform.position) <= fDistance)
            {
                module[2].goY.SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.ISpawn = module[2].iNextSpawn;
                    GameManager.Instance.LoadLevel(module[2].strNextLevel);
                }
            }
            else
            {
                module[2].goY.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        if (module.Length > 0)
        {
            Gizmos.DrawCube(module[0].tSpawnPoint.transform.position, new Vector3(1, 2.4f, 1));
        }
        if (module.Length > 1)
        {
            Gizmos.DrawCube(module[1].tSpawnPoint.transform.position, new Vector3(1, 2.4f, 1));
        }
        if (module.Length > 2)
        {
            Gizmos.DrawCube(module[2].tSpawnPoint.transform.position, new Vector3(1, 2.4f, 1));
        }
    }

    /*
    [SerializeField] string strNextLevel;
    [SerializeField] Transform[] tSpawnPoint;
    [Tooltip("0 para el spawn de entrada, 1 para la salida 1, 2 para la salida 2")]
    [SerializeField] int iNextSpawn;
    [SerializeField] float fDistance;
    [SerializeField] GameObject[] goY;

    private void Start()
    {
        foreach (GameObject go in goY)
        {
            go.SetActive(false);
        }
    }

    private void Update()
    {
        if (tSpawnPoint.Length > 0)
        {
            if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, tSpawnPoint[0].transform.position) <= fDistance)
            {
                goY[0].SetActive(true);

                if (Input.GetButtonDown("Y"))
                {
                    GameManager.Instance.LoadLevel(strNextLevel);
                }
            }
            else
            {
                goY.SetActive(false);
            }
        }
        if (tSpawnPoint.Length > 1)
        {
            
        }
        if (tSpawnPoint.Length > 2)
        {
            
        }

        if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
        {
            goY.SetActive(true);

            if (Input.GetButtonDown("Y"))
            {
                GameManager.Instance.LoadLevel(strNextLevel);
            }
        }
        else
        {
            goY.SetActive(false);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        if(tSpawnPoint.Length > 0)
        {
            Gizmos.DrawCube(tSpawnPoint[0].transform.position, Vector3.one * 2);
        }
        if (tSpawnPoint.Length > 1)
        {
            Gizmos.DrawCube(tSpawnPoint[1].transform.position, Vector3.one * 2);
        }
        if (tSpawnPoint.Length > 2)
        {
            Gizmos.DrawCube(tSpawnPoint[2].transform.position, Vector3.one * 2);
        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTransition : MonoBehaviour
{
    [SerializeField] float fDistance;
    [SerializeField] GameObject goY;

    [Tooltip("Módulo en el que va a spawnear en el siguiente nivel")]
    public int iNextSpawn;
    [Tooltip("Nombre siguiente nivel")]
    public string strNextLevel;

    void Update()
    {
        if (Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
        {
            goY.SetActive(true);

            if (Input.GetButtonDown("Y"))
            {
                GameManager.Instance.ISpawn = iNextSpawn;
                GameManager.Instance.LoadLevel(strNextLevel);
            }
        }
        else
        {
            goY.SetActive(false);
        }
    }
}

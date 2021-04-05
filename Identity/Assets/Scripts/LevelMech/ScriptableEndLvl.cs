using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScriptableEndLvl :MonoBehaviour
{
    [Tooltip("Módulo en el que va a spawnear en el siguiente nivel")]
    public int iNextSpawn;
    [Tooltip("Nombre siguiente nivel")]
    public string strNextLevel;
    [Tooltip("Punto de spawn de ESTE módulo")]
    public Transform tSpawnPoint;
    [Tooltip("¿El Trigger de este módulo funciona?")]
    public bool bLevelTransition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(bLevelTransition)
        {
            if (other.gameObject.tag == "Player")
            {
                GameManager.Instance.ISpawn = iNextSpawn;
                GameManager.Instance.LoadLevel(strNextLevel);
            }
        }
    }
}

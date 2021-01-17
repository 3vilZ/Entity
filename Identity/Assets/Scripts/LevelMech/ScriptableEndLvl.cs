using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ScriptableEndLvl
{
    public Transform tSpawnPoint;
    [Tooltip("0 para el spawn de entrada, 1 para la salida 1, 2 para la salida 2")]
    public int iNextSpawn;
    public string strNextLevel;
    public GameObject goY;
}

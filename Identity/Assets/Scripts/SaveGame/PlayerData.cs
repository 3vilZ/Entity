using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool[] goLobby1;
    public bool[] goLobby2;
    public bool[] goLobby3;

    public int iSpawn;

    public int iCoreStart;

    public int iCollectables;

    public List<int> listCollectableID = new List<int>();

    public int iSceneIndex;

    public PlayerData (GameManager gm)
    {
        goLobby1 = new bool[gm.goLobby1.Length];
        goLobby2 = new bool[gm.goLobby2.Length];
        goLobby3 = new bool[gm.goLobby3.Length];

        for (int i = 0; i < gm.goLobby1.Length; i++)
        {
            goLobby1[i] = gm.goLobby1[i];
        }
        for (int i = 0; i < gm.goLobby2.Length; i++)
        {
            goLobby2[i] = gm.goLobby2[i];
        }
        for (int i = 0; i < gm.goLobby3.Length; i++)
        {
            goLobby3[i] = gm.goLobby3[i];
        }

        iSpawn = gm.ISpawn;

        iCoreStart = gm.ICoreStart;

        iCollectables = gm.ICollectables;

        for (int i = 0; i < gm.listCollectableID.Count; i++)
        {
            listCollectableID.Add(gm.listCollectableID[i]);
        }

        iSceneIndex = gm.iSceneIndex;
    } 
}

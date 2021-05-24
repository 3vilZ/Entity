using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    bool bInteract = false;

    public void BackMainMenu()
    {
        if(bInteract)
        {
            GameManager.Instance.iSceneIndex = 2;
            GameManager.Instance.ISpawn = 0;
            GameManager.Instance.SaveGame();
            GameManager.Instance.LoadLevel(0);
        }
        
    }

    public void Interact()
    {
        bInteract = true;
    }
}

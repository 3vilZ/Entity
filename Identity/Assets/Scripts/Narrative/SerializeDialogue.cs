using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SerializeDialogue
{
    public string strName;

    [TextArea(3, 10)]
    public string[] strSentence;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetAnim : MonoBehaviour
{
    public Vector2 v2Direction = new Vector2(0.1f, 0f);
    public string textureName = "_LavaTex";
    Vector2 v2Offset = Vector2.zero;
    Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void LateUpdate()
    {
        v2Offset += (v2Direction * Time.deltaTime);
        if (rend.enabled)
        {
            rend.sharedMaterial.SetTextureOffset(textureName, v2Offset);
        }
    }
}

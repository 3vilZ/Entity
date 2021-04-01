using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPhantom : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z));        
    }
}

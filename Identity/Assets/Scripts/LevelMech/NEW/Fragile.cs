using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragile : MonoBehaviour
{
    [HideInInspector] public bool bKeepInfo = false;
    [SerializeField] GameObject goModel;
    //public float fExpForce;
    //public float fExpRadius;
    public float fDissapear;

    Rigidbody[] rbRocks;
    Vector3[] v3Pos;
    Quaternion[] v3Rot;

    private void Start()
    {
        rbRocks = new Rigidbody[goModel.transform.childCount];
        v3Pos = new Vector3[rbRocks.Length];
        v3Rot = new Quaternion[rbRocks.Length];

        for (int i = 0; i < goModel.transform.childCount; i++)
        {
            rbRocks[i] = goModel.transform.GetChild(i).GetComponent<Rigidbody>();
            v3Pos[i] = goModel.transform.GetChild(i).transform.localPosition;
            v3Rot[i] = goModel.transform.GetChild(i).transform.localRotation;
        }
    }

    IEnumerator Dissapear()
    {
        yield return new WaitForSeconds(fDissapear);

        foreach (Transform t in goModel.transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        if(!bKeepInfo)
        {
            GetComponent<BoxCollider2D>().enabled = true;

            for (int i = 0; i < goModel.transform.childCount; i++)
            {
                rbRocks[i].gameObject.SetActive(true);
                rbRocks[i].isKinematic = true;
                rbRocks[i].gameObject.transform.localPosition = v3Pos[i];
                rbRocks[i].gameObject.transform.localRotation = v3Rot[i];

            }

            transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
            transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
        }
    }

    public void Crash()
    {
        for (int i = 0; i < goModel.transform.childCount; i++)
        {
            rbRocks[i].isKinematic = false;
            //rbRocks[i].AddExplosionForce(fExpForce, transform.position, fExpRadius);
        }

        StartCoroutine(Dissapear());

        GameManager.Instance.CheckIfLobby(gameObject);
        GameManager.Instance.KeepInfo(this);
        GetComponent<BoxCollider2D>().enabled = false;
        transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
        transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
        AudioManager.Instance.PlayMechFx("Fragile");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneDoor : MonoBehaviour
{
    [SerializeField] GameObject goBlock;
    [SerializeField] GameObject goKey;
    [SerializeField] float fSpeed;
    [SerializeField] float fDistance;
    [SerializeField] Transform tDestiny;

    bool bUnlock = false;
    bool bActivated = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goKey.GetComponent<RuneKey>().bFollowing)
        {
            if (Vector3.Distance(GameManager.Instance.GoPlayer.transform.position, transform.position) <= fDistance)
            {
                bUnlock = true;
                goKey.GetComponent<RuneKey>().bFollowing = false;
            }
        }

        if(bUnlock)
        {
            goKey.transform.position = Vector3.MoveTowards(goKey.transform.position, goBlock.transform.position, fSpeed * Time.deltaTime);

            if (Vector3.Distance(goKey.transform.position, goBlock.transform.position) <= 0.01)
            {
                goKey.SetActive(false);
                bActivated = true;
                GameManager.Instance.CheckIfLobby(gameObject);
                bUnlock = false;
            }
        }

        if (bActivated)
        {
            goBlock.transform.position = Vector3.MoveTowards(goBlock.transform.position, tDestiny.position, fSpeed * Time.deltaTime);

            if (Vector3.Distance(goBlock.transform.position, tDestiny.position) <= 0.01)
            {
                bActivated = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint : MonoBehaviour
{
    [Header("Content")]
    [SerializeField] Sizes size;
    [SerializeField] Sprite img;
    [TextArea(4, 10)]
    [SerializeField] string txt;

    [Space(20)]
    [SerializeField] float fDistance;
    [SerializeField] GameObject goTrigger;
    [Tooltip("0 = Nada, 1 = Lanzar, 2 = Recoger, 3 = Dash")]
    [SerializeField] int iNeedCore;

    bool bTriggered = false;


    [Header("Components")]
    [SerializeField] GameObject[] goModules;
    [SerializeField] TextMeshProUGUI[] txtModules;
    [SerializeField] Image[] imgModules;
    [SerializeField] ParticleSystem ps;

    Animator animator;
    

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            txtModules[i].text = txt;
            imgModules[i].sprite = img;
            goModules[i].SetActive(false);
        }

        goModules[(int)size].SetActive(true);

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!bTriggered)
        {
            if(Vector2.Distance(GameManager.Instance.GoPlayer.transform.position, goTrigger.transform.position) < fDistance 
                && GameManager.Instance.ICoreStart >= iNeedCore)
            {
                ps.Play();
                StartCoroutine(Grow());
                bTriggered = true;
            }
        }
    }
    public void Sound()
    {
        AudioManager.Instance.PlayMechFx("HintUp");
    }

    IEnumerator Grow()
    {
        AudioManager.Instance.PlayMechFx("Hint");
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Up");
    }


    public enum Sizes
    {
        Large, Medium, Small
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(goTrigger.transform.position, fDistance);
    }
}

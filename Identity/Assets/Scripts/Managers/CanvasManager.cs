using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    [Header("Narrative")]
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtSentence;
    public GameObject goNarrativeDisplay;

    [Header("GetCore")]
    public TextMeshProUGUI txtCenter;
    public TextMeshProUGUI txtLeft;
    public TextMeshProUGUI txtRight;
    public GameObject[] goCore;
    public GameObject goCoreDisplay;

    [Header("Collectable")]
    public TextMeshProUGUI txtCollectable;
    public GameObject goCollectableDisplay;

    [HideInInspector] public Animator canvasAnim;

    bool bButtonCore = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        for (int i = 0; i < goCore.Length; i++)
        {
            goCore[i].SetActive(false);
        }

        txtCenter.text = "";
        txtLeft.text = "";
        txtRight.text = "";

        goNarrativeDisplay.SetActive(false);
        goCoreDisplay.SetActive(false);
        goCollectableDisplay.SetActive(false);
        canvasAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if(bButtonCore)
        {
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Shoot") || Input.GetButtonDown("Y"))
            {
                canvasAnim.SetTrigger("CoreOff");
                
                bButtonCore = false;
            }
        }
    }

    public void ButtonCore()
    {
        bButtonCore = true;
    }
    public void CoreDisplayOff()
    {
        goCoreDisplay.SetActive(false);
        GameManager.Instance.ScriptPlayer.bInteracting = false;
    }
    public void CollectableOff()
    {
        goCollectableDisplay.SetActive(false);
    }
    /*
     * LAYERS
     * -2 InOut
     * -1 Platforms
     * 0 Null
     * 1-6 Player
     * 7-18 Ball
     * 20 Spikes
     * 21-22 Interact
     * 
     * 
     * 26 Collectable
     * 
     * 
     * 
     */
}

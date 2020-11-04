using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]GameObject Pltf;

    [SerializeField]GameObject Uno;
    [SerializeField]GameObject Dos;

    [SerializeField] [Range(0, 2)] float speed;
    [SerializeField]bool loop;
    [SerializeField]bool activation;
    
    [SerializeField]GameObject activButton;
    bool stop = false;
    bool activated;
    Vector3 Direcc;


    void Start()
    {
        Pltf.transform.position = Uno.transform.position;
        Direcc = Dos.transform.position - Uno.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if (activation){    
            if (activated){
                Pltf.transform.position += Direcc * Time.deltaTime * speed;
            }
        }else{
            if (!stop){
                Pltf.transform.position += Direcc * Time.deltaTime * speed;
            }
        }
        

        if (loop){
            if (Vector3.Distance(Pltf.transform.position, Dos.transform.position) <= 0.2f){
                Direcc = Uno.transform.position - Dos.transform.position;
            }else if (Vector3.Distance(Pltf.transform.position, Uno.transform.position) <= 0.2f ){
                Direcc = Dos.transform.position - Uno.transform.position;
            }
        }else{
            if (Vector3.Distance(Pltf.transform.position, Dos.transform.position) <= 0.2f){
                stop = true;
            }
        }
        
    }

    public void ActivatePltf(){
        activated = true;
    }
}

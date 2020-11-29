using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideLevel : MonoBehaviour
{

    public float[] colors;

    bool ballin;
    bool playerin;

    void Start(){

        colors = new float[transform.childCount];

       int children = transform.childCount;

            for (int i = 0; i < children; ++i){

                Color tmp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                colors[i] = tmp.a;
            }
    }


   void OnTriggerEnter2D(Collider2D col)
    {


        if (col.tag == "Player" || col.tag == "Ball" ){
            int children = transform.childCount;

            for (int i = 0; i < children; ++i){

                Color tmp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;

            }


            if (col.tag == "Player"){
                playerin = true;
            }else{
                ballin = true;
            }
        }
        
             
    }

    /*
    void OnTriggerExit2D(Collider2D col)
    {

        if (col.tag == "Player" || col.tag == "Ball"){

            if (col.tag == "Ball"){
                ballin = false;

                if(!playerin){
                    int children = transform.childCount;
                    for (int i = 0; i < children; ++i){
                    Color tmp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                    tmp.a = colors[i];
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
                    }
                }
            }else{
                playerin = false;

                if(!ballin){
                    int children = transform.childCount;
                    for (int i = 0; i < children; ++i){
                    Color tmp = transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                    tmp.a = colors[i];
                    transform.GetChild(i).GetComponent<SpriteRenderer>().color = tmp;
                    }
                }

            }


        }
    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    public float speed = 20;
    public GameObject ArmaPistola;
    // Start is called before the first frame update
    void Start(){
        
    }
    // La bala avanza (gravedad = 0.25)
    void FixedUpdate(){
        transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    public void OnTriggerEnter2D(Collider2D other){
        Destroy(gameObject);
        if(other.gameObject.tag == "player"){
            //killplayer
        }
    }
}


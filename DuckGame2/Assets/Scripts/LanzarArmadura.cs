using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarArmadura : Objeto
{
    public GameObject PrefabEscudo;
    public float secondsCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarArmadura";
        Activar();
    }

    // Update is called once per frame
    void Update()
    {
        //contador de segundos activos
        secondsCounter += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)  && interactuable == true){
            PrefabEscudo.SetActive(true);
            gameObject.transform.parent.gameObject.GetComponent<Collider2D>().enabled=false; //peta
            secondsCounter = 0;
        }

        if(secondsCounter > 5){
           PrefabEscudo.SetActive(false);
        }
        
        if(numUsos == 0){
            gameObject.SetActive(false);
        }
    }

    private void Activar(){
        gameObject.SetActive(true);
        numUsos = 1; //Numero de balas
        interactuable = true;
    }
}

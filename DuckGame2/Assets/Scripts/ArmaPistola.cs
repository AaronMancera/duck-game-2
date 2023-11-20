using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPistola : Objeto
{
    public GameObject BalaPrefab;
    public float secondsCounter=0;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "armaPistola";
        Activar();

        Application.targetFrameRate = 60;
    }

/// <summary>
/// La pistola dispara y tiene un retardo de 0.5 segundos para poder disparar de nuevo
/// </summary>
    // Update is called once per frame
    void Update()
    {
        //contador de segundos para el cooldown
        secondsCounter += Time.deltaTime;
        //Debug.Log(secondsCounter);
        
        if (Input.GetKeyDown(KeyCode.Space)  && interactuable == true && numUsos > 0){
            secondsCounter = 0;
            numUsos = numUsos - 1;
            interactuable = false;
            Instantiate(BalaPrefab, transform.position, gameObject.transform.rotation);
        }

        if(secondsCounter > 0.5){
            interactuable = true;
        }
    //NOTE: Si se queda sin balas se desactiva el objeto
        if(numUsos == 0){
            gameObject.SetActive(false);
        }
         
    }
/// <summary>
/// Método que reinicia el arma cuando un jugador la obtenga
/// </summary>
    private void Activar(){
        gameObject.SetActive(true);
        numUsos = 6; //Numero de balas
        interactuable = true;
    }
}

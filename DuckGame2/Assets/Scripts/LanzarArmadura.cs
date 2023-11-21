using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarArmadura : Objeto
{
    public GameObject PrefabEscudo;
    [SerializeField] private Collider2D colisionJugador;
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
            Escudo();
        }

        if(secondsCounter > 5){
           FinEscudo();
        }
        
        if(numUsos == 0){
            gameObject.SetActive(false);
        }
    }

    private void Activar(){
        gameObject.SetActive(true);
        numUsos = 1;
        interactuable = true;
    }

    private void Escudo(){
        PrefabEscudo.SetActive(true);
            Debug.Log(gameObject.transform.parent.name);
            colisionJugador.enabled= false;
            secondsCounter = 0;
    }

    private void FinEscudo(){
        PrefabEscudo.SetActive(false);
        colisionJugador.enabled = true;
    }
}

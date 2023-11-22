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
    }


    // Update is called once per frame
    void Update()
    {
        //contador de segundos activos
        secondsCounter += Time.deltaTime;

        //if (Input.GetKeyDown(KeyCode.Space) && secondsCounter==0){
        //    PonerseEscudo();
        //}
        if (InputManager.playerControls.Player.DispararPrincipal.IsPressed() && secondsCounter == 0)
        {
            PonerseEscudo();
        }

        if (secondsCounter > 5){
           FinEscudo();
        }
        
        if(numUsos == 0){
            Reiniciar();
        }
    }

    //private void Inicializar(){
    //    gameObject.SetActive(true);
    //    numUsos = 1;
    //    interactuable = true;
    //}
    private void Reiniciar()
    {
        gameObject.SetActive(false);
    }

    private void PonerseEscudo(){
        PrefabEscudo.SetActive(true);
            Debug.Log(gameObject.transform.parent.name);
            colisionJugador.enabled= false;
            secondsCounter = 0;
    }

    private void FinEscudo(){
        numUsos = 0;
        PrefabEscudo.SetActive(false);
        colisionJugador.enabled = true;
    }
}

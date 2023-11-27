using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarPollo : Objeto
{
    [SerializeField] ControlJugador controlJugador;
    public GameObject PolloPrefab;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarPollo";
        Inicializar();
    }

    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerarPollo();
        }*/

        if (numUsos == 0)
        {
            Reiniciar();
        }
    }

    #region InputSystem

    private void OnEnable()
    {
        if (controlJugador.idPlayer == 1)
        {
            controlJugador.playerControls.Player.DispararSecundario.performed += GetDispararInput;

        }
        else if (controlJugador.idPlayer == 2)
        {
            controlJugador.playerControls.PlayerP2.DispararSecundario.performed += GetDispararInput;
        }
    }

    private void OnDisable()
    {
        if (controlJugador.idPlayer == 1)
        {
            controlJugador.playerControls.Player.DispararSecundario.performed -= GetDispararInput;

        }
        else if (controlJugador.idPlayer == 2)
        {
            controlJugador.playerControls.PlayerP2.DispararSecundario.performed -= GetDispararInput;
        }
    }

    private void GetDispararInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GenerarPollo();
        }
    }
    #endregion

    private void GenerarPollo()
    {
        GameObject pollo = Instantiate(PolloPrefab, transform.position, Quaternion.identity);
        pollo.GetComponent<MovimientoPollo>().mirandoDerecha = !controlJugador.mirandoALaDerecha;
        numUsos = numUsos -1; 
    }


    private void Inicializar()
    {
        gameObject.SetActive(true);
        numUsos = 1;
    }

    public void Reiniciar()
    {
        //Llamar al jugador y quitarle el arma secundaria
        controlJugador.SoltarArma(false);

        gameObject.SetActive(false);
    }



   
}

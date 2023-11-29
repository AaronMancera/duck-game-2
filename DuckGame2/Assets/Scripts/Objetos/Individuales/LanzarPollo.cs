using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarPollo : Objeto
{
    [SerializeField] ControlJugador controlJugador;
    public GameObject PolloPrefab;

    [SerializeField] AudioClip lanzarPolloSFX;
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
        Vector2 lugarDeInstanciamiento = transform.position;
        lugarDeInstanciamiento.y += 1;
        if (transform.parent.transform.localScale.x==-1)
        {
            lugarDeInstanciamiento.x += 1;
        }
        else
        {
            lugarDeInstanciamiento.x -= 1;

        }
        GameObject pollo = Instantiate(PolloPrefab, lugarDeInstanciamiento, Quaternion.identity);
        pollo.GetComponent<MovimientoPollo>().mirandoDerecha = !controlJugador.mirandoALaDerecha;

        AudioManager.instanceAudioManager.PlaySFX(lanzarPolloSFX);


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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmaBomba : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    [Header("Atributos")]
    [SerializeField] private GameObject bomba;
    [SerializeField] private Transform puntoDeLanzar;

    [SerializeField] AudioClip lanzarBomba;

    // Start is called before the first frame update
    void Start()
    {
        nombre = "ArmaBomba";
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        if (controlDelJugador.idPlayer == 1)
        {
            controlDelJugador.playerControls.Player.DispararPrincipal.performed += GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.DispararPrincipal.performed += GetDispararInput;
        }
    }

    private void OnDisable()
    {
        if (controlDelJugador.idPlayer == 1)
        {
            controlDelJugador.playerControls.Player.DispararPrincipal.performed -= GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.DispararPrincipal.performed -= GetDispararInput;
        }
    }
    private void GetDispararInput(InputAction.CallbackContext context)
    {
        if (context.performed && numUsos > 0)
        {
            Disparar();
        }
    }

    void Update()
    {
        gameObject.SetActive(Municion());
    }

    /// <summary>
    /// Dispara la bomba
    /// </summary>
    private void Disparar()
    {
        Bomba componenteBomba = bomba.gameObject.GetComponent<Bomba>();

        Bomba bombaInvocada = Instantiate(componenteBomba, puntoDeLanzar.transform.position, /*bomba.gameObject.transform.rotation*/ Quaternion.identity);
        bombaInvocada.transform.localScale = new Vector3(transform.parent.localScale.x, componenteBomba.transform.localScale.y, componenteBomba.transform.localScale.z);
        AudioManager.instanceAudioManager.PlaySFX(lanzarBomba);


        numUsos--;
    }

    /// <summary>
    /// Activa el gameObject
    /// </summary>
    //private void Activar()
    //{
    //    gameObject.SetActive(true);
    //    numUsos = 1;
    //    interactuable = true;
    //}
    /// <summary>
    /// Devuelve un booleano para saber si tenemos balas
    /// </summary>
    private bool Municion()
    {
        if (numUsos <= 0)
        {
            controlDelJugador.SoltarArma(true); //Pongo true porque es principal
            return false;
        }
        return true;
    }
}

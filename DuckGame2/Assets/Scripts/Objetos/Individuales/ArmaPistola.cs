using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmaPistola : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    [SerializeField] private GameObject BalaPrefab;
    [SerializeField] private Transform puntoDeDisparar;
    private float secondsCounter = 0;
    private bool puedeDispara;


    [SerializeField] AudioClip audioDisparar;


    // Start is called before the first frame update
    void Start()
    {
        ////TESTING
        //Inicializar();
        //Application.targetFrameRate = 60;
        nombre = "armaPistola";
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

        //if (Input.GetKeyDown(KeyCode.Space) && puedeDispara == true && numUsos > 0)
       /* if (InputManager.playerControls.Player.DispararPrincipal.enabled && puedeDispara == true && numUsos > 0)
        {
            Disparar();
        }*/

        //retardo de disparo
        if (secondsCounter > 0.5)
        {
            puedeDispara = true;
        }
        //NOTE: Si se queda sin balas se desactiva el objeto
        if (numUsos == 0)
        {
            SinMunicion();
        }

    }
    #region InputSystem

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
        if (context.performed && puedeDispara == true && numUsos > 0)
        {
            //Debug.Log(gameObject.transform.parent.name);
            //Disparar();


            //Animator
            animator.SetTrigger("Disparar");
        }
    }
    #endregion
    /// <summary>
    /// Método que reinicia el arma cuando un jugador la obtenga
    /// </summary>
    //private void Inicializar()
    //{
    //    gameObject.SetActive(true);
    //    numUsos = 2; //Numero de balas
    //    interactuable = true;
    //}

    public void Disparar()
    {
        AudioManager.instanceAudioManager.PlaySFX(audioDisparar);

        secondsCounter = 0;
        numUsos = numUsos - 1;
        puedeDispara = false;


        GameObject bala = Instantiate(BalaPrefab, puntoDeDisparar.transform.position, gameObject.transform.rotation);
        if (gameObject.transform.parent.localScale.x == -1)
        {
            //Izq
            bala.transform.rotation= Quaternion.Euler(0,180, 0);
        }
        else
        {
            //Der
            bala.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //bala.transform.localScale=gameObject.transform.parent.localScale;
    }

    public void SinMunicion()
    {
        gameObject.SetActive(false);

        //Desactiva arma al player
        controlDelJugador.SoltarArma(true); //Pongo true porque es principal
    }
}

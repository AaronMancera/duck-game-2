using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarArmadura : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    public GameObject PrefabEscudo;
    [SerializeField] private Collider2D colisionJugador;
    public float secondsCounter = 0;
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
        //if (InputManager.playerControls.Player.DispararSecundario.WasPressedThisFrame() && secondsCounter == 0)
        //{
        //    PonerseEscudo();
        //}

        //if (secondsCounter > 5)
        //{
        //    FinEscudo();
        //}

        if (numUsos == 0)
        {
            Reiniciar();
        }
    }
    #region InputSystem

    private void OnEnable()
    {
        if (controlDelJugador.idPlayer == 1)
        {
            controlDelJugador.playerControls.Player.DispararSecundario.performed += GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.DispararSecundario.performed += GetDispararInput;
        }
    }

    private void OnDisable()
    {
        if (controlDelJugador.idPlayer == 1)
        {
            controlDelJugador.playerControls.Player.DispararSecundario.performed -= GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.DispararSecundario.performed -= GetDispararInput;
        }
    }

    private void GetDispararInput(InputAction.CallbackContext context)
    {
        if (context.performed && secondsCounter == 0)
        {
            PonerseEscudo();
        }
    }
    #endregion
    //private void Inicializar(){
    //    gameObject.SetActive(true);
    //    numUsos = 1;
    //    interactuable = true;
    //}
    private void Reiniciar()
    {
        //Llamar al jugador y quitarle el arma secundaria
        controlDelJugador.SoltarArma(false);

        gameObject.SetActive(false);
    }

    private void PonerseEscudo()
    {
        PrefabEscudo.SetActive(true);
        Debug.Log(gameObject.transform.parent.name);
        colisionJugador.enabled = false;
        secondsCounter = 0;
    }

    private void FinEscudo()
    {
        numUsos = 0;
        PrefabEscudo.SetActive(false);
        colisionJugador.enabled = true;
    }
}

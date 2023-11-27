using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarArmadura : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    public GameObject SpriteEscudo;
    [SerializeField] private Collider2D colisionJugador;
    //public float secondsCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarArmadura";
    }


    // Update is called once per frame
    void Update()
    {
        //contador de segundos activos
        //secondsCounter += Time.deltaTime;

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
        if (context.performed /*&& secondsCounter == 0*/)
        {
            //PonerseEscudo();
            StartCoroutine(Escudo(3));
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
        SpriteEscudo.SetActive(false);
        controlDelJugador.setAmadura(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        controlDelJugador.SoltarArma(false);


        //gameObject.SetActive(false);
    }

    private void PonerseEscudo()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        SpriteEscudo.SetActive(true);
        //Debug.Log(gameObject.transform.parent.name);
        //colisionJugador.enabled = false;
        controlDelJugador.setAmadura(true);
        //secondsCounter = 0;
    }
    IEnumerator Escudo(float tiempo)
    {
        PonerseEscudo();
        yield return new  WaitForSeconds(tiempo);
        Reiniciar();

    }
}

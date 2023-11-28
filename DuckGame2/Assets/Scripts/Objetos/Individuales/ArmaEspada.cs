using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmaEspada : Objeto
{
    [Header("Espada")]//Para selecionar la skin de la espada. Se puede hacer una array para añadir diferentes tipos de espadas.
    [SerializeField] Sprite Aspecto;

    [Header("Variables de golpeo")]
    [SerializeField] float cadenciaGolpe = 0.5f;//Cuantos golpes x segundos podemos dar.


    [Header("Ajustes detector de colisiones")]
    [SerializeField] GameObject detectorColision;//Encargado de detectar los objetos atacables.
    [SerializeField] bool EnRango;//Comprueba que exista un objeto atacable en rango, al cual se le aplicara el daño del arma. No hacer caso al warning de momento.

    [Header("Animaciones")]//Parametro;  HeAtacado para cambiar la animacion de AtaqueEspada a IdleEspada y controlar la variable cadenciaGolpe.
    [SerializeField] Animator animEspada;
    [SerializeField] bool puedeAtacar; // Variable para controlar si puede atacar
    [Header("Control")]
    [SerializeField] ControlJugador controlDelJugador;

    #region START/UPDATE
    private void Start()
    {
        //TODO: Utilizar parametros de la clase objeto (padre)
        //numUsos = durabilidad;
        animator = animEspada;
        puedeAtacar = true;
        Application.targetFrameRate = 60;
        //gameObject.SetActive(false);//Activar en otro script al cogerlo ya que este estará desactivado de inicio.

    }
    private void Update()
    {
        Municion();
        //Atacar();
        //EspadaManager();
    }
    #endregion
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
        if (context.performed)
        {
            Atacar();
        }
    }
    #endregion
    void Atacar()
    {
        //if (puedeAtacar && Input.GetKeyDown("Fire1") && durabilidad > 0)
        if (puedeAtacar && numUsos > 0)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(detectorColision.transform.position, detectorColision.GetComponent<BoxCollider2D>().size, 1f);//Compruebo que exista un collider en contacto con tag player.
            foreach (Collider2D collider in colliders)
            {
                Debug.Log(collider.gameObject.tag);
                if (collider.CompareTag("Player") && EnRango)
                {
                    if (collider.GetComponent<ControlJugador>().idPlayer != controlDelJugador.idPlayer)
                    {
                        //Debug.Log("¡Ataque realizado!");
                        collider.GetComponent<ControlJugador>().RecibirDanyo();
                        collider.GetComponent<Rigidbody2D>().AddTorque(5f, ForceMode2D.Force);
                    }
                    // Manejar la lógica de ataque y posible destrucción aquí
                    // collider.GetComponent<Player>().RecibirGolpe();
                }
            }
            animEspada.SetBool("PuedoAtacar", true);//Esto es para el funcionamiento de la animacion.
            StartCoroutine(ControlarCadencia());
        }
    }

    //void EspadaManager() //Resetear arma si nos quedamos a 0 de durabilidad y desactivarla.
    //{
    //    if (durabilidad <= 0)
    //    {
    //        gameObject.SetActive(false);
    //        durabilidad = 3;
    //    }
    //}

    IEnumerator ControlarCadencia()
    {
        puedeAtacar = false; // Desactivar la capacidad de atacar.
        yield return new WaitForSeconds(cadenciaGolpe); // Esperar el tiempo de cadencia.
        animEspada.SetBool("PuedoAtacar", false);//Esto es para el funcionamiento de la animacion.
        animEspada.SetBool("HeAtacado", true);//Esto es para el funcionamiento de la animacion. Este se puede ver de quitarlo.
        puedeAtacar = true; // Reactivar la capacidad de atacar.
        numUsos--;//Le quitamos un punto de durabilidad.



    }
    private void Municion()
    {
        if (numUsos <= 0)
        {
            controlDelJugador.SoltarArma(true);
        }
    }
    #region TRIGGERS
    //COMPROBAMOS QUE ESTÉ EN RANGO.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnRango = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EnRango = false;
        }
    }
    #endregion
}






























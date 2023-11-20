using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmaEspada : Objeto
{
    [Header("Espada")]//Para selecionar la skin de la espada. Se puede hacer una array para añadir diferentes tipos de espadas.
    [SerializeField] Sprite Aspecto;

    [Header("Variables de golpeo")]
    [SerializeField] float cadenciaGolpe = 0.5f;//Cuantos golpes x segundos podemos dar.

    [Header("Ajustes detector de colisiones")]
    [SerializeField] GameObject detectorColision;//Encargado de detectar los objetos atacables.
    [SerializeField] bool EnRango;//Comprueba que exista un objeto atacable en rango, al cual se le aplicara el daño del arma.
    [SerializeField] bool Atacando;//Saber si se está realizando el ataque.


    [Header("Animaciones")]//Parametro;  HeAtacado para cambiar la animacion de AtaqueEspada a IdleEspada y controlar la variable cadenciaGolpe.
    [SerializeField] Animator animEspada;
    [SerializeField] bool puedeAtacar = true; // Variable para controlar si puede atacar



    private void Start()
    {
        animEspada = GetComponent<Animator>();
        Application.targetFrameRate = 60;
    }
    private void Update()
    {
        Atacar();
    }

    void Atacar() 
    {
        if (puedeAtacar && Input.GetKeyDown(KeyCode.LeftControl))
        {
            animEspada.SetBool("PuedoAtacar", true);
            StartCoroutine(ControlarCadencia());
        }
    }

    IEnumerator ControlarCadencia()
    {
        puedeAtacar = false; // Desactivar la capacidad de atacar
        Atacando = true;
        yield return new WaitForSeconds(cadenciaGolpe); // Esperar el tiempo de cadencia
        animEspada.SetBool("PuedoAtacar", false);
        animEspada.SetBool("HeAtacado", true);
        puedeAtacar = true; // Reactivar la capacidad de atacar
        Atacando = false;
    }

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //aqui controlo el detectorColision para saber si tengo una colision.
        //si detectorColision colisiona con un tag player , destruye el tag player) 
        Debug.Log(collision.name);
        if (collision.tag == "Player" && EnRango && Atacando) // Comprobar si el objeto tiene el tag "player"
        {
            Debug.Log("Lo he matado");
            Destroy(collision.gameObject); // Destruir el objeto
            //EVENTO DE MATAR PLAYER.

        }
    }

}































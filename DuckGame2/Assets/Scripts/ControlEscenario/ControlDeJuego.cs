using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDeJuego : MonoBehaviour
{
    //public List<GameObject> jugadores;
    public bool finDeRonda;
    public int numRonda;
    private int[] arrayEscenas = { 1, 2, 3 };
    private int escenaActual;
    public static ControlDeJuego instancia;
    public MovimientoTelon telon;
    public bool finDePartida;
    public bool aux;

    //Contol para habilitar el movimiento de los jugadores
    [SerializeField] private List<GameObject> listaJugadores;

    void Awake()
    {
        
        // Verificar si ya existe una instancia del script
        if (instancia == null)
        {
            // Si no hay instancia, asignar esta instancia como la instancia �nica
            
            instancia = this;

            // Evitar que el objeto se destruya al cargar una nueva escena
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruir este objeto para mantener solo una instancia
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        telon = FindFirstObjectByType<MovimientoTelon>();

        numRonda = 1;
        escenaActual = 1;
        //SceneManager.LoadScene(escenaActual); // este sera el que se usar� cuando tengamos las escenas en el build settings
        finDeRonda = false;
        finDePartida = false;
        listaJugadores.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        FindFirstObjectByType<UIController>().setNumRondas(numRonda);



    }
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.M)) //esto es solo para probar que vaya el reinicio de escena 
        //{
        //    finDeRonda = true;
        //}
        // Verificar si ha terminado la ronda

        if (finDeRonda && telon != null && aux == true)
        {
            // Obtener una escena aleatoria diferente a la predeterminada
            escenaActual = ObtenerEscenaAleatoria();

            StartCoroutine(ReinicioNivel());
            //Debug.Log("Heeeeeeeeeeee");
            
        }
        if (telon == null)
        {
            //telon = GameObject.FindObjectOfType<MovimientoTelon>();
            telon = FindFirstObjectByType<MovimientoTelon>();
        }
        if (listaJugadores.Count<=0 && !aux)
        {
            listaJugadores.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            FindFirstObjectByType<UIController>().setNumRondas(numRonda);

            //Debug.Log("Hiiiiiiiiiiiiiiii");

        }
        if (listaJugadores != null&& telon != null &&!finDeRonda)
        {
            foreach (GameObject gO in listaJugadores)
            {
                if (gO != null)
                {
                    gO.GetComponent<ControlJugador>().sePuedeMover = telon.telonAbierto;
                    if (!finDeRonda)
                    {
                        if (gO.GetComponent<ControlJugador>().vida <= 0 && !finDeRonda)
                        {
                            finDeRonda = true;
                            aux = true;

                        }

                        //Debug.Log("SoloConVida");
                    }
                    Debug.Log(gO.GetComponent<ControlJugador>().idPlayer);
                }
                else
                {
                    listaJugadores = new List<GameObject>();
                    break;
                }

            }
        }
        



    }
    int ObtenerEscenaAleatoria()
    {
        int nuevaEscena = escenaActual;

        while (nuevaEscena == escenaActual)
        {
            nuevaEscena = arrayEscenas[Random.Range(0, arrayEscenas.Length)];
        }

        return nuevaEscena;
    }


    IEnumerator ReinicioNivel()
    {
        aux = false;

        //finDeRonda = false;
        //listaJugadores.Clear();
        // Resetear la ronda

        // Parar o camara lenta al juego puede estar guay si agregamos un zoom al ganador y una animacion de celebracion
        //Time.timeScale = 0.7f;

        // Aumentar el n�mero de rondas
        numRonda++;

        // Si agregamos animaci�n de victria esto nos puede servir
        //float tiempoInicial = 0f;
        //float tiempoEspera = 3f;
        //while (tiempoInicial <= tiempoEspera)
        //{
        //    GameObject jugadorganador = jugadores[0]; //Seleccionas el ganador
        //    //Aqui se podr�a agregar el codigo para que haga la celebracion
        //    tiempoInicial += Time.unscaledDeltaTime; // Tiempo no afectado por Time.timeScale
        //    yield return null;
        //}
        while (telon.telonAbierto)
        {
            //    Debug.Log("while");
            //GameObject jugadorganador = jugadores[0]; //Seleccionas el ganador
            //Aqui se podr�a agregar el codigo para que haga la celebracion
            telon.CerrarTelon();
            yield return null;
        }


        //Debug.Log("antes");
        // Esperar 3 segundos

        //Debug.Log("despues");
        //Debug.Log(numRonda);
        // Verificar si es la quinta ronda
        if (numRonda == 6)
        {
            Debug.Log("�Vuelta al men�!");
            finDePartida = true;
            // Agregar aqu� la l�gica para regresar al menu principal o una llamada al void q lo haga
        }
        else
        {
            // Cambiar a la nueva escena esto seria lo que pondriamos cuando tengamos las escenas
            SceneManager.LoadScene(0);
            finDeRonda = false;

            Debug.Log("esc" + escenaActual);
        }

    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    // Estructura a
    [Serializable]
    public class EstructuraDicResultados
    {
        private int idJugador;
        private int victorias;
        public EstructuraDicResultados(int idJugador, int victorias)
        {
            this.idJugador = idJugador;
            this.victorias = victorias;
        }
        public int getIdJugador()
        {
            return idJugador;
        }
        public void setVictorias(int victorias)
        {
            this.victorias += victorias;
        }
        public int getVictorias()
        {
            return victorias;
        }
        public string ToString()
        {
            return "EstructuraDicResultados: [ idJugador: " + idJugador + ", victorias: " + victorias + "]";
        }
    }
    [Header("Diccionario sobre los resultads")]
    [SerializeField] private Dictionary<int, EstructuraDicResultados> dicResultados;


    void Awake()
    {

        // Verificar si ya existe una instancia del script
        if (instancia == null)
        {
            // Si no hay instancia, asignar esta instancia como la instancia única

            instancia = this;

            // Evitar que el objeto se destruya al cargar una nueva escena
            if (!finDePartida)
            {
                DontDestroyOnLoad(gameObject);
            }
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
        //SceneManager.LoadScene(escenaActual); // este sera el que se usará cuando tengamos las escenas en el build settings
        finDeRonda = false;
        finDePartida = false;
        listaJugadores.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        //NOTE: Diccionario de control de victorias
        dicResultados = new Dictionary<int, EstructuraDicResultados>();
        inicializaDiccionario();

        FindFirstObjectByType<UIController>().setNumRondas(numRonda);
        //Application.targetFrameRate = 60;



    }
    void Update()
    {
        if (!finDePartida)
        {
            EnPartida();
        }
        else
        {
            EscenaVictria();
        }

    }
    private void EnPartida()
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
        if (listaJugadores.Count <= 0 && !aux)
        {
            listaJugadores.AddRange(GameObject.FindGameObjectsWithTag("Player"));
            FindFirstObjectByType<UIController>().setNumRondas(numRonda);

            //Debug.Log("Hiiiiiiiiiiiiiiii");

        }
        if (listaJugadores != null && telon != null && !finDeRonda)
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
                    //Debug.Log(gO.GetComponent<ControlJugador>().idPlayer);
                }
                else
                {
                    listaJugadores = new List<GameObject>();
                    break;
                }

            }
        }
    }
    private void EscenaVictria()
    {
        if (telon == null)
        {
            //telon = GameObject.FindObjectOfType<MovimientoTelon>();
            telon = FindFirstObjectByType<MovimientoTelon>();
            FindFirstObjectByType<UIController>().PanelDeVictoria(revisarSiHaGanadoYQuien());
            FindFirstObjectByType<UIController>().deshabilitarNumRondas();
        }

        if (telon.GetComponent<MovimientoTelon>().telonAbierto && !aux)
        {
            StartCoroutine(EscenaVictoria());

        }
        

        if (listaJugadores != null && telon != null)
        {
            foreach (GameObject gO in listaJugadores)
            {
                if (gO != null)
                {

                    if (gO.GetComponent<ControlJugador>().idPlayer == revisarSiHaGanadoYQuien() && !aux)
                    {
                        gO.GetComponent<ControlJugador>().sePuedeMover = true;
                    }
                    else if (gO.GetComponent<ControlJugador>().idPlayer != revisarSiHaGanadoYQuien())
                    {
                        gO.SetActive(false);
                    }

                }
                else
                {
                    listaJugadores = new List<GameObject>();
                    listaJugadores.AddRange(GameObject.FindGameObjectsWithTag("Player"));
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
        //NOTE: Revisa cual es el jugador que tiene vida y le da un punto de victoria
        foreach (GameObject gameObject in listaJugadores)
        {
            if (gameObject.GetComponent<ControlJugador>().vida > 0)
            {
                actualizarResultados(gameObject.GetComponent<ControlJugador>().idPlayer, 1);
            }
        }
        //finDeRonda = false;
        //listaJugadores.Clear();
        // Resetear la ronda

        // Parar o camara lenta al juego puede estar guay si agregamos un zoom al ganador y una animacion de celebracion
        //Time.timeScale = 0.7f;

        // Aumentar el número de rondas
        numRonda++;

        // Si agregamos animación de victria esto nos puede servir
        //float tiempoInicial = 0f;
        //float tiempoEspera = 3f;
        //while (tiempoInicial <= tiempoEspera)
        //{
        //    GameObject jugadorganador = jugadores[0]; //Seleccionas el ganador
        //    //Aqui se podría agregar el codigo para que haga la celebracion
        //    tiempoInicial += Time.unscaledDeltaTime; // Tiempo no afectado por Time.timeScale
        //    yield return null;
        //}
        while (telon.telonAbierto)
        {
            //    Debug.Log("while");
            //GameObject jugadorganador = jugadores[0]; //Seleccionas el ganador
            //Aqui se podría agregar el codigo para que haga la celebracion
            telon.CerrarTelon();
            yield return null;
        }


        //Debug.Log("antes");
        // Esperar 3 segundos

        //Debug.Log("despues");
        //Debug.Log(numRonda);


        // Verificar si es la quinta ronda
        //if (numRonda == 6)
        //{
        //    Debug.Log("¡Vuelta al menú!");
        //    SceneManager.LoadScene(0);
        //    finDePartida = true;
        //    // Agregar aquí la lógica para regresar al menu principal o una llamada al void q lo haga
        //}
        //else
        //{
        //    // Cambiar a la nueva escena esto seria lo que pondriamos cuando tengamos las escenas
        //    SceneManager.LoadScene(ObtenerEscenaAleatoria());
        //    finDeRonda = false;

        //    //Debug.Log("esc" + escenaActual);
        //}
        if (revisarSiHaGanadoYQuien() == 0)
        {
            // Cambiar a la nueva escena esto seria lo que pondriamos cuando tengamos las escenas
            SceneManager.LoadScene(ObtenerEscenaAleatoria());
            finDeRonda = false;

            //Debug.Log("esc" + escenaActual);
        }
        else
        {
            Debug.Log("El jugador :" + revisarSiHaGanadoYQuien() + " ha ganado");
            //Debug.Log("¡Vuelta al menú!");
            //Carga la escena de victoria
            SceneManager.LoadScene(arrayEscenas.Length + 1);
            finDePartida = true;
        }

    }
    IEnumerator EscenaVictoria()
    {
        aux = true;
        yield return new WaitForSeconds(10);
        while (telon.telonAbierto)
        {
            //    Debug.Log("while");
            //GameObject jugadorganador = jugadores[0]; //Seleccionas el ganador
            //Aqui se podría agregar el codigo para que haga la celebracion
            telon.CerrarTelon();
            yield return null;
        }
        Debug.Log("¡Vuelta al menú!");
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    private void inicializaDiccionario()
    {
        foreach (GameObject jugador in listaJugadores)
        {
            Debug.Log(jugador.GetComponent<ControlJugador>().idPlayer);
            dicResultados.Add(jugador.GetComponent<ControlJugador>().idPlayer,
                new EstructuraDicResultados(jugador.GetComponent<ControlJugador>().idPlayer, 0));

        }
        foreach (var jugador in dicResultados)
        {
            Debug.Log(jugador.Key + " : " + jugador.Value.ToString());
        }
    }
    private void actualizarResultados(int idJugador, int puntosDeVictoria)
    {
        dicResultados[idJugador].setVictorias(puntosDeVictoria);
    }
    private int revisarSiHaGanadoYQuien()
    {
        foreach (var jugador in dicResultados)
        {
            Debug.Log(jugador.Key + " : " + jugador.Value.ToString());
            if (jugador.Value.getVictorias() >= 3)
            {
                return jugador.Value.getIdJugador();
            }
        }
        return 0;
    }
}
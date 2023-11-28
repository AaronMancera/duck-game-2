using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlDeJuego : MonoBehaviour
{
    public List<GameObject> jugadores;
    public bool finDeRonda;
    public int numRonda;
    private int[] arrayEscenas = { 1, 2, 3 };
    private int escenaActual;
    private static ControlDeJuego instancia;
    private bool reiniciando;
    [SerializeField] private TMP_Text rondas;
    void Awake()
    {
        // Verificar si ya existe una instancia del script
        if (instancia == null)
        {
            // Si no hay instancia, asignar esta instancia como la instancia única
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
        numRonda = 1;
        escenaActual = 1;
        //SceneManager.LoadScene(escenaActual); // este sera el que se usará cuando tengamos las escenas en el build settings
        finDeRonda = false;
        reiniciando = false;
        rondas.text = numRonda+"";
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M)) //esto es solo para probar que vaya el reinicio de escena 
        foreach (GameObject gO in jugadores)
        {
            if (gO.GetComponent<ControlJugador>().vida <= 0 && reiniciando!=true) //esto es solo para probar que vaya el reinicio de escena 
            {
                finDeRonda = true; break;
            }
        }

        // Verificar si ha terminado la ronda
        if (finDeRonda)
        {
            reiniciando = true;
            // Obtener una escena aleatoria diferente a la predeterminada
            escenaActual = ObtenerEscenaAleatoria();

            StartCoroutine(ReinicioNivel());
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
        // Resetear la ronda
        finDeRonda = false;

        // Parar o camara lenta al juego puede estar guay si agregamos un zoom al ganador y una animacion de celebracion
        //Time.timeScale = 0.2f;
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


        //Debug.Log("antes");
        // Esperar 5 segundos
        yield return new WaitForSeconds(5f);
        //Debug.Log("despues");
        Debug.Log(numRonda);

        // Verificar si es la quinta ronda
        if (numRonda == 6)
        {
            Debug.Log("¡Vuelta al menú!");
            // Agregar aquí la lógica para regresar al menu principal o una llamada al void q lo haga
        }
        else
        {
            // Restaurar la escala de tiempo para reanudar el juego si ponemos camara lenta
            Time.timeScale = 1f;
            // Cambiar a la nueva escena esto seria lo que pondriamos cuando tengamos las escenas
            //SceneManager.LoadScene(escenaActual);
            Debug.Log("esc" + escenaActual);


        }


    }
}

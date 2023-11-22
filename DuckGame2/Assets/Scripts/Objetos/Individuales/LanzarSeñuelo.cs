using System.Collections;
using UnityEngine;

public class LanzarSeñuelo : MonoBehaviour
{
    #region Variables del Inspector

    [Header("Configuración del Señuelo")]
    [SerializeField] private GameObject señueloPrefab;
    [SerializeField] private float tiempoVidaSeñuelo = 3f;
    [SerializeField] private GameObject player;

    [Header("Configuración del Desplazamiento")]
    [SerializeField] private float fuerzaDesplazamiento = 1000f;
    [SerializeField] private float distanciaMaxima = 1.3f;
    [SerializeField] private float tiempoFrenado = 0.5f;

    [Header("Configuración de Usos")]
    [SerializeField] private int usosMaximos = 1;

    #endregion

    #region Variables Internas

    private Rigidbody2D jugadorRb;
    private Vector2 posicionInicial;
    private bool detenerDesplazamiento = false;
    private GameObject señueloActual;
    private int usosRestantes;
    private float ultimaDireccion = 1f;

    #endregion

    #region Funciones de Unity

    void Start()
    {
        // Inicializar el Rigidbody del jugador
        jugadorRb = player.GetComponent<Rigidbody2D>();

        // Inicializar el contador de usos
        usosRestantes = usosMaximos;
    }

    void Update()
    {
        // Lanzar el señuelo cuando se presiona la tecla y hay usos disponibles
        if (Input.GetKeyDown(KeyCode.Space) && usosRestantes > 0)
        {
            // Almacenar la última dirección antes de lanzar el señuelo
            ultimaDireccion = Mathf.Sign(Input.GetAxis("Horizontal"));

            Lanzar();
        }
    }

    void FixedUpdate()
    {
        // Detener el jugador cuando alcanza la distancia máxima
        if (detenerDesplazamiento)
        {
            float distanciaActual = Mathf.Abs(player.transform.position.x - posicionInicial.x);

            if (distanciaActual >= distanciaMaxima)
            {
                // Detener completamente el jugador
                jugadorRb.velocity = Vector2.zero;
                detenerDesplazamiento = false;
                Debug.Log("Jugador detenido en la distancia máxima");

                // Iniciar la corutina para eliminar el señuelo después de un tiempo
                StartCoroutine(EliminarSeñuelo());
            }
        }
    }

    #endregion

    #region Funciones Personalizadas

    void Lanzar()
    {
        // Almacenar la posición inicial del jugador
        posicionInicial = player.transform.position;

        // Instanciar el señuelo en la posición del jugador
        señueloActual = Instantiate(señueloPrefab, player.transform.position, Quaternion.identity);

        // Aplicar una fuerza en la dirección opuesta a la última dirección de movimiento
        float direccion = -ultimaDireccion;
        jugadorRb.AddForce(new Vector2(direccion * fuerzaDesplazamiento, 0));

        // Habilitar el flag para detener el desplazamiento
        detenerDesplazamiento = true;

        // Reducir el contador de usos
        usosRestantes--;

        Debug.Log("Se lanzó el señuelo");
    }

    IEnumerator EliminarSeñuelo()
    {
        // Esperar el tiempo de vida del señuelo antes de eliminarlo
        yield return new WaitForSeconds(tiempoVidaSeñuelo);

        // Destruir el señuelo
        Destroy(señueloActual);

        // Reiniciar usos si es necesario (opcional)
        if (usosRestantes == 0)
        {

            usosRestantes = usosMaximos;
            gameObject.SetActive(false);
        }
    }

    #endregion
}

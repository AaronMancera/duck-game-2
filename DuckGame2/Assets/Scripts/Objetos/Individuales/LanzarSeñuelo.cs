using System.Collections;
using UnityEngine;

public class LanzarSeñuelo : Objeto
{
    #region Variables del Inspector

    [Header("Configuración del Señuelo")]
    [SerializeField] private GameObject señueloPrefab;
    [SerializeField] private float tiempoVidaSeñuelo = 1f;
    [SerializeField] private GameObject player;

    [Header("Configuración del Desplazamiento")]
    [SerializeField] private float fuerzaDesplazamiento = 1000f;
    [SerializeField] private float distanciaMaxima = 1.3f;
    [SerializeField] private float tiempoFrenado = 0.5f;

    [Header("Configuración de Dirección")]
    [SerializeField] private bool mirandoALaIzquierda = false;

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
        usosRestantes = numUsos;
    }

    void Update()
    {
        UsarSeñuelo();
    }

    void FixedUpdate()
    {
        DeslizamientoPlayer();
    }

    #endregion

    #region Funciones Personalizadas

    void UsarSeñuelo()
    {
        // Obtener la dirección del movimiento horizontal del jugador
        float direccionHorizontal = Input.GetAxis("Horizontal");

        // Actualizar la dirección del jugador solo si está en movimiento
        if (direccionHorizontal != 0)
        {
            ultimaDireccion = Mathf.Sign(direccionHorizontal);
            mirandoALaIzquierda = ultimaDireccion < 0;
        }

        // Lanzar el señuelo cuando se presiona la tecla y hay usos disponibles
        if (Input.GetKeyDown(KeyCode.Backspace) && usosRestantes > 0)
        {
            Lanzar();
        }
    }

    void Lanzar()
    {
        // Almacenar la posición inicial del jugador
        posicionInicial = player.transform.position;

        // Calcular la dirección del dash
        float direccionDash = mirandoALaIzquierda ? -1f : 1f;

        // Calcular la posición de inicio del señuelo en función de la dirección del dash
        Vector2 posicionInicioSeñuelo = (Vector2)player.transform.position + new Vector2(direccionDash * distanciaMaxima, 0f);

        // Instanciar el señuelo en la nueva posición calculada
        señueloActual = Instantiate(señueloPrefab, posicionInicioSeñuelo, Quaternion.identity);

        // Aplicar una fuerza en la dirección opuesta al dash para el jugador
        float direccionFuerza = -direccionDash;
        jugadorRb.AddForce(new Vector2(direccionFuerza * fuerzaDesplazamiento, 0));

        // Reducir el contador de usos
        usosRestantes--;

        // Desactivar el objeto señuelo después de un tiempo
        StartCoroutine(EliminarSeñuelo());

        //DESACTIVAR EL LANZARSEÑUELO
        Debug.Log("Se lanzó el señuelo");
    }

    void DeslizamientoPlayer()
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

                if (señueloActual != null) // Añadir esta condición
                {
                    // Iniciar la corutina para eliminar el señuelo después de un tiempo
                    StartCoroutine(EliminarSeñuelo());
                }
            }
        }
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
            usosRestantes = numUsos;
        }

        // Reactivar el objeto principal
        gameObject.SetActive(true);
    }

    #endregion
}
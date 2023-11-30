using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarCaja : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    //[Header("Atributos")]
    //[SerializeField] private GameObject caja;
    //[SerializeField] private Transform puntoDeLanzar;
    [Header("Configuración de Caja")]
    [SerializeField] private GameObject cajaPrefab;
    [SerializeField] private float fuerzaLanzamiento;
    [SerializeField] private float anguloInclinacion = 30f;
    private bool mirandoALaIzquierda;

    [SerializeField] AudioClip lanzarCajaSFX;


    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarCaja";
        //Application.targetFrameRate = 60;
    }
    void Update()
    {
        ActualizarDireccion();
        Municion();
    }
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
            controlDelJugador.playerControls.Player.DispararPrincipal.performed -= GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.DispararPrincipal.performed -= GetDispararInput;
        }
    }
    private void GetDispararInput(InputAction.CallbackContext context)
    {
        if (context.performed && numUsos > 0)
        {
            Lanzar();
        }
    }
    void ActualizarDireccion()
    {
        //float direccionHorizontal = Input.GetAxis("Horizontal");
        float direccionHorizontal = transform.parent.transform.localScale.x;

        if (direccionHorizontal != 0)
        {
            mirandoALaIzquierda = direccionHorizontal < 0;
        }
    }

    void Lanzar()
    {
        AudioManager.instanceAudioManager.PlaySFX(lanzarCajaSFX);


        numUsos--;
        Vector2 lugarDeInstanciamiento = transform.position;
        lugarDeInstanciamiento.y += 1;
        if (mirandoALaIzquierda)
        {
            lugarDeInstanciamiento.x -= 2;
        }
        else
        {
            lugarDeInstanciamiento.x += 2;

        }
        GameObject nuevaCaja = Instantiate(cajaPrefab, lugarDeInstanciamiento, Quaternion.identity);
        Rigidbody2D rbCaja = nuevaCaja.GetComponent<Rigidbody2D>();

        // Obtener la dirección de lanzamiento basada en la última dirección registrada
        float direccionLanzamiento = mirandoALaIzquierda ? -1f : 1f;

        // Calcular el ángulo de inclinación en radianes
        float anguloRad = Mathf.Deg2Rad * anguloInclinacion;

        // Calcular los componentes X e Y de la fuerza inicial con el ángulo de inclinación
        float fuerzaX = fuerzaLanzamiento * direccionLanzamiento * Mathf.Cos(anguloRad);
        float fuerzaY = fuerzaLanzamiento * Mathf.Sin(anguloRad);

        // Aplicar fuerza inicial al lanzar la caja
        Vector2 fuerzaInicial = new Vector2(fuerzaX, fuerzaY);
        rbCaja.AddForce(fuerzaInicial, ForceMode2D.Impulse);
    }
    private void Municion()
    {
        if (numUsos <= 0)
        {
            //Llamar al jugador y quitarle el arma secundaria
            controlDelJugador.SoltarArma(false);
        }
    }
}

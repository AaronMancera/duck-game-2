using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzarCaja : Objeto
{
    [SerializeField] ControlJugador controlDelJugador;
    [Header("Atributos")]
    [SerializeField] private GameObject caja;
    [SerializeField] private Transform puntoDeLanzar;

    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarCaja";
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        if (controlDelJugador.idPlayer == 1)
        {
            controlDelJugador.playerControls.Player.DispararPrincipal.performed += GetDispararInput;

        }
        else if (controlDelJugador.idPlayer == 2)
        {
            controlDelJugador.playerControls.PlayerP2.Saltar.performed += GetDispararInput;
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
            controlDelJugador.playerControls.PlayerP2.Saltar.performed -= GetDispararInput;
        }
    }
    private void GetDispararInput(InputAction.CallbackContext context)
    {
        if (context.performed && numUsos > 0)
        {
            Disparar();
        }
    }

    void Update()
    {
        gameObject.SetActive(Municion());
    }


    // Dispara la bomba

    private void Disparar()
    {
        CajaPrefabScript componenteBomba = caja.gameObject.GetComponent<CajaPrefabScript>();

        Instantiate(componenteBomba, puntoDeLanzar.transform.position, caja.gameObject.transform.rotation);

        numUsos--;
    }

    private bool Municion()
    {
        if (numUsos <= 0)
        {
            //Llamar al jugador y quitarle el arma secundaria
            controlDelJugador.SoltarArma(false);
            return false;
        }
        return true;
    }
}

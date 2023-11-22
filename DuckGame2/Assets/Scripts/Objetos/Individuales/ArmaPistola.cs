using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPistola : Objeto
{
    [SerializeField] private GameObject BalaPrefab;
    private float secondsCounter = 0;
    private bool puedeDispara;
    // Start is called before the first frame update
    void Start()
    {
        ////TESTING
        //Inicializar();
        //Application.targetFrameRate = 60;
        nombre = "armaPistola";
    }

    /// <summary>
    /// La pistola dispara y tiene un retardo de 0.5 segundos para poder disparar de nuevo
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        //contador de segundos para el cooldown
        secondsCounter += Time.deltaTime;
        //Debug.Log(secondsCounter);

        //if (Input.GetKeyDown(KeyCode.Space) && puedeDispara == true && numUsos > 0)
        if (InputManager.playerControls.Player.DispararPrincipal.enabled && puedeDispara == true && numUsos > 0)
        {
            Disparar();
        }

        //retardo de disparo
        if (secondsCounter > 0.5)
        {
            puedeDispara = true;
        }
        //NOTE: Si se queda sin balas se desactiva el objeto
        if (numUsos == 0)
        {
            SinMunicion();
        }

    }
    /// <summary>
    /// Método que reinicia el arma cuando un jugador la obtenga
    /// </summary>
    //private void Inicializar()
    //{
    //    gameObject.SetActive(true);
    //    numUsos = 2; //Numero de balas
    //    interactuable = true;
    //}

    public void Disparar()
    {
        secondsCounter = 0;
        numUsos = numUsos - 1;
        puedeDispara = false;
        Instantiate(BalaPrefab, transform.position, gameObject.transform.rotation);
    }

    public void SinMunicion()
    {
        gameObject.SetActive(false);
    }
}

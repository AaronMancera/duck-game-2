using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ArmaRayo : Objeto
{
    [Header("Control")]
    [SerializeField] ControlJugador controlDelJugador;

    [Header("Prefab con line renderer")]
    [SerializeField] private LineRenderer lineaDeDisparo;
    private LineRenderer lineaDeDisparoActual;
    [Header("Velocidad para limpiar")]
    [SerializeField] private float velocidadDeLimpiezaAsignada;
    private List<Vector3> puntosDelTrail = new List<Vector3>();
    private bool pulsarFire1;
    //private bool mantenerFire1;
    [Header("Tiempo de carga")]
    [SerializeField] private float tiempoDeCargaMaximo;
    [SerializeField] private int distanciaPuntosRayo;



    #region StartUpdate
    // Start is called before the first frame update
    void Start()
    {
        //TESTING
        //numUsos = 50;
        //interactuable = true;
        nombre = "ArmaRayo";
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //NOTE: Refactorizacion para el InputManager
        //pulsarFire1 = Input.GetButtonDown("Fire1");
        //pulsarFire1 = controlDelJugador.playerControls.Player.DispararPrincipal.WasPressedThisFrame(); //GetButtonDown
        //Debug.Log(controlDelJugador.idPlayer);
        switch (controlDelJugador.idPlayer)
        {
            case 1:
                pulsarFire1 = controlDelJugador.playerControls.Player.DispararPrincipal.WasPressedThisFrame();
                //Debug.Log(controlDelJugador.playerControls.Player.DispararPrincipal.WasPressedThisFrame());
                break;
            case 2:
                pulsarFire1 = controlDelJugador.playerControls.PlayerP2.DispararPrincipal.WasPressedThisFrame();
                //Debug.Log(controlDelJugador.playerControls.PlayerP2.DispararPrincipal.WasPressedThisFrame());

                break;
           
               

        }
        ////mantenerFire1 = Input.GetButton("Fire1");
        //mantenerFire1 = controlDelJugador.playerControls.Player.DispararPrincipal.IsPressed(); //GetButton
        if (pulsarFire1 && numUsos == 1)
        {
            //Animacion to guapa
            StartCoroutine(DispararRayoInstantaneo(distanciaPuntosRayo));
            numUsos = 0;
        }
        gameObject.SetActive(!SinMunicion());
        UpdatePuntosTrail();
        LimpiarPuntosTrail();
    }
    // Update is called in 0.2 seg
    private void FixedUpdate()
    {
        //Disparar();
    }
    #endregion
    #region MetodosDeGestionDeTiro
    /// <summary>
    /// Metodo que nos indicara si estamos sin municion y tenemos en cuenta si existe aun una linea de rayo
    /// </summary>
    /// <returns></returns>
    private bool SinMunicion()
    {
        if (numUsos < 0)
        {
            if (puntosDelTrail.Count <= 0)
            {
                //Llamar al jugador y quitarle el arma principal
                controlDelJugador.SoltarArma(true);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// Este metodo realizara la accion de disparar, siempre y cuando tengamos numero de usos restantes.
    /// Cuando se nos agote3 el numero de usuos, no se deshabilitara el arma hasta que se acabe la linea actual, y no podremos incrementarla
    /// </summary>
    //private void Disparar()
    //{
    //    if (!SinMunicion())
    //    {
    //        if (pulsarFire1)
    //        {
    //            DestroyTrailActual();
    //            CrearTrailActual();
    //            AddPunto();

    //        }
    //        if (mantenerFire1)
    //        {
    //            AddPunto();
    //        }
    //        else
    //        {
    //            LimpiarPuntosTrail();
    //        }
    //    }
    //    else
    //    {
    //        LimpiarPuntosTrail();
    //    }
    //    UpdatePuntosTrail();
    //}
    private IEnumerator DispararRayoInstantaneo(int distancia)
    {
        yield return new WaitForSeconds(tiempoDeCargaMaximo);
        CrearTrailActual();
        for (int i = 0; i < distancia; i++)
        {
            AddPunto();
            //Debug.Log(puntosDelTrail.Count);
        }
        numUsos--;
    }
    #endregion
    #region Generador y limpieza de puntos del rayo
    /// <summary>
    /// Se llamara para crear una nueva linea de disparo
    /// </summary>
    private void CrearTrailActual()
    {
        lineaDeDisparoActual = Instantiate(lineaDeDisparo);
        lineaDeDisparoActual.transform.localScale = GameObject.FindGameObjectWithTag("Player").gameObject.transform.localScale;
        //lineaDeDisparoActual.transform.SetParent(transform, true);
    }
    /// <summary>
    /// Se llamara para crear un nuevo punto para la linea de disparo
    /// </summary>
    private void AddPunto()
    {
        Vector2 punto = lineaDeDisparoActual != null && puntosDelTrail.Count > 0 ? puntosDelTrail.Last() : new Vector2(transform.position.x, transform.position.y);
        int aux;
        #region Para rotar la direccion
        if (transform.parent.transform.localScale.x == 1)
        {
            aux = 1;
            punto.x += 1.5f;
        }
        else
        {
            aux = -1;
            punto.x += -2f;
        }
        #endregion
        //Debug.Log(punto.ToString());
        puntosDelTrail.Add(new Vector3(punto.x + aux, punto.y, 0));
    }
    /// <summary>
    /// Se llamara para ir uniendo los puntos hasta llegar al final hasta que se no haya sufiencentes puntos o no exista el el rastro actual
    /// </summary>
    private void UpdatePuntosTrail()
    {
        if (lineaDeDisparoActual != null && puntosDelTrail.Count > 1)
        {
            lineaDeDisparoActual.positionCount = puntosDelTrail.Count;
            lineaDeDisparoActual.SetPositions(puntosDelTrail.ToArray());
        }
        else
        {
            DestroyTrailActual();
        }
    }
    /// <summary>
    /// Se llamara cuando queramos generar otra linea de corte, destruyendo la anterior y generando la nueva
    /// </summary>
    private void DestroyTrailActual()
    {
        if (lineaDeDisparoActual != null)
        {
            Destroy(lineaDeDisparoActual.gameObject);
            lineaDeDisparoActual = null;
            puntosDelTrail.Clear();
        }
    }
    /// <summary>
    /// Se llamara para ir limpiando el reastro a la velocidad determinada de la velocidadDeLimpiezaAsignada y la cantidad de puntos que existan
    /// </summary>
    private void LimpiarPuntosTrail()
    {
        float velLimpieza = velocidadDeLimpiezaAsignada;
        //NOTE: Esto evita que podamos tener una linea demasiado larga, aumentado su velocidad de limpieza dependiendo de la longuitud de la linea
        velLimpieza *= puntosDelTrail.Count / 2;
        float distanciaLimpieza = velLimpieza;

        while (puntosDelTrail.Count > 1 && distanciaLimpieza > 0)
        {
            
            float distancia = (puntosDelTrail[1] - puntosDelTrail[0]).magnitude;
            if (distanciaLimpieza > distancia)
            {
                puntosDelTrail.RemoveAt(0);
            }
            else
            {
                puntosDelTrail[0] = Vector3.Lerp(puntosDelTrail[0], puntosDelTrail[1], distanciaLimpieza / distancia);
            }
            distanciaLimpieza -= distancia;
        }
    }
    #endregion

}

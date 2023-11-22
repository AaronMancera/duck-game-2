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
    [SerializeField] private LineRenderer lineaDeDisparo;
    private LineRenderer lineaDeDisparoActual;
    [SerializeField] private float velocidadDeLimpiezaAsignada;
    private List<Vector3> puntosDelTrail = new List<Vector3>();
    private bool pulsarFire1;
    private bool mantenerFire1;

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
        pulsarFire1 = InputManager.playerControls.Player.DispararPrincipal.WasPressedThisFrame();
        ////mantenerFire1 = Input.GetButton("Fire1");
        mantenerFire1 = InputManager.playerControls.Player.DispararPrincipal.IsPressed();
    }
    // Update is called in 0.2 seg
    private void FixedUpdate()
    {

        Disparar();

    }

    /// <summary>
    /// Metodo que nos servira para recoger el arma y activarlo
    /// </summary>
    //public void Inicializador()
    //{
    //    //numUsos = 50;
    //    //interactuable = true;
    //    //gameObject.SetActive(true);
    //    setNumUsosMaximo(numUsos);
    //}
    /// <summary>
    /// Metodo que nos indicara si estamos sin municion y tenemos en cuenta si existe aun una linea de rayo
    /// </summary>
    /// <returns></returns>
    private bool SinMunicion()
    {
        if (numUsos <= 0)
        {
            if (puntosDelTrail.Count <= 0)
            {
                gameObject.SetActive(false);
            }
            return true;
        }
        return false;
    }
    /// <summary>
    /// Este metodo realizara la accion de disparar, siempre y cuando tengamos numero de usos restantes.
    /// Cuando se nos agote3 el numero de usuos, no se deshabilitara el arma hasta que se acabe la linea actual, y no podremos incrementarla
    /// </summary>
    private void Disparar()
    {
        if (!SinMunicion())
        {
            if (pulsarFire1)
            {
                DestroyTrailActual();
                CrearTrailActual();
                AddPunto();

            }
            if (mantenerFire1)
            {
                AddPunto();
            }
            else
            {
                LimpiarPuntosTrail();
            }
        }
        else
        {
            LimpiarPuntosTrail();
        }
        UpdatePuntosTrail();
    }
    /// <summary>
    /// Se llamara para crear una nueva linea de disparo
    /// </summary>
    private void CrearTrailActual()
    {
        lineaDeDisparoActual = Instantiate(lineaDeDisparo);
        lineaDeDisparoActual.transform.SetParent(transform, true);
    }
    /// <summary>
    /// Se llamara para crear un nuevo punto para la linea de disparo
    /// </summary>
    private void AddPunto()
    {
        Vector2 punto = lineaDeDisparoActual != null && puntosDelTrail.Count > 0 ? puntosDelTrail.Last() : new Vector2(transform.position.x, transform.position.y);
        int aux;
        if (transform.eulerAngles.z != 180)
        {
            aux = 1;
        }
        else
        {
            aux = -1;
        }
        //Debug.Log(punto.ToString());
        puntosDelTrail.Add(new Vector3(punto.x + aux, punto.y, 0));
        numUsos--;

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
}

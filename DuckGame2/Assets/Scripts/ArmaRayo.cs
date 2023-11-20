using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ArmaRayo : Objeto
{
    [SerializeField] private LineRenderer lineaDeDisparo;
    private LineRenderer lineaDeDisparoActual;
    [SerializeField] private float limpiezaSpeed;
    private List<Vector3> puntosDelTrail = new List<Vector3>();
    private bool pulsarFire1;
    private bool mantenerFire1;


    // Start is called before the first frame update
    void Start()
    {
        numUsos = 50;
        nombre = "ArmaRayo";
        interactuable = true;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        pulsarFire1 = Input.GetButtonDown("Fire1");
        mantenerFire1 = Input.GetButton("Fire1");
    }
    private void FixedUpdate()
    {
        Disparar();
    }
    private void Disparar()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            DestroyTrailActual();
            CrearTrailActual();
            AddPunto();
        }
        if (Input.GetButton("Fire1"))
        {
            AddPunto();
        }
        UpdatePuntosTrail();
        LimpiarPuntosTrail();
    }
    /// <summary>
    /// Se llamara para crear una nueva linea
    /// </summary>
    private void CrearTrailActual()
    {

        lineaDeDisparoActual = Instantiate(lineaDeDisparo);
        lineaDeDisparoActual.transform.SetParent(transform, true);
    }
    /// <summary>
    /// Se llamara para crear un nuevo punto para la linea
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
    }
    /// <summary>
    /// Se llamara para ir uniendo los puntos hasta llegar al final hasta que se no haya sufiencente spuntos o no exista el el rastro actual
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
    /// Se llamara cuando queramos generar otra linea de corte
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
    /// Se llamara para ir limpiando el reastro poco a poco
    /// </summary>
    private void LimpiarPuntosTrail()
    {
        float velLimpieza = limpiezaSpeed;
        //NOTE: Esto evita que podamos tener una linea demasiado larga, aumentado su velocidad de limpieza dependiendo de la longuitud de la linea
        if (puntosDelTrail.Count > 5)
        {
            velLimpieza *= puntosDelTrail.Count / 2;
        }
        else if (puntosDelTrail.Count > 10)
        {
            velLimpieza *= puntosDelTrail.Count / 1;
        }
        else
        {
            velLimpieza = limpiezaSpeed;
        }
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

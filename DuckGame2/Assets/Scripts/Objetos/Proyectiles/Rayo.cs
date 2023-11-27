using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo : MonoBehaviour
{
    
    private EdgeCollider2D edgeCollider2D;
    private LineRenderer rayo;
    // Start is called before the first frame update
    void Start()
    {
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        rayo = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetEdgeColliderALaLinea(rayo);
    }
    /// <summary>
    /// Pone el edgeCollider en tiempo real cogiendo todos los puntos de la linea y añadiendole la colision. Para ello se recoge por parametro el lineRenderer
    /// </summary>
    /// <param name="lineRenderer"></param>
    private void SetEdgeColliderALaLinea(LineRenderer lineRenderer)
    {
        List<Vector2> edge = new List<Vector2>();
        for(int point=0; point<lineRenderer.positionCount; point++)
        {
            Vector3 puntosDeLineRederer = lineRenderer.GetPosition(point);
            edge.Add(new Vector2(puntosDeLineRederer.x, puntosDeLineRederer.y));
        }
        edgeCollider2D.SetPoints(edge);
    }
    /// <summary>
    /// Pillara la colision y detectara mediante una condicion si es un jugeador 
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Jugador jugador = collision.gameObject.GetComponent<Jugador>();
        //if (jugador != null) 
        //{
        //    jugador.Morir();
        //}
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControlJugador>().RecibirDanyo();
            //killplayer
        }
    }
}

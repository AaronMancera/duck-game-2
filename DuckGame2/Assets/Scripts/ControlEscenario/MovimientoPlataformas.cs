using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataformas : MonoBehaviour
{
    public Transform[] plataformasPaArriba;
    public Transform[] plataformasPaAbajo;
    public Transform[] plataformasPaDcha;
    public Transform[] plataformasPaIzq;
    public float velocidadMovimiento = 3f;
    //private Vector3[] posicionesIniciales;
    //private int plataformaActual;
    //private bool DetectarVueltaAPosicion;
    //[SerializeField] private float limitadorMovimiento = 5f;
    //private string[] direccionPlataforma = {"izquierda","derecha","arriba","abajo" };
    //private string direccionElegida;
    //[SerializeField] private bool movimientoX ;
    //[SerializeField] private bool movimientoY ;
    //private bool plataformaEnMovimiento;
    void Start()
    {
        //Antigua Version
        //posicionesIniciales = new Vector3[plataformas.Length];

        //// Guardar las posiciones iniciales de las plataformas
        //for (int i = 0; i < plataformas.Length; i++)
        //{
        //    posicionesIniciales[i] = plataformas[i].position;
        //}
        ////plataformaEnMovimiento = false;
        //DetectarVueltaAPosicion = false;
        //// Cambiar a otra plataforma al azar 
        //plataformaActual = Random.Range(0, plataformas.Length);
        //// Elegir una dirección aleatoria del array a no ser que quieras que vaya en una direccion concreta
        //if (movimientoX && !movimientoY)
        //{
        //    direccionElegida = direccionPlataforma[Random.Range(0, 1)];
        //}
        //else if (movimientoY && !movimientoX)
        //{
        //    direccionElegida = direccionPlataforma[Random.Range(2, direccionPlataforma.Length)];
        //}
        //else
        //{
        //    direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];
        //}
    }

    void Update()
    {

            MoverPlataforma();

    }

    void MoverPlataforma() 
    {
        for (int i = 0; i < plataformasPaArriba.Length; i++)
        {
            Vector3 posicionActual = plataformasPaArriba[i].position;
            posicionActual.y += velocidadMovimiento * Time.deltaTime;
            plataformasPaArriba[i].position = posicionActual;

            // Si la plataforma alcanza la posición yDestino
            if (posicionActual.y > 5.5f)
            {
                // Reposicionar la plataforma por abajo
                posicionActual.y = -5.5f;
                plataformasPaArriba[i].position = posicionActual;
            }
        }
        for (int i = 0; i < plataformasPaAbajo.Length; i++)
        {
            Vector3 posicionActual = plataformasPaAbajo[i].position;
            posicionActual.y -= velocidadMovimiento * Time.deltaTime;
            plataformasPaAbajo[i].position = posicionActual;

            // Si la plataforma alcanza la posición yDestino
            if (posicionActual.y < -5.5f)
            {
                // Reposicionar la plataforma por arriba
                posicionActual.y = 5.5f;
                plataformasPaAbajo[i].position = posicionActual;
            }
        }
        for (int i = 0; i < plataformasPaIzq.Length; i++)
        {
            Vector3 posicionActual = plataformasPaIzq[i].position;
            posicionActual.x += velocidadMovimiento * Time.deltaTime;
            plataformasPaIzq[i].position = posicionActual;

            // Si la plataforma alcanza la posición yDestino
            if (posicionActual.x > -9.89f)
            {
                // Reposicionar la plataforma por abajo
                posicionActual.x = 9.89f;
                plataformasPaIzq[i].position = posicionActual;
            }
        }
        for (int i = 0; i < plataformasPaDcha.Length; i++)
        {
            Vector3 posicionActual = plataformasPaDcha[i].position;
            posicionActual.x += velocidadMovimiento * Time.deltaTime;
            plataformasPaDcha[i].position = posicionActual;

            // Si la plataforma alcanza la posición yDestino
            if (posicionActual.x > -9.89f)
            {
                // Reposicionar la plataforma por abajo
                posicionActual.x = 9.89f;
                plataformasPaDcha[i].position = posicionActual;
            }
        }
        //Obtener la posición actual de la plataforma seleccionada
        //Antigua version
        ////Debug.Log("entra en funcion movplat");

        ////Debug.Log("La dirección elegida es: " + direccionElegida);
        //// Obtener la posición actual de la plataforma seleccionada
        //Vector3 posicionActual = plataformas[plataformaActual].position;

        //// Utilizar un switch case para mover la plataforma según la dirección elegida

        //    //Debug.Log("switch");
        //    switch (direccionElegida)
        //    {
        //        case "izquierda":
        //            // Mover la plataforma hacia izquierda
        //            posicionActual.x -= velocidadMovimiento * Time.deltaTime;
        //        break;

        //        case "derecha":
        //            // Mover la plataforma hacia derecha
        //            posicionActual.x += velocidadMovimiento * Time.deltaTime;
        //        break;

        //        case "arriba":
        //            // Mover la plataforma hacia arriba
        //            posicionActual.y += velocidadMovimiento * Time.deltaTime;
        //        break;

        //        case "abajo":
        //            // Mover la plataforma hacia abajo
        //            posicionActual.y -= velocidadMovimiento * Time.deltaTime;
        //        break;

        //        default:
        //            Debug.LogError("Dirección no reconocida");
        //            break;
        //    }

        //// Si la plataforma alcanza los límites superiores o inferiores // proxima version restar al limitador lo recrrido por la plataforma 
        //if (posicionActual.y >= 5.5f)
        //{
        //    CambiarDireccion();
        //}
        //else if (posicionActual.y <= -5.5f)
        //{
        //    CambiarDireccion();
        //}

        //// Si la plataforma alcanza los límites izquierdos o derechos
        //if (posicionActual.x >= 11.7f)
        //{
        //    CambiarDireccion();
        //}
        //else if (posicionActual.x <= -11.7f)
        //{
        //    CambiarDireccion();
        //}

        //// Si la plataforma alcanza la posición yDestino
        //if (direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y + limitadorMovimiento  ||
        //    direccionElegida == "abajo" && posicionActual.y < posicionesIniciales[plataformaActual].y -limitadorMovimiento )
        //    {
        //        //Debug.Log("cambio dir");
        //        CambiarDireccion();
        //        DetectarVueltaAPosicion = true;
        //}
        //else if (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x + limitadorMovimiento  ||
        //        direccionElegida == "izquierda" && posicionActual.x < posicionesIniciales[plataformaActual].x - limitadorMovimiento)
        //    {
        //        //Debug.Log("cambio dir");
        //        CambiarDireccion();
        //        DetectarVueltaAPosicion = true;
        //    }
        //plataformas[plataformaActual].position = posicionActual;

        //// Si la plataforma ha vuelto a su posición inicial No he podido hacerlo mas corto el mathf approximately no me ha funcinado y si lo comparo con su posicion inicial (==) tampoco
        //if ((direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion || 
        //   (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
        //   (direccionElegida == "izquierda" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
        //   (direccionElegida == "abajo" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion)
        //    {
        //        //Debug.Log("entra en el detener plat");

        //        // Detener el movimiento estableciendo la posición a la inicial
        //        plataformas[plataformaActual].position = posicionesIniciales[plataformaActual];
        //        // Cambiar a otra plataforma al azar
        //        plataformaActual = Random.Range(0, plataformas.Length);
        //        // Elegir una dirección aleatoria del array a no ser que quieras que vaya en una direccion concreta
        //        if (movimientoX && !movimientoY)
        //        {
        //            direccionElegida = direccionPlataforma[Random.Range(0, 1)];
        //        }
        //        else if (movimientoY && !movimientoX)
        //        {
        //            direccionElegida = direccionPlataforma[Random.Range(2, direccionPlataforma.Length)];
        //        }
        //        else
        //        {
        //            direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];
        //        }
        //        DetectarVueltaAPosicion = false;
        //        //plataformaEnMovimiento = false;
    }
}
//Antigua Version
//    void CambiarDireccion()
//    {
//        // Invertir la dirección de la plataforma según la dirección actual
//        if (direccionElegida == "izquierda")
//        {
//            direccionElegida = "derecha";
//        }
//        else if (direccionElegida == "derecha")
//        {
//            direccionElegida = "izquierda";
//        }
        
//        else if (direccionElegida == "arriba")
//        {
//            direccionElegida = "abajo";
//        }
//        else
//        {
//            direccionElegida = "arriba";
//        }
//    }
//}







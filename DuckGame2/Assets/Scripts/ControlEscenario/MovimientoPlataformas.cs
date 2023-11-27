using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataformas : MonoBehaviour
{
    public Transform[] plataformas;
    public float velocidadMovimiento = 3f;
    private Vector3[] posicionesIniciales;
    private int plataformaActual;
    private bool DetectarVueltaAPosicion;
    private float limitadorMovimiento = 5f;
    private string[] direccionPlataforma = {"izquierda","derecha","arriba","abajo" };
    private string direccionElegida;
    //private bool plataformaEnMovimiento;
    void Start()
    {
        posicionesIniciales = new Vector3[plataformas.Length];

        // Guardar las posiciones iniciales de las plataformas
        for (int i = 0; i < plataformas.Length; i++)
        {
            posicionesIniciales[i] = plataformas[i].position;
        }
        //plataformaEnMovimiento = false;
        DetectarVueltaAPosicion = false;
        // Cambiar a otra plataforma al azar
        plataformaActual = Random.Range(0, plataformas.Length);
        // Elegir una direcci�n aleatoria del array
        direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];
    }

    void Update()
    {
        //if (!plataformaEnMovimiento) 
        //{
        //    Debug.Log("entra en el if update");

            MoverPlataforma();
        //}
        
    }

    void MoverPlataforma() 
    {
        //Debug.Log("entra en funcion movplat");

        //Debug.Log("La direcci�n elegida es: " + direccionElegida);
        // Obtener la posici�n actual de la plataforma seleccionada
        Vector3 posicionActual = plataformas[plataformaActual].position;

        // Utilizar un switch case para mover la plataforma seg�n la direcci�n elegida

            //Debug.Log("switch");
            switch (direccionElegida)
            {
                case "izquierda":
                    // Mover la plataforma hacia izquierda
                    posicionActual.x -= velocidadMovimiento * Time.deltaTime;
                break;

                case "derecha":
                    // Mover la plataforma hacia derecha
                    posicionActual.x += velocidadMovimiento * Time.deltaTime;
                break;

                case "arriba":
                    // Mover la plataforma hacia arriba
                    posicionActual.y += velocidadMovimiento * Time.deltaTime;
                break;

                case "abajo":
                    // Mover la plataforma hacia abajo
                    posicionActual.y -= velocidadMovimiento * Time.deltaTime;
                break;

                default:
                    Debug.LogError("Direcci�n no reconocida");
                    break;
            }

        // Si la plataforma alcanza los l�mites superiores o inferiores // proxima version restar al limitador lo recrrido por la plataforma 
        if (posicionActual.y >= 5.5f)
        {
            CambiarDireccion();
        }
        else if (posicionActual.y <= -5.5f)
        {
            CambiarDireccion();
        }

        // Si la plataforma alcanza los l�mites izquierdos o derechos
        if (posicionActual.x >= 11.7f)
        {
            CambiarDireccion();
        }
        else if (posicionActual.x <= -11.7f)
        {
            CambiarDireccion();
        }
        
        // Si la plataforma alcanza la posici�n yDestino
        if (direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y + limitadorMovimiento  ||
            direccionElegida == "abajo" && posicionActual.y < posicionesIniciales[plataformaActual].y -limitadorMovimiento )
            {
                //Debug.Log("cambio dir");
                CambiarDireccion();
                DetectarVueltaAPosicion = true;
        }
        else if (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x + limitadorMovimiento  ||
                direccionElegida == "izquierda" && posicionActual.x < posicionesIniciales[plataformaActual].x - limitadorMovimiento)
            {
                //Debug.Log("cambio dir");
                CambiarDireccion();
                DetectarVueltaAPosicion = true;
            }
        plataformas[plataformaActual].position = posicionActual;

        // Si la plataforma ha vuelto a su posici�n inicial No he podido hacerlo mas corto el mathf approximately no me ha funcinado y si lo comparo con su posicion inicial (==) tampoco
        if ((direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion || 
           (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
           (direccionElegida == "izquierda" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
           (direccionElegida == "abajo" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion)
            {
                //Debug.Log("entra en el detener plat");

                // Detener el movimiento estableciendo la posici�n a la inicial
                plataformas[plataformaActual].position = posicionesIniciales[plataformaActual];
                // Cambiar a otra plataforma al azar
                plataformaActual = Random.Range(0, plataformas.Length);
                // Elegir una direcci�n aleatoria del array
                direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];
                DetectarVueltaAPosicion = false;
                //plataformaEnMovimiento = false;
        }
    }
    void CambiarDireccion()
    {
        // Invertir la direcci�n de la plataforma seg�n la direcci�n actual
        if (direccionElegida == "izquierda")
        {
            direccionElegida = "derecha";
        }
        else if (direccionElegida == "derecha")
        {
            direccionElegida = "izquierda";
        }
        
        else if (direccionElegida == "arriba")
        {
            direccionElegida = "abajo";
        }
        else
        {
            direccionElegida = "arriba";
        }
    }
}






//Futuro codigo para que las plataformas que pasen al otro lado cuando el contadormovimiento llegue al limitador movimiento cambien de direccion esta casi
//public Transform[] plataformas;
//public float velocidadMovimiento = 3f;
//private Vector3[] posicionesIniciales;
//private int plataformaActual;
//private bool DetectarVueltaAPosicion;
//public float limitadorMovimiento = 10f;
//private string[] direccionPlataforma = { "izquierda", "derecha", "arriba", "abajo" };
//private string direccionElegida;
////private bool plataformaEnMovimiento;
//private float contadorMovimientoX;
//private float contadorMovimientoY;
//void Start()
//{
//    posicionesIniciales = new Vector3[plataformas.Length];

//    // Guardar las posiciones iniciales de las plataformas
//    for (int i = 0; i < plataformas.Length; i++)
//    {
//        posicionesIniciales[i] = plataformas[i].position;
//    }
//    //plataformaEnMovimiento = false;
//    DetectarVueltaAPosicion = false;
//    // Cambiar a otra plataforma al azar
//    plataformaActual = Random.Range(0, plataformas.Length);
//    // Elegir una direcci�n aleatoria del array
//    direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];

//    contadorMovimientoX = 0f;
//    contadorMovimientoY = 0f;
//}

//void Update()
//{
//    //if (!plataformaEnMovimiento) 
//    //{
//    //    Debug.Log("entra en el if update");

//    MoverPlataforma();
//    //}

//}

//void MoverPlataforma()
//{
//    //Debug.Log("entra en funcion movplat");

//    //Debug.Log("La direcci�n elegida es: " + direccionElegida);
//    // Obtener la posici�n actual de la plataforma seleccionada
//    Vector3 posicionActual = plataformas[plataformaActual].position;

//    // Utilizar un switch case para mover la plataforma seg�n la direcci�n elegida

//    //Debug.Log("switch");
//    switch (direccionElegida)
//    {
//        case "izquierda":
//            // Mover la plataforma hacia izquierda
//            posicionActual.x -= velocidadMovimiento * Time.deltaTime;
//            if (!DetectarVueltaAPosicion)
//            {
//                contadorMovimientoX += velocidadMovimiento * Time.deltaTime;

//            }
//            break;

//        case "derecha":
//            // Mover la plataforma hacia derecha
//            posicionActual.x += velocidadMovimiento * Time.deltaTime;
//            if (!DetectarVueltaAPosicion)
//            {
//                contadorMovimientoX += velocidadMovimiento * Time.deltaTime;

//            }
//            break;

//        case "arriba":
//            // Mover la plataforma hacia arriba
//            posicionActual.y += velocidadMovimiento * Time.deltaTime;
//            if (!DetectarVueltaAPosicion)
//            {
//                contadorMovimientoY += velocidadMovimiento * Time.deltaTime;

//            }
//            break;

//        case "abajo":
//            // Mover la plataforma hacia abajo
//            posicionActual.y -= velocidadMovimiento * Time.deltaTime;
//            if (!DetectarVueltaAPosicion)
//            {
//                contadorMovimientoY += velocidadMovimiento * Time.deltaTime;

//            }
//            break;

//        default:
//            Debug.LogError("Direcci�n no reconocida");
//            break;
//    }

//    // Si la plataforma alcanza los l�mites superiores o inferiores // proxima version restar al limitador lo recrrido por la plataforma 
//    if (posicionActual.y > 5.5f)
//    {
//        posicionActual.y = -5.5f;
//    }
//    else if (posicionActual.y < -5.5f)
//    {
//        posicionActual.y = 5.5f;
//    }

//    // Si la plataforma alcanza los l�mites izquierdos o derechos
//    if (posicionActual.x > 11.7f)
//    {
//        posicionActual.x = -11.7f;
//    }
//    else if (posicionActual.x < -11.7f)
//    {
//        posicionActual.x = 11.7f;
//    }

//    // Si la plataforma alcanza la posici�n yDestino
//    if (direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y + limitadorMovimiento ||
//        direccionElegida == "abajo" && posicionActual.y < posicionesIniciales[plataformaActual].y - limitadorMovimiento || contadorMovimientoY >= limitadorMovimiento)
//    {
//        Debug.Log("cambio dir");
//        // Invertir la direcci�n de la plataforma seg�n la direcci�n actual
//        if (direccionElegida == "arriba")
//        {
//            direccionElegida = "abajo";
//        }
//        else if (direccionElegida == "abajo")
//        {
//            direccionElegida = "arriba";
//        }
//        DetectarVueltaAPosicion = true;
//        contadorMovimientoY = 0f;
//    }
//    else if (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x + limitadorMovimiento ||
//            direccionElegida == "izquierda" && posicionActual.x < posicionesIniciales[plataformaActual].x - limitadorMovimiento || contadorMovimientoX >= limitadorMovimiento)
//    {
//        //Debug.Log("cambio dir");
//        if (direccionElegida == "izquierda")
//        {
//            direccionElegida = "derecha";
//        }
//        else if (direccionElegida == "derecha")
//        {
//            direccionElegida = "izquierda";
//        }
//        DetectarVueltaAPosicion = true;
//        contadorMovimientoX = 0f;
//    }
//    plataformas[plataformaActual].position = posicionActual;

//    // Si la plataforma ha vuelto a su posici�n inicial No he podido hacerlo mas corto el mathf approximately no me ha funcinado y si lo comparo con su posicion inicial (==) tampoco
//    if ((direccionElegida == "arriba" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion ||
//       (direccionElegida == "derecha" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
//       (direccionElegida == "izquierda" && posicionActual.x > posicionesIniciales[plataformaActual].x - 0.08f && posicionActual.x < posicionesIniciales[plataformaActual].x + 0.08f) && DetectarVueltaAPosicion ||
//       (direccionElegida == "abajo" && posicionActual.y > posicionesIniciales[plataformaActual].y - 0.08f && posicionActual.y < posicionesIniciales[plataformaActual].y + 0.08f) && DetectarVueltaAPosicion)
//    {
//        //Debug.Log("entra en el detener plat");

//        // Detener el movimiento estableciendo la posici�n a la inicial
//        plataformas[plataformaActual].position = posicionesIniciales[plataformaActual];
//        // Cambiar a otra plataforma al azar
//        plataformaActual = Random.Range(0, plataformas.Length);
//        // Elegir una direcci�n aleatoria del array
//        direccionElegida = direccionPlataforma[Random.Range(0, direccionPlataforma.Length)];
//        DetectarVueltaAPosicion = false;
//        //plataformaEnMovimiento = false;
//    }
//}
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
    }

    void Update()
    {
        //if (!plataformaEnMovimiento) //Habr�a que agregar un if aqui para que no llame a la funci�n todo el rato pero no se pq este bool no me funcina
        //{
            //Debug.Log("entra en el if update");

            MoverPlataforma();
        //}
        
    }

    void MoverPlataforma() // *Alomejor es mejor usar un IEnumerator mas adelante* ya que asi podriamos quitar booleanos como detectarvueltaaposicion y agregar una pausa
    {
        //Debug.Log("entra en funcion movplat");
        // Obtener la posici�n actual de la plataforma seleccionada
        Vector3 posicionActual = plataformas[plataformaActual].position;
        
        // Mover la plataforma hacia arriba
        posicionActual.y += velocidadMovimiento * Time.deltaTime;
        plataformas[plataformaActual].position = posicionActual;
        //plataformaEnMovimiento = true;
        // Si la plataforma alcanza la posici�n yDestino
        if (plataformas[plataformaActual].position.y > 5.5f)
        {
            //Debug.Log("entra en el pasar pa abajo");

            // Reposicionar la plataforma por debajo
            posicionActual.y = -5.5f;
            plataformas[plataformaActual].position = posicionActual;
            DetectarVueltaAPosicion = true;
        }
        
        // Si la plataforma ha vuelto a su posici�n inicial No he podido hacerlo mas corto el mathf approximately no me ha funcinado y si lo comparo con su posicion inicial (==) tampoco
        if (DetectarVueltaAPosicion && plataformas[plataformaActual].position.y >= posicionesIniciales[plataformaActual].y - 0.08f && plataformas[plataformaActual].position.y <= posicionesIniciales[plataformaActual].y + 0.08f )
        {
            //Debug.Log("entra en el detener plat");

            // Detener el movimiento estableciendo la posici�n a la inicial
            plataformas[plataformaActual].position = posicionesIniciales[plataformaActual];
            // Cambiar a otra plataforma al azar
            plataformaActual = Random.Range(0, plataformas.Length);
            DetectarVueltaAPosicion = false;
            //plataformaEnMovimiento = false;
        }
    }
}

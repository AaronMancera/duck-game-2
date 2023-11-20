
using System;
using UnityEngine;

public class InvocadorDeObjeto : MonoBehaviour
{
    //private EnumObjetos enumObjetos;
    [SerializeField] private GameObject objetoInvocado;
    private bool tengoArma;

    // Start is called before the first frame update
    void Start()
    {
        tengoArma = false;
        InvocarObjeto();
    }

    // Update is called once per frame
    void Update()
    {
        //PARA QUE GIRE SOBRE SU PROPIO EJE
        if(objetoInvocado != null)
        {
            float gradosSegundo = 100;
            objetoInvocado.transform.Rotate(new Vector3(0, gradosSegundo, 0) * Time.deltaTime);
        }

    }

    #region TRIGGER

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.CompareTag("Player") && tengoArma)
        {
            Debug.Log($"Cogiendo objeto, dando a {collision.gameObject.name}");
            tengoArma = false;
            OtorgarArma(collision.gameObject.GetComponent<ControlJugador>());   //DAR ARMA A JUGADOR
        }

    }



    #endregion

    #region MÉTODOS

    private void InvocarObjeto()
    {
        
        int rangoObjetos = Enum.GetNames(typeof(EnumObjetos)).Length;   //NUMERO DE OBJETOS DISPONIBLES
        int objetoAleatorio = UnityEngine.Random.Range(0, rangoObjetos);    //GENERAR OBJETO ALEATORIO
        string nombreObjeto = Enum.GetName(typeof(EnumObjetos), objetoAleatorio);   //COGER EL NOMBRE DEL OBJETO ALEATORIO
        Debug.Log($"El objeto generado es:  {nombreObjeto}");
        objetoInvocado.GetComponent<Objeto>().setNombre(nombreObjeto);  //SET OBJETO

        tengoArma = true;
        
    }

    private void OtorgarArma(ControlJugador jugador)
    {
        //PONERLO EN EL INVENTARIO
        if (objetoInvocado.GetComponent<Objeto>().getNombre().Contains("Arma"))
        {
            jugador.inventario["Arma"] = objetoInvocado.GetComponent<Objeto>(); 
            Debug.Log($"El jugador ahora tiene el arma: {jugador.inventario["Arma"].getNombre()}");
        } 
        else
        {
            jugador.inventario["Objeto"] = objetoInvocado.GetComponent<Objeto>();
            Debug.Log($"El jugador ahora tiene el objeto: {jugador.inventario["Objeto"].getNombre()}");
        }
           
        Destroy(objetoInvocado);    //DESTRUIR OBJETO DEL PEDESTAL
    }

    #endregion
}

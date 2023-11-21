
using System;
using System.Collections.Generic;
using UnityEngine;

public class InvocadorDeObjeto : MonoBehaviour
{
    Transform parent;
    //private EnumObjetos enumObjetos;
    [SerializeField] private GameObject objetoInvocado;
    [SerializeField] private List<GameObject> objetos;
    private bool tengoArma;


    // Start is called before the first frame update
    void Start()
    {
        tengoArma = false;
        parent = this.gameObject.GetComponent<Transform>();
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
            OtorgarArma(collision.gameObject.GetComponent<ControlJugador>());   //DAR ARMA A JUGADOR

        }

    }

    #endregion

    #region M�TODOS

    private void InvocarObjeto()
    {
        
        int rangoObjetos = Enum.GetNames(typeof(EnumObjetos)).Length;   //NUMERO DE OBJETOS DISPONIBLES
        int objetoAleatorio = UnityEngine.Random.Range(0, rangoObjetos);    //GENERAR OBJETO ALEATORIO
        string nombreObjeto = Enum.GetName(typeof(EnumObjetos), objetoAleatorio);   //COGER EL NOMBRE DEL OBJETO ALEATORIO

        Debug.Log($"El objeto generado es:  {nombreObjeto}");

        //objetoInvocado.GetComponent<Objeto>().setNombre(nombreObjeto);  //SET OBJETO
        objetoInvocado = Instantiate(objetos[objetoAleatorio], objetoInvocado.transform.position, Quaternion.identity, parent);

        tengoArma = true;
        
        
    }

    private void OtorgarArma(ControlJugador jugador)
    {
        //PONERLO EN EL INVENTARIO


        if (objetoInvocado.GetComponent<Objeto>().getNombre().Contains("Arma") && jugador.gameObject.GetComponent<ControlJugador>().principalEnMano == null)
        {
            jugador.RecogerArma(objetoInvocado.GetComponent<Objeto>().getNombre(), 0);

            jugador.inventario["Arma"] = objetoInvocado;


            tengoArma = false;


            Destroy(objetoInvocado);
            //Debug.Log($"El jugador ahora tiene el arma: {jugador.inventario["Arma"].getNombre()}");
        } 
        else if(!objetoInvocado.GetComponent<Objeto>().getNombre().Contains("Arma") && jugador.gameObject.GetComponent<ControlJugador>().secundariaEnMano == null)
        {
            jugador.RecogerArma(objetoInvocado.GetComponent<Objeto>().getNombre(), 1);
            jugador.inventario["Objeto"] = objetoInvocado;

            tengoArma = false;


            Destroy(objetoInvocado);
            //Debug.Log($"El jugador ahora tiene el objeto: {jugador.inventario["Objeto"].getNombre()}");
        }
           
        //Destroy(objetoInvocado);    //DESTRUIR OBJETO DEL PEDESTAL
    }

    #endregion
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvocadorDeObjeto : MonoBehaviour
{
    Transform parent;
    [SerializeField] private GameObject objetoInvocado;
    [SerializeField] private List<GameObject> objetos;
    private bool tengoArma;
    private bool animGiroObjeto;
    private float segundosEspera = 5f;

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
        if (objetoInvocado != null)
        {
            float segundosGiro = 100f;
            objetoInvocado.transform.Rotate(new Vector3(0, segundosGiro, 0) * Time.deltaTime);
        }
        else
        {
            StartCoroutine(RutinaGenerarObjetos());
        }

    }

    #region TRIGGER

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && tengoArma)
        {
            //Debug.Log($"Cogiendo objeto, dando a {collision.gameObject.name}");
            OtorgarArma(collision.gameObject.GetComponent<ControlJugador>());   //DAR ARMA A JUGADOR

        }

    }

    #endregion

    #region MÉTODOS

    private void InvocarObjeto()
    {
        if (!tengoArma)
        {
            int rangoObjetos = Enum.GetNames(typeof(EnumObjetos)).Length;   //NUMERO DE OBJETOS DISPONIBLES
            int objetoAleatorio = UnityEngine.Random.Range(0, rangoObjetos);    //GENERAR OBJETO ALEATORIO
            string nombreObjeto = Enum.GetName(typeof(EnumObjetos), objetoAleatorio);   //COGER EL NOMBRE DEL OBJETO ALEATORIO

            //Debug.Log($"El objeto generado es:  {nombreObjeto}");
            Vector3 posicionDeInicio = transform.position+ new Vector3(-0.20f, 0.3f, 0);
            objetoInvocado = Instantiate(objetos[objetoAleatorio], /*objetoInvocado.transform.position*/posicionDeInicio, Quaternion.identity, parent);

            tengoArma = true;
        }
    }

    private void OtorgarArma(ControlJugador jugador)
    {
        //PONERLO EN EL INVENTARIO


        if (objetoInvocado.GetComponent<Objeto>().getNombre().Contains("Arma") && jugador.gameObject.GetComponent<ControlJugador>().principalEnMano == null)
        {
            jugador.RecogerArma(objetoInvocado.GetComponent<Objeto>().getNombre(), 0);
        }
        else if (!objetoInvocado.GetComponent<Objeto>().getNombre().Contains("Arma") && jugador.gameObject.GetComponent<ControlJugador>().secundariaEnMano == null)
        {
            jugador.RecogerArma(objetoInvocado.GetComponent<Objeto>().getNombre(), 1);
        }

        tengoArma = false;
        Destroy(objetoInvocado);
        
        //objetoInvocado.SetActive(false);
    }

    #endregion
    #region RUTINA
    private IEnumerator RutinaGenerarObjetos()
    {
       yield return new WaitForSeconds(segundosEspera);
        InvocarObjeto();
    }
    #endregion

}

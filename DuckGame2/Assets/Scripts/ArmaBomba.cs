using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaBomba : Objeto
{
    [Header("Atributos")]
    [SerializeField] private GameObject bomba;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "ArmaBomba";
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && numUsos > 0)
        {
            Disparar();
        }
        else
        {
            Debug.Log("El jugador soltará este arma");
        }
    }

    /// <summary>
    /// Dispara la bomba
    /// </summary>
    private void Disparar()
    {
        Bomba componenteBomba = bomba.gameObject.GetComponent<Bomba>();

        Instantiate(componenteBomba, bomba.gameObject.transform.position, bomba.gameObject.transform.rotation);

        numUsos--;
    }

    /// <summary>
    /// Activa el gameObject
    /// </summary>
    private void Activar()
    {
        gameObject.SetActive(true);
        numUsos = 1;
        interactuable = true;
    }
}

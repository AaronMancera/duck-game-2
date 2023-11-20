using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaBomba : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private GameObject bomba;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Disparar();
        }
    }

    /// <summary>
    /// Dispara la bomba
    /// </summary>
    private void Disparar()
    {
        Bomba componenteBomba = bomba.gameObject.GetComponent<Bomba>();

        Instantiate(componenteBomba, transform.position, transform.rotation);
    }

    /// <summary>
    /// Activa el gameObject
    /// </summary>
    private void Activacion()
    {
        gameObject.SetActive(true);
    }
}

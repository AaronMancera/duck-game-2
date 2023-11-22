using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarPollo : Objeto
{

    


    public GameObject PolloPrefab;
    // Start is called before the first frame update
    void Start()
    {
        nombre = "LanzarPollo";
        Inicializar();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && interactuable == true)
        {
            GenerarPollo();
        }

        if (numUsos == 0)
        {
            Reiniciar();
        }
    }

    private void GenerarPollo()
    {
        Instantiate(PolloPrefab, transform.position, gameObject.transform.rotation);
        numUsos = numUsos -1; 
    }

    private void Inicializar()
    {
        gameObject.SetActive(true);
        numUsos = 1;
        interactuable = true;
    }

    public void Reiniciar()
    {
        gameObject.SetActive(false);
    }



   
}

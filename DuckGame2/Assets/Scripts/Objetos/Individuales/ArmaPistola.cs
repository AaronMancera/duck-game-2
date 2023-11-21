using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaPistola : Objeto 
{
    float cadenciaDisparo;
    [SerializeField] GameObject bala;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disparar()
    {

    }
}

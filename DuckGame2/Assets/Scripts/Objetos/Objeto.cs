using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour

{
    [SerializeField] private int numUsosMaximo;
    public int numUsos;
    //public bool interactuable;
    public string nombre;
    public Animator animator;
    public string getNombre()
    {
        return nombre;
    }
    public void Activar()
    {
        numUsos = numUsosMaximo;
        //interactuable = true;
        gameObject.SetActive(true);

    }
}

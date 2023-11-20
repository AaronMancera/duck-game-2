using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    private int numUsos;
    private bool interactuable;
    private string nombre;
    private Animator animator;

    public string getNombre()
    {
        return nombre;
    }

    public void setNombre(string nombre)
    {
        this.nombre = nombre;
    }

}

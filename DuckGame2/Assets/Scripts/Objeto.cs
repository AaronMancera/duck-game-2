using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    [Header("CaracterÝsticas Objeto")]
    [HideInInspector] public Animator animator;
    public string nombre;
    public int numUsos;
    public bool interactuable;

    public string getNombre()
    {
        return nombre;
    }

    public void setNombre(string nombre)
    {
        this.nombre = nombre;
    }

}

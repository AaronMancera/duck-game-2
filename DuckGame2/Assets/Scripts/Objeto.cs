using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto: MonoBehaviour
{
    public int numUsos;
    public bool interactuable;
    public string nombre;
    public Animator animator;

    public string getNombre()
    {
        return nombre;
    }
}

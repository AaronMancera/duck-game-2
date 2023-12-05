using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreditosControlador : MonoBehaviour
{
    public int enQuePaginaEsta;
    [SerializeField] GameObject[] paginas;
    public void CarruselSiguiente()
    {
        enQuePaginaEsta = (enQuePaginaEsta + 1) % paginas.Length;
        RefrescarPagina();
        
    }
    public void CarruselAnterior()
    {
        enQuePaginaEsta = (enQuePaginaEsta - 1 + paginas.Length) % paginas.Length;
        RefrescarPagina();
    }
    void RefrescarPagina()
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            if (i == enQuePaginaEsta)
            {
                paginas[i].SetActive(true);
            }
            else
            {
                paginas[i].SetActive(false);
            }
        }
    }
}

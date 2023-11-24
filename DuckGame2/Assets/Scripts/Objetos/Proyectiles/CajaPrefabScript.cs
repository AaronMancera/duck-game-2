using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPrefabScript : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float fuerzaDeEmpuje;
    [SerializeField] private Animator animator;

    void Start()
    {
        Lanzada();
    }

    private void Explotar()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Lanza la bomba
    /// </summary>
    private void Lanzada()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce((Vector2.one - new Vector2(0.3f, 0.3f)) * fuerzaDeEmpuje, ForceMode2D.Impulse);

    }

    /// <summary>
    /// Se encarga de las colisiones de la bomba
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

}

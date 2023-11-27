using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FadePlataformas : MonoBehaviour
{
    [SerializeField] private float segundosDesactivar, segundosActivar;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(temporizadorDesactivar());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator temporizadorDesactivar()
    {
        anim.SetTrigger("Desactivar");
        yield return new WaitForSeconds(segundosActivar);
        StartCoroutine(temporizadorActivar());
    }

    IEnumerator temporizadorActivar()
    {
        anim.SetTrigger("Activar");
        yield return new WaitForSeconds(segundosDesactivar);
        StartCoroutine(temporizadorDesactivar());
    }
}

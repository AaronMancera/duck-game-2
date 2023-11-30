using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasMovimientoManager : MonoBehaviour
{
    [SerializeField] float velocidad, direccion;

    private void Update()
    {
        //transform.Translate(Vector2.up * direccion * velocidad * Time.deltaTime);
        
    }
    private void FixedUpdate()
    {
        Vector2 nuevaPoscion = new Vector2(transform.position.x, transform.position.y + direccion);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f,direccion*velocidad);
    }
}

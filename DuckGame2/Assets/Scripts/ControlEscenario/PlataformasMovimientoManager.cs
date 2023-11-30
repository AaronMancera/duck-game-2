using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformasMovimientoManager : MonoBehaviour
{
    [SerializeField] float velocidad, direccion;

    private void Update()
    {
        transform.Translate(Vector2.up * direccion * velocidad * Time.deltaTime);
    }
}

using UnityEngine;

public class LanzarCaja : Objeto
{
    [Header("Configuración de Caja")]
    [SerializeField] GameObject cajaPrefab;
    [SerializeField] float fuerzaLanzamiento;
    [SerializeField] float anguloInclinacion = 30f;

    bool mirandoALaIzquierda;

    void Update()
    {
        ActualizarDireccion();
        UsarCaja();
    }

    void ActualizarDireccion()
    {
        float direccionHorizontal = Input.GetAxis("Horizontal");

        if (direccionHorizontal != 0)
        {
            mirandoALaIzquierda = direccionHorizontal < 0;
        }
    }

    void UsarCaja()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Lanzar();
        }
    }

    void Lanzar()
    {
        GameObject nuevaCaja = Instantiate(cajaPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rbCaja = nuevaCaja.GetComponent<Rigidbody2D>();

        // Obtener la dirección de lanzamiento basada en la última dirección registrada
        float direccionLanzamiento = mirandoALaIzquierda ? -1f : 1f;

        // Calcular el ángulo de inclinación en radianes
        float anguloRad = Mathf.Deg2Rad * anguloInclinacion;

        // Calcular los componentes X e Y de la fuerza inicial con el ángulo de inclinación
        float fuerzaX = fuerzaLanzamiento * direccionLanzamiento * Mathf.Cos(anguloRad);
        float fuerzaY = fuerzaLanzamiento * Mathf.Sin(anguloRad);

        // Aplicar fuerza inicial al lanzar la caja
        Vector2 fuerzaInicial = new Vector2(fuerzaX, fuerzaY);
        rbCaja.AddForce(fuerzaInicial, ForceMode2D.Impulse);
    }

   
}
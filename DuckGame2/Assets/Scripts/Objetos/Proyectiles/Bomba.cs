using System.Collections;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private float fuerzaDeEmpuje;
    [SerializeField] private float tiempoParaExplotar;
    [SerializeField] private float fuerzaDeRotacion = -1000;
    [SerializeField] private Animator animator;
    [SerializeField] ParticleSystem particulasBomba;

    // Start is called before the first frame update
    void Start()
    {
        Lanzada();
        StartCoroutine(RutinaExplotar());
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Explota la bomba
    /// </summary>
    private void Explotar()
    {
        float radio = 4f;

        Instantiate(particulasBomba, transform.position, Quaternion.identity);
        Destroy(gameObject);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radio);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            float fuerza = 4f;

            if (rb != null)
            {
                ControlJugador cj = collider.GetComponent<ControlJugador>();
                rb.AddForce(new Vector2(fuerza * (collider.transform.position.x + transform.position.x), fuerza * (collider.transform.position.y + transform.position.y)), ForceMode2D.Impulse);
                if (cj != null)
                {
                    cj.RecibirDanyo();
                }
            }
        }
    }

    /// <summary>
    /// Lanza la bomba
    /// </summary>
    private void Lanzada()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(Vector2.one * fuerzaDeEmpuje, ForceMode2D.Impulse);
        rigidbody2D.AddTorque(fuerzaDeRotacion);
    }

    /// <summary>
    /// Se encarga de las colisiones de la bomba
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    /// <summary>
    /// Rutina para que la bomba explote una vez el temporizador llegue a cero
    /// </summary>
    /// <returns></returns>
    private IEnumerator RutinaExplotar()
    {
        yield return new WaitForSeconds(tiempoParaExplotar);
        Explotar();
    }
}

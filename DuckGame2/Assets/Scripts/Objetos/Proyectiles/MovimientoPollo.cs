using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPollo : MonoBehaviour
{
    public float speed;
    public float vidaSecondsCounter = 0;
    private Rigidbody2D rb2d;

    public float saltoFuerza;

    public bool mirandoDerecha;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        rb2d = GetComponent<Rigidbody2D>();

        flip = mirandoDerecha;
        GetComponent<SpriteRenderer>().flipX = !mirandoDerecha;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCollisions();
        FlipLlamada();


        vidaSecondsCounter += Time.deltaTime;

        if (vidaSecondsCounter > 30)
        {
            FinPollo();
        }


    }



    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            Saltar();
        }
    }

    private void Saltar()
    {
        int fuerzaAleatoria = UnityEngine.Random.Range(2, 8);

        rb2d.velocity = new Vector2(rb2d.velocity.x, 0f);
        rb2d.AddForce(Vector2.up * (float)fuerzaAleatoria, ForceMode2D.Impulse);

    }

    private void FinPollo()
    {
        Destroy(gameObject);
    }



    #region FLIP
    public float velocidad;

    public bool flip, flipBool;
    [SerializeField] float segundosParaActivarFlip;

    [Header("Raycasts")]
    [SerializeField] Vector3 wallRaycastOffset;
    [SerializeField] float wallRaycastLength;
    public bool checkWall;

    [Header("LayerMasks")]
    [SerializeField] LayerMask groundLayer;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (checkWall)
        {
            Gizmos.color = Color.green;
        }

        //Wall Check
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);


    }

    private void CheckCollisions()
    {
        //Wall Collisions
        if ((Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, groundLayer) ||
            Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, groundLayer))
            && flipBool)

        {
            Flip();
            flipBool = false;
            //GetComponent<SpriteRenderer>().flipX = !flip;
            StartCoroutine(FlipCount(segundosParaActivarFlip));
        }


        checkWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, groundLayer) ||
                    Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, groundLayer);

    }

    IEnumerator FlipCount(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        flipBool = true;
    }


    void Flip()
    {
        flip = !flip;
        //transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
        GetComponent<SpriteRenderer>().flipX = !flip;

    }

    private void FlipLlamada()
    {
        Vector3 movimiento = transform.position;
        if (!flip)
        {
            //rb2d.velocity = new Vector2(rb2d.velocity.x - (-velocidad * Time.fixedDeltaTime), rb2d.velocity.y);
            rb2d.velocity = new Vector2(-velocidad, rb2d.velocity.y);



        }
        else
        {
            rb2d.velocity = new Vector2(velocidad, rb2d.velocity.y);

        }
    }
    #endregion
    #region Colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControlJugador>().EfectoNegativo("Ralentizar");
            //Destroy(gameObject);
            Flip();
        }
    }
    #endregion
}

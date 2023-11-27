using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Progress;


public class ControlJugador : MonoBehaviour
{
    public PlayerControls playerControls;


    [Header("ID")]
    public int idPlayer;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private int vida = 1;

    #region Movimiento
    [Header("Movimiento")]
    [SerializeField] float playerSpeed;
    [HideInInspector] float horizontalInput;
    [SerializeField] bool isGrounded;

    [SerializeField][Range(0, 1)] float horizontalDampingWhenStopping;
    [SerializeField][Range(0, 1)] float horizontalDampingWhenTurning;
    [SerializeField][Range(0, 1)] float horizontalDampingWhenInAir;
    [SerializeField][Range(0, 1)] float horizontalDampingBasic;

    Vector2 movement;

    public bool mirandoALaDerecha;

    [Header("JUMP")]
    public float jumpForce;
    [SerializeField]
    [Range(0, 1)]
    float jumpCut;
    public float jumpFallMultiplier;

    [SerializeField] float jumpBufferLength;
    float jumpBufferCounter;
    [SerializeField] float coyoteTime;
    float coyoteTimeCounter;

    [SerializeField] bool canJump;

    private bool isJumping;

    [SerializeField] int extraJumps = 1;
    public int extraJumpsValue;
    [SerializeField] float airLinearDrag = 2.5f;

    bool estaCayendo;
    #endregion

    [Header("Inventario")]
    [SerializeField] GameObject[] objetosInventario;

    public GameObject principalEnMano, secundariaEnMano;

    //Dictionary<String, Objeto> inventario;
    /*EnumObjetos enumObjetos;*/


    [Header("Raycasts")]
    [SerializeField] Vector3 groundRaycastOffset;
    [SerializeField] float groundRaycastLength;

    [Header("LayerMasks")]
    [SerializeField] LayerMask groundLayer;

    [Header("INVENTARIO")]
    public Dictionary<String, GameObject> inventario = new Dictionary<String, GameObject>();
    //public Dictionary<String, String> inventario = new Dictionary<string, string>();
    



    #region EVENTS SUBS

    private void OnEnable()
    {

        //InputManager.playerControls.Player.Saltar.performed += GetSaltoInput;
        //InputManager.playerControls.Player.Saltar.canceled += JumpCut;
        if (idPlayer == 1)
        {
            playerControls.Player.Saltar.performed += GetSaltoInput;

        }
        else if (idPlayer == 2)
        {
            playerControls.PlayerP2.Saltar.performed += GetSaltoInput;
        }
    }

    private void OnDisable()
    {
        //InputManager.playerControls.Player.Saltar.performed -= GetSaltoInput;
        //InputManager.playerControls.Player.Saltar.canceled -= JumpCut;
        if (idPlayer == 1)
        {
            playerControls.Player.Saltar.performed -= GetSaltoInput;

        }
        else if (idPlayer == 2)
        {
            //Debug.Log(InputManager.playerControls.PlayerP2.Saltar);
            playerControls.PlayerP2.Saltar.performed -= GetSaltoInput;
        }
    }
    #endregion

    #region GETEO INPUTS

    void GetSaltoInput(InputAction.CallbackContext context)
    {
        if (context.performed && extraJumpsValue > 0)
        {
            extraJumpsValue--;
            Jump(Vector2.up);
            jumpBufferCounter = jumpBufferLength;
        }
    }
    void JumpCut(InputAction.CallbackContext context) // salto corto
    {
        /*if (rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCut);
        }*/
    }
    #endregion

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        principalEnMano = null;
        secundariaEnMano = null;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        Application.targetFrameRate = 60;

        inventario.Add("Arma", null);
        inventario.Add("Objeto", null);


        if (idPlayer == 1)
        {
            animator.SetLayerWeight(0,1);
            animator.SetLayerWeight(1,0);


        }
        else if (idPlayer == 2)
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 1);
        }

    }

    private void Update()
    {
        if (transform.localScale.x == 1)
        {
            mirandoALaDerecha = true;
        }
        else
        {
            mirandoALaDerecha = false;
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            RecibirDanyo();
        }
    }

    


    private void FixedUpdate()
    {
        GetMoveInput();

        CheckCollisions();
        MoveCharacter();

        SueloControl();

        /*if (jumpBufferCounter > 0f && (coyoteTimeCounter > 0f || extraJumpsValue > 0))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }*/


        //Animator
        animator.SetBool("EstaEnElSuelo", isGrounded);
        animator.SetBool("EstaCayendo", estaCayendo);
    }

    public void EfectoNegativo(string queEfecto)
    {
        switch (queEfecto)
        {
            case "Ralentizar":
                // Implementa la lógica para ralentizar aquí
                // Por ejemplo, puedes agregar un código que afecte la velocidad del juego o la ejecución de ciertas acciones.
                Console.WriteLine("Efecto de ralentización aplicado");
                break;

            case "SoltarArmas":
                // Implementa la lógica para soltar armas aquí
                SoltarArma(principalEnMano);
                Console.WriteLine("Armas soltadas");
                break;

        }
    }
    public void RecibirDanyo()
    {
        vida--;

        //Animator
        animator.SetTrigger("Danyo");

        if (vida <= 0)
        {
            //Te mueres

        }
    }

    #region MOVIMIENTO
    private void GetMoveInput()
    {
        //movement = InputManager.playerControls.Player.Movement.ReadValue<Vector2>();
        if (idPlayer == 1)
        {
            movement = playerControls.Player.Movement.ReadValue<Vector2>();
        }
        else if (idPlayer == 2)
        {
            movement = playerControls.PlayerP2.Movement.ReadValue<Vector2>();

        }
        if (movement.x > 0.1f || movement.x < -0.1f)
        {
            horizontalInput = movement.x * playerSpeed;
            Vector3 scale = new Vector3(movement.x, transform.localScale.y, transform.localScale.z);
            transform.localScale = scale;
        }
        else
        {
            horizontalInput = 0;
        }
    }

    private void MoveCharacter()
    {
        float velocidadHorizontal = rb.velocity.x;
        velocidadHorizontal += horizontalInput;

        if (Mathf.Abs(horizontalInput) < 0.01f && isGrounded) // si paramos
            velocidadHorizontal *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.fixedDeltaTime * 10f);
        else if (Mathf.Sign(horizontalInput) != Mathf.Sign(velocidadHorizontal) && isGrounded) // si cambiamos de dirección
            velocidadHorizontal *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.fixedDeltaTime * 10f);
        else if (!isGrounded) // si estoy en el aire
            velocidadHorizontal *= Mathf.Pow(1f - horizontalDampingWhenInAir, Time.fixedDeltaTime * 10f);
        else
            velocidadHorizontal *= Mathf.Pow(1f - horizontalDampingBasic, Time.fixedDeltaTime * 10f);

        rb.velocity = new Vector2(velocidadHorizontal, rb.velocity.y);

        //Para activar Animator
        animator.SetFloat("Velocidad", Mathf.Abs(velocidadHorizontal));
    }

    private void Jump(Vector2 direction)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(direction * jumpForce, ForceMode2D.Impulse);


        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
        isJumping = true;

        //Animator
        animator.SetTrigger("Saltar");

    }

    void Fall() // mejoras en la caida
    {

        if (rb.velocity.y < 0) // Si estamos cayendo del salto, añadimos multiplicador de gravedad
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier) * Time.fixedDeltaTime;
            estaCayendo = true;
        }
        else if (rb.velocity.y > 0 && playerControls.Player.Saltar.phase == InputActionPhase.Canceled) // si estamos aún en subida del salto y ya hemos dejado de pulsar el botón, añadimos multiplicador pequeño
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpFallMultiplier / 2) * Time.fixedDeltaTime;

        }

    }

    private void SueloControl()
    {
        if (isGrounded)
        {
            estaCayendo = false;
            if (rb.velocity.y < 0)
            {
                extraJumpsValue = extraJumps;
                coyoteTimeCounter = coyoteTime;

            }
        }
        else
        {
            //ApplyAirLinearDrag();
            Fall();
            coyoteTimeCounter -= Time.fixedDeltaTime;
            if (rb.velocity.y < 0f) isJumping = false;
        }
    }
    #endregion

    #region INVENTARIO
    public void RecogerArma(string queArma, int quePuesto) //si quePuesto es 0 es la principal, si es 1 es la secundaria
    {
        //Añades el arma al diccionario
        //inventario.Add(queArma, );

        //Activas el arma que toca
        if (quePuesto == 0)
        {
            if (principalEnMano == null)
            {
                foreach (GameObject item in objetosInventario)
                {
                    if (item.name == queArma)
                    {
                        principalEnMano = item;
                        //Activar item (Arma)
                    }
                }

                principalEnMano.SetActive(true);
                principalEnMano.GetComponent<Objeto>().Activar();

            }

        }
        else if (quePuesto == 1)
        {
            if (secundariaEnMano == null)
            {
                foreach (GameObject item in objetosInventario)
                {
                    if (item.name == queArma)
                    {
                        secundariaEnMano = item;
                    }
                }

                secundariaEnMano.SetActive(true);
                secundariaEnMano.GetComponent<Objeto>().Activar();

            }
        }


    }

    public void SoltarArma(bool siEsPrincipal)
    {
       
        // Asegúrate de que haya un arma en la mano antes de intentar soltarla
        if (siEsPrincipal && principalEnMano != null)
        {
            // Desactiva el arma principal
            principalEnMano.SetActive(false);

             inventario.Remove(principalEnMano.name);

            // Establece la referencia a null ya que el arma se ha soltado
            principalEnMano = null;
        }
        else if (!siEsPrincipal && secundariaEnMano)
        {
            // Desactiva el arma secundaria
            secundariaEnMano.SetActive(false);
         
             inventario.Remove(secundariaEnMano.name);

            // Establece la referencia a null ya que el arma se ha soltado
            secundariaEnMano = null;

        }

        else if (siEsPrincipal && principalEnMano && secundariaEnMano)
        {
            // Desactiva el arma principal
            principalEnMano.SetActive(false);


            inventario.Remove(principalEnMano.name);

            // Establece la referencia a null ya que el arma se ha soltado
            principalEnMano = null;

            secundariaEnMano.SetActive(false);

            inventario.Remove(secundariaEnMano.name);

            // Establece la referencia a null ya que el arma se ha soltado
            secundariaEnMano = null;
        }
        else
        {
            Debug.LogWarning("No hay un arma en la mano para soltar.");
            // Si el jugador enemigo no tiene armas y se intenta soltar, puedes manejarlo de manera diferente
        }
    }

    #endregion

    #region COLLISIONS
    private void CheckCollisions()
    {
        //Ground Collisions
        isGrounded = Physics2D.Raycast(transform.position + groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer) ||
                     Physics2D.Raycast(transform.position - groundRaycastOffset, Vector2.down, groundRaycastLength, groundLayer);

        /*//Corner Collisions
        canCornerCorrect = Physics2D.Raycast(transform.position + edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
                           !Physics2D.Raycast(transform.position + innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) ||
                           Physics2D.Raycast(transform.position - edgeRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer) &&
                           !Physics2D.Raycast(transform.position - innerRaycastOffset, Vector2.up, topRaycastLength, cornerCorrectLayer);

        //Wall Collisions
        onWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer) ||
                 Physics2D.Raycast(transform.position, Vector2.left, wallRaycastLength, wallLayer);
        onRightWall = Physics2D.Raycast(transform.position, Vector2.right, wallRaycastLength, wallLayer);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (isGrounded)
        {
            Gizmos.color = Color.green;
        }
        //Ground Check
        Gizmos.DrawLine(transform.position + groundRaycastOffset, transform.position + groundRaycastOffset + Vector3.down * groundRaycastLength);
        Gizmos.DrawLine(transform.position - groundRaycastOffset, transform.position - groundRaycastOffset + Vector3.down * groundRaycastLength);

        /*//Corner Check
        Gizmos.DrawLine(transform.position + edgeRaycastOffset, transform.position + edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - edgeRaycastOffset, transform.position - edgeRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset, transform.position + innerRaycastOffset + Vector3.up * topRaycastLength);
        Gizmos.DrawLine(transform.position - innerRaycastOffset, transform.position - innerRaycastOffset + Vector3.up * topRaycastLength);

        //Corner Distance Check
        Gizmos.DrawLine(transform.position - innerRaycastOffset + Vector3.up * topRaycastLength,
                        transform.position - innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.left * topRaycastLength);
        Gizmos.DrawLine(transform.position + innerRaycastOffset + Vector3.up * topRaycastLength,
                        transform.position + innerRaycastOffset + Vector3.up * topRaycastLength + Vector3.right * topRaycastLength);

        //Wall Check
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * wallRaycastLength);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * wallRaycastLength);*/


    }
    #endregion

}

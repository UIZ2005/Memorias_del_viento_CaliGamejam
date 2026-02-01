using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movNiña : MonoBehaviour
{
  
    [Header("Movimiento")]
    public float moveSpeed;
    public float SpeedDash;
    private float BaseSpeed;

    [Header("Salto")]
    public float jumpForce; 

    [Header("Componentes")]
    public Rigidbody2D theRB;  

    [Header("DetectorPiso")]
    public Transform groundCheckPoint; 
    public LayerMask whatIsGround; 
    private bool isGrounded;

    [Header("Animator")]
    public Animator anim; 
    private SpriteRenderer theSR; 

    public float velocidadCaida;

    //activar el movimiento de la niña
    private bool puedeMoverse = false;

    public GameObject escudo1;
    public GameObject escudo2;

    //Dash
    [SerializeField] private float tiempoSprint;
    private float tiempoActualSprint;
    private float tiemposiguietneSprint;
    [SerializeField] private float tiempoEntreSprint;
    private bool puedeCorrer;
    private bool estaCorriendo;
    void Start()
    {
        anim = GetComponent<Animator>(); 
       theSR = GetComponent<SpriteRenderer>();
        anim.Play("dormida");
        BaseSpeed = moveSpeed;
        tiempoActualSprint = tiempoSprint;
    }

   
    void Update()
    {

        // Verificar si está en la animación de "dormida"
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool estaDurmiendo = stateInfo.IsName("dormida") && stateInfo.normalizedTime < 1f;


        // Si ya terminó de dormir, pero aún no puede moverse, forzar idle
        if (estaDurmiendo || !puedeMoverse)
        {
            anim.SetTrigger("despertar");
            return;
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeCorrer)
        {
            gameObject.GetComponent<PlayerManager>().enabled = false;
            anim.SetBool("dash", true);
            moveSpeed =SpeedDash;
            tiempoActualSprint = tiempoSprint;
            estaCorriendo = true;
        }
        // Reducir el tiempo de sprint mientras esté activo
        if (estaCorriendo)
        {
            tiempoActualSprint -= Time.deltaTime;

            // Si el sprint se agota, volver a la velocidad normal
            if (tiempoActualSprint <= 0)
            {
                anim.SetBool("dash", false);
                moveSpeed = BaseSpeed;
                estaCorriendo = false;
                puedeCorrer = false; // Bloquea el sprint hasta que se recargue
                tiemposiguietneSprint = Time.time + tiempoEntreSprint;
            }
        }

        // Recargar sprint después del tiempo de espera
        if (!puedeCorrer && Time.time >= tiemposiguietneSprint)
        {
            puedeCorrer = true;
        }
        theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

        // esta es para detectar cuando el jugador de se encuentra en el piso, para desues usarlo en el salto, y que el jugador solo pueda hacerlo cuando esta en el piso
        
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);


        // para detectar el salto es getbuttondown porqiue sucede solo cuando se oprime
          if (Input.GetButtonDown("Jump"))  
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                }
            }

        //para hacer el flip del personaje y mire hacia donde camina
            if (theRB.velocity.x < 0) // si la velocidad es menos, es decir va hacia la izquiera
            {
                theSR.flipX = true; // se hace el flip del personaje

            }
            else if (theRB.velocity.x > 0)
            {
                theSR.flipX = false;
            }

        bool activarSombrilla = !isGrounded && Input.GetMouseButton(0);
        anim.SetBool("sombrilla", activarSombrilla);

        if (activarSombrilla && theRB.velocity.y < 0)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, theRB.velocity.y * velocidadCaida); // cae más lento (40% de velocidad de caída)
        }

        bool activarEscudo = isGrounded && Input.GetMouseButton(1);
        anim.SetBool("escudo", activarEscudo);
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        if (Input.GetMouseButton(1))
        {
            escudo1.SetActive(true);
            escudo2.SetActive(true);
        }
        else
        {
            escudo1.SetActive(false);
            escudo2.SetActive(false);
        }



        // escudo camina

        anim.SetBool("escudo", activarEscudo); // Detecta si se está usando el escudo
        anim.SetFloat("moveSpeed", Mathf.Abs(theRB.velocity.x)); // Detecta si se está moviendo

    }

    public void ActivarMovimiento()
    {
        puedeMoverse = true;
    }
}

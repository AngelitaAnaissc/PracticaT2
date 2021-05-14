using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NinjaController : MonoBehaviour
{
    public Text PuntajeText;
    public int puntaje = 0;
    public Text VidasText;
    public int vidas = 3;

    public float fuerzaSalto = 40;
    public float velocidad = 10;

    public float altura = 0;

    private bool EstaSaltando = false;
    private bool EstoyEnLaEscaler = false;
    private bool EstaMuerto = false;
    private bool EstaPlaneando = false;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    public GameObject Bala;
    public Transform BalaTransform;

    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_CORRER = 1;
    private const int ANIMATION_SALTAR = 2;
    private const int ANIMATION_TREPAR = 3;
    private const int ANIMATION_PLANEAR = 4;
    private const int ANIMATION_DESLIZAR = 5;
    private const int ANIMATION_MORIR = 6;

    void Start()
    {
        Debug.Log("Esto se crea una unica vez");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (vidas <= 0) { EstaMuerto = true; }
        if (EstaMuerto != true)
        {
            PuntajeText.text="Puntaje : "+puntaje;
            VidasText.text = "Vidas : " + vidas;

            if (EstoyEnLaEscaler == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    rb.gravityScale = 0.5f;
                    CambiarAnimacion(ANIMATION_PLANEAR);
                    EstaPlaneando = true;
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.Space) && !EstaSaltando)
                    {
                        CambiarAnimacion(ANIMATION_SALTAR);
                        Saltar();
                        EstaSaltando = true;
                    }


                    if (Input.GetKey(KeyCode.RightArrow))
                    {
                        rb.velocity = new Vector2(velocidad, rb.velocity.y);
                        CambiarAnimacion(ANIMATION_CORRER);
                        spriteRenderer.flipX = false;

                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                        CambiarAnimacion(ANIMATION_CORRER);
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        CambiarAnimacion(ANIMATION_QUIETO);
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        Instantiate(Bala, BalaTransform.position, Quaternion.identity);
                    }

                    rb.gravityScale = 9;
                }


                if (Input.GetKey(KeyCode.C) && spriteRenderer.flipX == false)
                {
                    rb.velocity = new Vector2(velocidad, rb.velocity.y);
                    CambiarAnimacion(ANIMATION_DESLIZAR);
                }
                else if (Input.GetKey(KeyCode.C) && spriteRenderer.flipX == true)
                {
                    rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                    CambiarAnimacion(ANIMATION_DESLIZAR);
                }
            }

            if (EstoyEnLaEscaler == true)
            {
                rb.gravityScale = 0;
                CambiarAnimacion(ANIMATION_TREPAR);
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    CambiarAnimacion(ANIMATION_TREPAR);
                    rb.velocity = Vector2.up * velocidad;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    CambiarAnimacion(ANIMATION_TREPAR);
                    rb.velocity = Vector2.down * velocidad;
                }
                else
                {
                    rb.velocity = Vector2.up * 0;
                }
            }
        }
        else
        {
            CambiarAnimacion(ANIMATION_MORIR);
        }

    }
    private void Saltar()
    {
        rb.velocity = Vector2.up * fuerzaSalto;
    }

    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Escalera")
        {
            EstoyEnLaEscaler = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        if (collision.gameObject.tag == "Suelo")
        {
            if (altura >= 10 && EstaPlaneando==false)
            {
                altura = 0;
                EstaMuerto = true;
            }
            rb.gravityScale = 9;
            EstaSaltando = false;
            EstoyEnLaEscaler = false;
        }
        if (collision.gameObject.tag == "Final")
        {
            EstoyEnLaEscaler = false;
            rb.gravityScale = 9;
            EstaPlaneando = false;
        }
        if (collision.gameObject.tag == "SueloPiso2")
        {
            rb.gravityScale = 9;
            EstaSaltando = false;
            altura = 1.90f;
        }
        if (collision.gameObject.tag == "SueloPiso3")
        {
            rb.gravityScale = 9;
            EstaSaltando = false;
            altura = 10f;
        }

        if (collision.gameObject.tag == "Destruible")
        {
            vidas--;
            Destroy(collision.gameObject);
        }
    }
}

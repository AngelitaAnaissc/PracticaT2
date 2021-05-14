using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{ 
    public GameObject Zombies;

public float velocidad = 3;

private SpriteRenderer spriteRenderer;
private Animator animator;
private Rigidbody2D rb;


    
    void Start()
{
    Debug.Log("Esto se crea una unica vez");
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
        
        InvokeRepeating("Creando", 4, 0f);
}


void Update()
{

    rb.velocity = new Vector2(-velocidad, rb.velocity.y);
    spriteRenderer.flipX = true;

}
public void CambiarAnimacion(int animacion)
{
    animator.SetInteger("Estado", animacion);
}
public void Creando()
{
    Vector3 SpawnPosition = new Vector3(24.37f, -6.39f, 0);
    GameObject Zombie = Instantiate(Zombies, SpawnPosition, Quaternion.identity);
}

private void OnCollisionEnter2D(Collision2D collision)
{

}



}
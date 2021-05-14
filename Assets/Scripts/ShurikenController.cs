using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{ 
private GameObject Ninja;
public float velocidad = 10f;
private Rigidbody2D rb;


void Start()
{
    rb = GetComponent<Rigidbody2D>();
    Destroy(this.gameObject, 2f);
    Ninja = GameObject.Find("Ninja");

}

 
void Update()
{

    rb.velocity = new Vector2(velocidad, rb.velocity.y);
}

private void OnCollisionEnter2D(Collision2D collision)
{

    if (collision.gameObject.tag == "Destruible")
    {
            Ninja.GetComponent<NinjaController>().puntaje += 10;
        Destroy(collision.gameObject);
            Destroy(this.gameObject);
    }

}
}

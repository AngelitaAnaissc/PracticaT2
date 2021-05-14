using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public GameObject Jugador;

    private Transform _transform;
   
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector3(Jugador.transform.position.x, _transform.position.y, _transform.position.z);

    }
}

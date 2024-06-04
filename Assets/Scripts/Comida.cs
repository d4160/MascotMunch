using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Comida : MonoBehaviour
{
    public string nombre;
    public float velocidadInicial;
    public float velocidadMaxima;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rb.velocity = Vector3.back * velocidadInicial;
    }

    public void AumentarVelocidad(float aumento)
    {
        _rb.velocity += Vector3.back * aumento;
        if (Mathf.Abs(_rb.velocity.z) > velocidadMaxima)
        {
            _rb.velocity = Vector3.back * velocidadMaxima;
        }
    }
}

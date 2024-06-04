using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mascota : MonoBehaviour
{
    public string nombre;
    public Comida comidaFavorita;
    public Sprite icon;
    public string teclaRapida;

    [Header("Audio")]
    public AudioClip[] sonidosCorrectos;
    public AudioClip[] sonidosIncorrectos;
    public AudioClip sonidoClic;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        Comida comida = other.attachedRigidbody.GetComponent<Comida>();

        if (comida.nombre == comidaFavorita.nombre)
        {
            PlayMascotaSonidoCorrecto();
            _animator.SetTrigger("Jump");
            PlayerControlador.Instance.AgregarPuntos(1);
        }
        else
        {
            PlayMascotaSonidoIncorrecto();
            _animator.SetTrigger("Hit");
            PlayerControlador.Instance.QuitarVidas(1);
        }

        Destroy(comida.gameObject);
    }

    private void PlayMascotaSonidoCorrecto()
    {
        GameControlador.Instance.PlaySonidoSFX(ObtenerSonidoCorrecto());
    }

    private void PlayMascotaSonidoIncorrecto()
    {
        GameControlador.Instance.PlaySonidoSFX(ObtenerSonidoIncorrecto());
    }

    private AudioClip ObtenerSonidoCorrecto()
    {
        return sonidosCorrectos[Random.Range(0, sonidosCorrectos.Length)];
    }

    private AudioClip ObtenerSonidoIncorrecto()
    {
        return sonidosIncorrectos[Random.Range(0, sonidosIncorrectos.Length)];
    }
}

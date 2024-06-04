using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlador : MonoBehaviour
{
    public Mascota[] mascotas;
    public int vidasTotales = 3;

    private int _puntosTotales;
    private int _vidasRestantes;

    public int PuntosTotales => _puntosTotales;
    public int VidasRestantes => _vidasRestantes;

    public static PlayerControlador Instance { get; private set; }

    public event Action<int> OnAgregarPuntos;
    public event Action<int> OnQuitarVidas;

    private bool _activo = false;

    void Awake()
    {
        Instance = this;

        _vidasRestantes = vidasTotales;
        _puntosTotales = 0;
    }

    void Start()
    {
        ActivarMascota(ObtenerMascotaIndice());
    }

    void Update()
    {
        if (!_activo) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayMascotaSonidoClic(0);
            ActivarMascota(0);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            PlayMascotaSonidoClic(1);
            ActivarMascota(1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            PlayMascotaSonidoClic(2);
            ActivarMascota(2);
        }
    }

    private void PlayMascotaSonidoClic(int indice)
    {
        GameControlador.Instance.PlaySonidoUI(mascotas[indice].sonidoClic);
    }

    public void ActivarMascota(int indice)
    {
        for (int i = 0; i < mascotas.Length; i++)
        {
            mascotas[i].gameObject.SetActive(indice == i);
        }
    }

    public void ActivarMascota(Mascota mascota)
    {
        for (int i = 0; i < mascotas.Length; i++)
        {
            mascotas[i].gameObject.SetActive(mascota.nombre == mascotas[i].nombre);
        }
    }

    public void AgregarPuntos(int puntos)
    {
        _puntosTotales += puntos;

        OnAgregarPuntos?.Invoke(_puntosTotales);
    }

    public void QuitarVidas(int cantidad)
    {
        _vidasRestantes -= cantidad;

        if (_vidasRestantes == 0)
        {
            GameControlador.Instance.FinJuego();
        }
        else if (_vidasRestantes < 0)
        {
            _vidasRestantes = 0;
        }

        OnQuitarVidas?.Invoke(_vidasRestantes);
    }

    public int ObtenerMascotaIndice()
    {
        return UnityEngine.Random.Range(0, mascotas.Length);
    }

    public void IniciarJuego()
    {
        _puntosTotales = 0;
        OnAgregarPuntos?.Invoke(_puntosTotales);

        _vidasRestantes = vidasTotales;
        OnQuitarVidas?.Invoke(_vidasRestantes);

        _activo = true;
    }

    public void FinJuego()
    {
        UGSControlador.Instance.AddPlayerScore(_puntosTotales);
        _activo = false;
    }
}

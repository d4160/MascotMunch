using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI tiempoTexto;
    public TextMeshProUGUI vidasTexto;
    public TextMeshProUGUI puntajeTexto;

    void Start()
    {
        puntajeTexto.text = PlayerControlador.Instance.PuntosTotales.ToString();
        vidasTexto.text = PlayerControlador.Instance.VidasRestantes.ToString();

        PlayerControlador.Instance.OnAgregarPuntos += PlayerControlador_OnAgregarPuntos;

        PlayerControlador.Instance.OnQuitarVidas += PlayerControlador_OnQuitarVidas;
    }

    void OnDestroy()
    {
        PlayerControlador.Instance.OnAgregarPuntos -= PlayerControlador_OnAgregarPuntos;

        PlayerControlador.Instance.OnQuitarVidas -= PlayerControlador_OnQuitarVidas;
    }

    void Update()
    {
        tiempoTexto.text = GameControlador.Instance.ObtenerTiempoFormateado();
    }

    private void PlayerControlador_OnAgregarPuntos(int puntos)
    {
        puntajeTexto.text = puntos.ToString();
    }

    private void PlayerControlador_OnQuitarVidas(int vidasRestantes)
    {
        vidasTexto.text = vidasRestantes.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MascotaUI : MonoBehaviour
{
    [Header("UI")]
    public Button boton;
    public Image mascotaImagen;
    public TextMeshProUGUI teclaRapidaTexto;

    public Mascota Mascota { get; internal set; }
    public PlayerControlador PlayerControlador { get; internal set; }

    void Awake()
    {
        boton.onClick.AddListener(() =>
        {
            GameControlador.Instance.PlaySonidoUI(Mascota.sonidoClic);
            PlayerControlador.ActivarMascota(Mascota);
        });
    }

    public void ActualizarUI()
    {
        if (Mascota)
        {
            mascotaImagen.sprite = Mascota.icon;
            teclaRapidaTexto.text = Mascota.teclaRapida;
        }
    }
}

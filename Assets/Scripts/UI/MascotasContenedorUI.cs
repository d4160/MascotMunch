using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MascotasContenedorUI : MonoBehaviour
{
    public Mascota[] mascotas;
    public MascotaUI[] mascotaUIs;
    public PlayerControlador playerControlador;

    void Start()
    {
        for (int i = 0; i < mascotas.Length; i++)
        {
            mascotaUIs[i].Mascota = mascotas[i];
            mascotaUIs[i].PlayerControlador = playerControlador;
            mascotaUIs[i].ActualizarUI();
        }
    }
}

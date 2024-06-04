using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpcionesUI : MonoBehaviour
{
    [Header("UI")]
    public Button botonPause;

    void Awake()
    {
        botonPause.onClick.AddListener(() =>
        {
            GameControlador.Instance.Pause();
        });
    }
}

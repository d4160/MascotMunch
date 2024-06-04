using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NombrePopup : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField inputNombre;
    public Button botonJugar;
    public Button botonCerrar;

    void Awake()
    {
        botonJugar.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(inputNombre.text))
            {
                UGSControlador.Instance.UpdatePlayerName(inputNombre.text.Replace(' ', '_'));
            }

            gameObject.SetActive(false);
            GameControlador.Instance.IniciarJuego();
        });
    }

    void OnEnable()
    {
        inputNombre.text = UGSControlador.Instance.PlayerName;

        UGSControlador.Instance.OnPlayerNameUpdated += UGSControlador_OnPlayerNameUpdated;
    }

    void OnDisable()
    {
        UGSControlador.Instance.OnPlayerNameUpdated -= UGSControlador_OnPlayerNameUpdated;
    }

    private void UGSControlador_OnPlayerNameUpdated(string nombre)
    {
        inputNombre.text = nombre.Split('#')[0];
    }
}

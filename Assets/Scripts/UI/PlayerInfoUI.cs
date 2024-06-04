using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI nombreTexto;
    public TextMeshProUGUI highscoreTexto;
    public Slider vidasSlider;

    void Start()
    {
        nombreTexto.text = UGSControlador.Instance.PlayerName;

        if (UGSControlador.Instance.PlayerHighscore != null)
        {
            highscoreTexto.text = UGSControlador.Instance.PlayerHighscore.Score.ToString();
        }
        else
        {
            highscoreTexto.text = "0";
        }

        UGSControlador.Instance.OnPlayerNameUpdated += UGSControlador_OnPlayerNameUpdated;

        UGSControlador.Instance.OnHighscoreUpdated += UGSControlador_OnHighscoreUpdated;

        PlayerControlador.Instance.OnQuitarVidas += PlayerControlador_OnQuitarVidas;
    }

    void OnDestroy()
    {
        UGSControlador.Instance.OnPlayerNameUpdated -= UGSControlador_OnPlayerNameUpdated;

        UGSControlador.Instance.OnHighscoreUpdated -= UGSControlador_OnHighscoreUpdated;

        PlayerControlador.Instance.OnQuitarVidas -= PlayerControlador_OnQuitarVidas;
    }

    private void UGSControlador_OnPlayerNameUpdated(string nombre)
    {
        nombreTexto.text = nombre.Split('#')[0];
    }

    private void UGSControlador_OnHighscoreUpdated(LeaderboardEntry entry, bool newRecord)
    {
        if (entry != null)
        {
            highscoreTexto.text = entry.Score.ToString();

            if (newRecord)
            {
                GameControlador.Instance.nuevoRecordPopup.SetActive(true);
            }
        }
    }

    private void PlayerControlador_OnQuitarVidas(int vidas)
    {
        vidasSlider.value = (float)vidas / PlayerControlador.Instance.vidasTotales;
    }
}

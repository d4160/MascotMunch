using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class RankingEntryUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI rankTexto;
    public TextMeshProUGUI playerNameTexto;
    public TextMeshProUGUI scoreTexto;

    public void ActualizarUI(LeaderboardEntry entry)
    {
        if (entry == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if (rankTexto)
        {
            rankTexto.text = (entry.Rank + 1).ToString();
        }
        playerNameTexto.text = entry.PlayerName.Split('#')[0];
        scoreTexto.text = entry.Score.ToString();

        gameObject.SetActive(true);
    }
}

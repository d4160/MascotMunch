using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;

public class RankingWindow : MonoBehaviour
{
    public RankingEntryUI entryUIPrefab;
    public Transform parentTransform;

    [Header("UI")]
    public Button botonAtras;

    [Header("Referencias")]
    public GameObject pausePopup;
    public GameObject gameOverPopup;
    public RankingEntryUI playerEntryUI;
    public RankingEntryUI[] top3EntriesUI;

    private List<RankingEntryUI> _entriesInstances = new();

    void Awake()
    {
        botonAtras.onClick.AddListener(() =>
        {
            if (GameControlador.Instance.IsGameOver)
            {
                gameOverPopup.SetActive(true);
            }
            else
            {
                pausePopup.SetActive(true);
            }

            gameObject.SetActive(false);
        });
    }

    async void OnEnable()
    {
        DisableEntriesInstances();

        LeaderboardScoresPage scoresPage = await UGSControlador.Instance.GetScoresAsync();

        LeaderboardEntry playerEntry = await UGSControlador.Instance.GetPlayerScoreAsync();

        playerEntryUI.ActualizarUI(playerEntry);

        for (int i = 0; i < scoresPage.Results.Count; i++)
        {
            if (i < top3EntriesUI.Length)
            {
                top3EntriesUI[i].ActualizarUI(scoresPage.Results[i]);
            }
            else
            {
                RankingEntryUI newEntry = GetEntryInstance();
                newEntry.ActualizarUI(scoresPage.Results[i]);

                _entriesInstances.Add(newEntry);
            }
        }
    }

    private RankingEntryUI GetEntryInstance()
    {
        for (int i = 0; i < _entriesInstances.Count; i++)
        {
            if (!_entriesInstances[i].gameObject.activeSelf)
            {
                return _entriesInstances[i];
            }
        }

        return Instantiate(entryUIPrefab, parentTransform, false);
    }

    private void DisableEntriesInstances()
    {
        entryUIPrefab.gameObject.SetActive(false);
        playerEntryUI.gameObject.SetActive(false);

        for (int i = 0; i < top3EntriesUI.Length; i++)
        {
            top3EntriesUI[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _entriesInstances.Count; i++)
        {
            _entriesInstances[i].gameObject.SetActive(false);
        }
    }
}

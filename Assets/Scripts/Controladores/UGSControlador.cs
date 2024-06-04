using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class UGSControlador : MonoBehaviour
{
    const string LeaderboardId = "Highscore";

    private string _playerName;

    public string PlayerName
    {
        get => !string.IsNullOrEmpty(_playerName) ? _playerName.Split('#')[0] : string.Empty;
        private set => _playerName = value;
    }
    public LeaderboardEntry PlayerHighscore { get; private set; }

    public static UGSControlador Instance { get; private set; }

    public event Action<string> OnPlayerNameUpdated;
    public event Action<LeaderboardEntry, bool> OnHighscoreUpdated;

    async void Awake()
    {
        Instance = this;

        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            PlayerName = await GetPlayerNameAsync();
            OnPlayerNameUpdated?.Invoke(PlayerName);

            PlayerHighscore = await GetPlayerScoreAsync();
            OnHighscoreUpdated?.Invoke(PlayerHighscore, false);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async Task<string> GetPlayerNameAsync()
    {
        return await AuthenticationService.Instance.GetPlayerNameAsync();
    }

    public async void UpdatePlayerName(string newName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(newName);

        PlayerName = newName;
        OnPlayerNameUpdated?.Invoke(PlayerName);
    }

    public async void AddPlayerScore(int puntajeTotal)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, puntajeTotal);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));

        bool newRecord = false;

        if ((PlayerHighscore == null && scoreResponse != null) || scoreResponse.Score > PlayerHighscore.Score)
        {
            newRecord = true;
        }

        PlayerHighscore = scoreResponse;
        OnHighscoreUpdated?.Invoke(PlayerHighscore, newRecord);
    }

    public async Task<LeaderboardEntry> GetPlayerScoreAsync()
    {
        try
        {
            var scoreResponse =
                await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));

            return scoreResponse;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<LeaderboardScoresPage> GetScoresAsync()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions()
            {
                Limit = 100
            });

        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        return scoresResponse;
    }
}

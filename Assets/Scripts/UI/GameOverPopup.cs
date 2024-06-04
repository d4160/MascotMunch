using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject rankingWindow;

    [Header("UI")]
    public Button botonRanking;
    public Button botonContinue;
    public Button botonQuit;

    void Awake()
    {
        botonRanking.onClick.AddListener(() =>
        {
            rankingWindow.SetActive(true);
            gameObject.SetActive(false);
        });

        botonContinue.onClick.AddListener(() =>
        {
            GameControlador.Instance.Restart();
        });

        botonQuit.onClick.AddListener(() =>
        {
            GameControlador.Instance.Quit();
        });
    }
}

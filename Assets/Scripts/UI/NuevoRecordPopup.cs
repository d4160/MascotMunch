using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NuevoRecordPopup : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject popupGameOver;

    [Header("UI")]
    public Button botonCerrar;

    void Awake()
    {
        botonCerrar.onClick.AddListener(() =>
        {
            popupGameOver.SetActive(true);
            gameObject.SetActive(false);
        });
    }

    void OnEnable()
    {
        popupGameOver.SetActive(false);
    }
}

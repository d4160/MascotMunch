using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlador : MonoBehaviour
{
    public bool iniciarJuegoAutomaticamente = false;
    public float tiempoParaMaximaDificultad = 31f;
    public AnimationCurve curvaAumentoVelocidad;
    public float maximoAumentoVelocidad = 7;
    public AnimationCurve curvaDisminucionEspera;
    public float maximoDisminucionEspera = 1f;

    [Header("Referencias")]
    public Instanciador instanciador;
    public GameObject nuevoRecordPopup;
    public GameObject pausePopup;
    public GameObject gameOverPopup;

    [Header("Audio")]
    public AudioSource uiAudioSource;
    public AudioSource sfxAudioSource;

    private float _tiempoTranscurrido;
    private bool _musicaFinalCambiada;
    private bool _activo;

    public bool IsGameOver => !_activo;

    public static GameControlador Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (iniciarJuegoAutomaticamente)
            IniciarJuego();
    }

    void Update()
    {
        if (!_activo) return;

        _tiempoTranscurrido += Time.deltaTime;

        instanciador.AumentarVelocidad(curvaAumentoVelocidad.Evaluate(_tiempoTranscurrido / tiempoParaMaximaDificultad) * maximoAumentoVelocidad);

        instanciador.DisminuirTiempoEspera(curvaDisminucionEspera.Evaluate(_tiempoTranscurrido / tiempoParaMaximaDificultad) * maximoDisminucionEspera);

        if (!_musicaFinalCambiada && _tiempoTranscurrido >= tiempoParaMaximaDificultad / 2)
        {
            MusicaControlador.Instance.CambiarMusicaFinal();
            _musicaFinalCambiada = true;
        }
    }

    public void IniciarJuego()
    {
        _musicaFinalCambiada = false;
        _tiempoTranscurrido = 0f;
        _activo = true;

        pausePopup.SetActive(false);
        gameOverPopup.SetActive(false);

        PlayerControlador.Instance.IniciarJuego();
        MusicaControlador.Instance.IniciarJuego();
        instanciador.IniciarJuego();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pausePopup.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pausePopup.SetActive(false);
    }

    public void Restart()
    {
        Continue();
        // SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        IniciarJuego();
    }

    public void Quit()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Restart();
#elif UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else

        Application.Quit();
#endif
    }

    public void FinJuego()
    {
        _activo = false;
        gameOverPopup.SetActive(true);

        PlayerControlador.Instance.FinJuego();
        MusicaControlador.Instance.FinJuego();
        instanciador.FinJuego();
    }

    public string ObtenerTiempoFormateado()
    {
        float minutos = (int)(_tiempoTranscurrido / 60);
        float segundos = (int)(_tiempoTranscurrido % 60);

        return $"{(minutos < 10 ? $"0{minutos}" : minutos)}:{(segundos < 10 ? $"0{segundos}" : segundos)}";
    }

    public void PlaySonidoUI(AudioClip clip)
    {
        uiAudioSource.PlayOneShot(clip);
    }

    public void PlaySonidoSFX(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}

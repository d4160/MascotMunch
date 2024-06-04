using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaControlador : MonoBehaviour
{
    public AudioClip[] musicasIniciales;
    public AudioClip[] musicasFinales;
    public float duracionFade = 1.31f;

    [Header("Referencias")]
    public AudioSource musicaAudioSource;

    public static MusicaControlador Instance { get; private set; }

    private bool _fadingOut;
    private bool _fadingIn;
    private AudioClip _nextMusic;

    void Awake()
    {
        Instance = this;
        musicaAudioSource.clip = ObtenerMusicaInicial();
        musicaAudioSource.Play();
    }

    void Update()
    {
        if (_fadingOut)
        {
            musicaAudioSource.volume = Mathf.MoveTowards(musicaAudioSource.volume, 0, Time.deltaTime * duracionFade);

            if (musicaAudioSource.volume <= 0.001f)
            {
                musicaAudioSource.volume = 0;
                _fadingOut = false;
                _fadingIn = true;
                musicaAudioSource.clip = _nextMusic;
                musicaAudioSource.Stop();
                musicaAudioSource.Play();
            }
        }

        if (_fadingIn)
        {
            musicaAudioSource.volume = Mathf.MoveTowards(musicaAudioSource.volume, 1, Time.deltaTime * duracionFade);

            if (musicaAudioSource.volume >= 0.991f)
            {
                musicaAudioSource.volume = 1;
                _fadingOut = false;
                _fadingIn = false;
            }
        }
    }

    private AudioClip ObtenerMusicaInicial()
    {
        return musicasIniciales[Random.Range(0, musicasIniciales.Length)];
    }

    private AudioClip ObtenerMusicaFinal()
    {
        return musicasFinales[Random.Range(0, musicasFinales.Length)];
    }

    public void CambiarMusicaInicial()
    {
        _nextMusic = ObtenerMusicaInicial();
        _fadingOut = true;
    }

    public void CambiarMusicaFinal()
    {
        _nextMusic = ObtenerMusicaFinal();
        _fadingOut = true;
    }

    public void IniciarJuego()
    {
        CambiarMusicaInicial();
    }

    public void FinJuego()
    {
        CambiarMusicaInicial();
    }
}

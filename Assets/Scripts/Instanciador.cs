using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour
{
    public Comida[] comidas;
    public Vector2 tiempoEsperaMinMax = new Vector2(1.77f, 3.1f);
    public float aumentoVelocidad = 0;

    private float _disminucionEspera = 0;
    private Coroutine _rutinaBucle;
    private List<Comida> _comidasInstanciadas = new();

    private IEnumerator InstanciarCo()
    {
        while (true)
        {
            Instanciar();

            yield return new WaitForSeconds(Random.Range(tiempoEsperaMinMax.x - _disminucionEspera, tiempoEsperaMinMax.y - _disminucionEspera));
        }
    }

    private void Instanciar()
    {
        int aleatorio = Random.Range(0, comidas.Length);
        Comida nuevaComida = Instantiate(comidas[aleatorio], this.transform, false);
        nuevaComida.AumentarVelocidad(aumentoVelocidad);

        _comidasInstanciadas.Add(nuevaComida);
    }

    private void DestroyAllComidas()
    {
        for (int i = 0; i < _comidasInstanciadas.Count; i++)
        {
            if (_comidasInstanciadas[i])
            {
                Destroy(_comidasInstanciadas[i].gameObject);
            }
        }

        _comidasInstanciadas.Clear();
    }

    public void IniciarJuego()
    {
        DestroyAllComidas();

        if (_rutinaBucle == null)
        {
            _rutinaBucle = StartCoroutine(InstanciarCo());
        }
    }

    public void FinJuego()
    {
        if (_rutinaBucle != null)
        {
            StopCoroutine(_rutinaBucle);
            _rutinaBucle = null;
        }

        DestroyAllComidas();
    }

    public void DisminuirTiempoEspera(float disminucion)
    {
        _disminucionEspera = disminucion;
    }

    public void AumentarVelocidad(float aumento)
    {
        aumentoVelocidad = aumento;
    }
}

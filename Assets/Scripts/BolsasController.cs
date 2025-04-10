using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BolsasController : MonoBehaviour
{
    public GameObject bolsaPrefab;
    public Transform spawnPoint;
    public float tiempoEntreBolsas = 2f;
    public Text puntosTexto;
    public Image[] corazones;

    private float timer;
    private int vidas = 3;
    private int puntos = 0;
    private int bolsasEnJugador = 0;

    public Transform jugador;
    public float distanciaParaDepositar = 1f;
    public Transform posicionBolsasVisual;
    public GameObject bolsaVisualPrefab;

    private List<GameObject> bolsasVisuales = new List<GameObject>();

    void Start()
    {
        SFXManager2.Instance.ReproducirAmbienteJuego();
        timer = tiempoEntreBolsas;
        ActualizarPuntos();
    }

    void Update()
    {
    timer -= Time.deltaTime;
    if (timer <= 0)
    {
        GenerarBolsa();

        // Mayor intensidad: bolsa cada vez más rápido hasta mínimo 0.2 segundos
        tiempoEntreBolsas = Mathf.Max(0.2f, tiempoEntreBolsas - 0.05f);
        timer = tiempoEntreBolsas;
    }

    RevisarDepositoDeBolsas();
    }

    void GenerarBolsa()
    {
        GameObject nuevaBolsa = Instantiate(bolsaPrefab, spawnPoint.position, Quaternion.identity);

        // Definir rango horizontal aleatorio donde puede caer la bolsa
        float xMin = -10f;
        float xMax = -2f;
        float yDestino = jugador.position.y;

        Vector3 destinoAleatorio = new Vector3(Random.Range(xMin, xMax), yDestino, 0);
        nuevaBolsa.AddComponent<MovimientoParabolico>().destino = destinoAleatorio;
        nuevaBolsa.AddComponent<CircleCollider2D>().isTrigger = true;
        nuevaBolsa.AddComponent<Bolsa>();
        nuevaBolsa.tag = "Bolsa";
    }

    void RevisarDepositoDeBolsas()
    { 
        if (jugador.position.x < -12f && bolsasEnJugador > 0)
        {
            SFXManager2.Instance.ReproducirBolsaPerdida();
            puntos += bolsasEnJugador;
            bolsasEnJugador = 0;
            ActualizarPuntos();

            foreach (GameObject bolsa in bolsasVisuales)
                Destroy(bolsa);
            bolsasVisuales.Clear();
        }
    }

    void ActualizarPuntos()
    {
        if (puntosTexto != null)
            puntosTexto.text = "PUNTOS: " + puntos;
    }

    public void AgarrarBolsa(GameObject bolsa)
    {
        Destroy(bolsa);
        bolsasEnJugador++;

        if (bolsaVisualPrefab != null && posicionBolsasVisual != null)
        {
            Vector3 offset = new Vector3(0, 0.5f * bolsasVisuales.Count, 0);
            GameObject visual = Instantiate(bolsaVisualPrefab, posicionBolsasVisual.position + offset, Quaternion.identity, jugador);
            bolsasVisuales.Add(visual);
        }

        if (bolsasEnJugador > 3)
        {
            PerderVida();
            bolsasEnJugador = 0;

            foreach (GameObject bolsaVis in bolsasVisuales)
                Destroy(bolsaVis);
            bolsasVisuales.Clear();
        }
    }

    public void BolsaPerdida()
    {
        PerderVida();
    }

    void PerderVida()
    {
        SFXManager2.Instance.ReproducirPerderVida();

        vidas--;
        if (vidas >= 0 && vidas < corazones.Length)
            corazones[vidas].enabled = false;

        if (vidas <= 0)
        {
            // Guardamos los puntos finales para mostrarlos en la escena final
            PlayerPrefs.SetInt("PuntosFinales", puntos);
            PlayerPrefs.Save();
            Debug.Log("Juego Terminado");
            Time.timeScale = 0;

            SceneManager.LoadScene("EndScene2");
        }
    }
}

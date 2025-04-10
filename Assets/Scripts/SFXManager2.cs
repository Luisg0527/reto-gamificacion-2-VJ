using UnityEngine;

public class SFXManager2 : MonoBehaviour
{
    public static SFXManager2 Instance;

    [Header("Audio Source")]
    public AudioSource ambienteSource;
    public AudioSource efectosSource;

    [Header("Audios - Ambiente")]
    public AudioClip ambienteMenu;
    public AudioClip ambienteJuego;
    public AudioClip ambienteFinal;

    [Header("Audios - Juego")]
    public AudioClip perderVida;
    public AudioClip bolsaPerdida;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void ReproducirAmbienteMenu()
    {
        ambienteSource.clip = ambienteMenu;
        ambienteSource.loop = true;
        ambienteSource.Play();
    }

    public void ReproducirAmbienteJuego()
    {
        ambienteSource.clip = ambienteJuego;
        ambienteSource.loop = true;
        ambienteSource.Play();
    }

    public void ReproducirAmbienteFinal()
    {
        ambienteSource.clip = ambienteFinal;
        ambienteSource.loop = true;
        ambienteSource.Play();
    }

    public void ReproducirPerderVida()
    {
        efectosSource.PlayOneShot(perderVida);
    }

    public void ReproducirBolsaPerdida()
    {
        efectosSource.PlayOneShot(bolsaPerdida);
    }

    public void PararAmbiente()
    {
        ambienteSource.Stop();
    }
}

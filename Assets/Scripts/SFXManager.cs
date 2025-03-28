using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //Audio al finalizar 5 preguntas
    public AudioClip final;
    //Audio durante el juego
    public AudioClip CorrAns;
    //Audio respuesta incorrecta
    public AudioClip IncorAns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finalSound()
    {
        AudioSource.PlayClipAtPoint(final,Camera.main.transform.position,0.5f);
    }


    public void respuestaCorrecta()
    {
        AudioSource.PlayClipAtPoint(CorrAns,Camera.main.transform.position,0.5f);
    }

    public void respuestaIncorrecta()
    {
        AudioSource.PlayClipAtPoint(IncorAns,Camera.main.transform.position,0.5f);
    }
}

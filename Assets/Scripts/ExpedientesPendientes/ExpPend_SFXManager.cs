using UnityEngine;

public class ExpPend_SFXManager : MonoBehaviour
{
    //Audio durante el juego
    public AudioClip AngryOut;
    //Audio respuesta incorrecta
    public AudioClip PhoneCall;
    public AudioSource currSource; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AngryOutSound()
    {
        AudioSource.PlayClipAtPoint(AngryOut,Camera.main.transform.position,0.5f);
    }

    public void PhoneCallSound()
    {
        //AudioSource.PlayClipAtPoint(PhoneCall,Camera.main.transform.position,0.5f);
        currSource.clip = PhoneCall;
        currSource.volume = 0.5f;
        currSource.Play();
    }

    public void StopPhoneCall()
    {
        currSource.Stop();
    }

}

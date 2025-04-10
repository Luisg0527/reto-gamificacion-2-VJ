
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text preguntaText;
    public TMP_Text respuesta1Text;
    public TMP_Text respuesta2Text;
    public TMP_Text respuesta3Text;
    public TMP_Text respuesta4Text;

    private Coroutine timerCoroutine;

    int time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Si hay un tiempo guardado y es mayor a 0, continúa desde ahí
        if (PlayerPrefs.HasKey("SavedTime") && PlayerPrefs.GetInt("SavedTime") > 0)
        {
            time = PlayerPrefs.GetInt("SavedTime");
        }
        else
        {
            // Si no hay tiempo guardado o ya terminó el juego, empieza desde el tiempo inicial
            time = MariposaGameControl.Instance.ansTime;
        }

        ActiveText();
    }

    public void ActiveText()
    {
        timeText.text = time.ToString();
    }



    public void StartTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine); 

        time = PlayerPrefs.GetInt("ansTime", MariposaGameControl.Instance.ansTime); 
        ActiveText(); // Update the UI
        timerCoroutine = StartCoroutine(MatchTime()); 
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine); 
            timerCoroutine = null;
        }
    }

    IEnumerator MatchTime()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            ActiveText();
            PlayerPrefs.SetInt("SavedTime", time);
            PlayerPrefs.Save();  
        }

        PlayerPrefs.DeleteKey("SavedTime");
        MariposaGameControl.Instance.preguntaIncorrecta();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMenu()
    {
        PlayerPrefs.SetInt("SavedTime", time);  
        PlayerPrefs.Save(); 
        MariposaGameControl.Instance.GotoMenu();
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

}

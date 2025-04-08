using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpPend_UIControl : MonoBehaviour
{
    public Text timeText;
    public Text startText;
    public float DialogueSpeed = 0.05f;
    public float DelayToWrite = 0.5f;
    int time;
    public Button startBtn;
    public Image startBoard;

    // Define el tiempo y las vidas
    void Start()
    {
        StartCoroutine(WriteText());
        time = ExpPend_GameControl.instance.timeToWin;
        //ActiveText();
    }
    
    // Funcion para la impresion del tiempo
    public void ActiveText(){
        timeText.text = time.ToString();
    }

    // funcion para empezar a contar el tiempo
    public void StartTimer(){
        startBtn.gameObject.SetActive(false);
        startBoard.gameObject.SetActive(false);
        StartCoroutine(MatchTime());
    }

    // Funcion para contar en reversa y no contar en negativo
    IEnumerator MatchTime(){
        yield return new WaitForSeconds(1);
        time--;
        ActiveText();
        if (time == 0) {
            ExpPend_GameControl.instance.ActiveEndScene();
        }
        else {
            StartCoroutine(MatchTime());
        }
    }

    IEnumerator WriteText() {
        string welcomeText = "¿Tienes ojo para encontrar a la persona perfecta?\nEn este juego tendrás que revisar candidatos, investigar su información dando clic en sus datos y decidir si los contratas o no.\n¡Haz la mayor cantidad de contrataciones correctas antes de que el tiempo se acabe!";
        yield return new WaitForSeconds (DelayToWrite);
        foreach (char Character in welcomeText.ToCharArray())
        {
            startText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

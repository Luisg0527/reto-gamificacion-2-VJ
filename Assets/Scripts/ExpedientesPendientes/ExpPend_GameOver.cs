using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;

public class ExpPend_GameOver : MonoBehaviour
{
    public Text letterGrade;
    public Text coinsWonText;
    public Text resumenText;
    public Text message;

    private string[] messagesArray = {"¡Eres una leyenda del reclutamiento! Hasta el CEO te aplaudiría.", "¡Buen trabajo, reclutador estrella! Estás a un paso de impresionar al CEO.", "¡Ánimo, futuro talento del reclutamiento! Hasta los grandes empiezan con pequeños pasos."};

    private int coinsWon;

    void calculateScore() {
        int totalContrataciones = PlayerPrefs.GetInt("buenasContrataciones") + PlayerPrefs.GetInt("malasContrataciones");
        float score = (PlayerPrefs.GetInt("buenasContrataciones") / (float)totalContrataciones) * 100 ;

        Debug.Log("Score: " + score + ", b: " + PlayerPrefs.GetInt("buenasContrataciones") + ", m: " + PlayerPrefs.GetInt("malasContrataciones"));

        calculateScoreAux(score);

        resumenText.text = "Empleados Contratados: Contrataste a " + PlayerPrefs.GetInt("Contrataciones") + " empleados\nBuenas Contrataciones: Perfecto... " + PlayerPrefs.GetInt("buenasContrataciones") + " contrataciones fueron un éxito\nMalas Contrataciones: Ups... " + PlayerPrefs.GetInt("malasContrataciones") + " podrían haber sido mejores decisiones";
    }

    void calculateScoreAux(float sc) {
        if (sc >= 90) {
            letterGrade.text = "A";
            coinsWon = 30;
            message.text = messagesArray[0];
        }
        else if (sc >= 80) {
            letterGrade.text = "B";
            coinsWon = 25;
            message.text = messagesArray[0];
        }
        else if (sc >= 70) {
            letterGrade.text = "C";
            coinsWon = 20;
            message.text = messagesArray[1];
        }
        else if (sc >= 60) {
            letterGrade.text = "D";
            coinsWon = 15;
            message.text = messagesArray[1];
        }
        else if (sc >= 50) {
            letterGrade.text = "E";
            coinsWon = 10;
            message.text = messagesArray[2];
        }
        else if (sc >= 40) {
            letterGrade.text = "F";
            coinsWon = 5;
            message.text = messagesArray[2];
        }
        else {
            letterGrade.text = "G";
            coinsWon = 0;
            message.text = messagesArray[2];
        }
        coinsWonText.text = coinsWon.ToString();
        PlayerPrefs.SetInt("gameCoins",coinsWon+ PlayerPrefs.GetInt("gameCoins"));
        StartCoroutine(MandarMonedas(coinsWon));
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        calculateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    IEnumerator MandarMonedas(int nCoins){
        byte[] bodyData = new byte[0];
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateCoins/"+ nCoins + "/" + PlayerPrefs.GetInt("idUsuario");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class ExpPend_UIControl : MonoBehaviour
{
    public Text timeText;
    public Text startText;
    public float DialogueSpeed = 0.05f;
    public float DelayToWrite = 0.5f;
    int time;
    public Button startBtn;
    public Image startBoard;
    IEnumerator write_text;

    string welcomeText = "¿Tienes ojo para encontrar a la persona perfecta?\nEn este juego tendrás que revisar candidatos, investigar su información dando clic en sus datos y decidir si los contratas o no.\n¡Haz la mayor cantidad de contrataciones correctas antes de que el tiempo se acabe!";

    // Define el tiempo y las vidas
    void Start()
    {
        write_text = WriteText(welcomeText);
        StartCoroutine(write_text);
        time = ExpPend_GameControl.instance.timeToWin;
    }
    
    // Funcion para la impresion del tiempo
    public void ActiveText(){
        timeText.text = time.ToString();
    }

    // funcion para empezar a contar el tiempo
    public void StartTimer(){
        startBtn.gameObject.SetActive(false);
        startBoard.gameObject.SetActive(false);
        StopCoroutine(write_text);
        startText.text = "...";
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

    IEnumerator WriteText(string txt) {
        yield return new WaitForSeconds (DelayToWrite);
        foreach (char Character in txt.ToCharArray())
        {
            startText.text += Character;
            yield return new WaitForSeconds(DialogueSpeed);
        }
    }

    // IEnumerator GetData () {
    //     string JSONurl = "https://192.168.1.78:7128/Oxxo/GetCandidato/" + 1;
    //     UnityWebRequest web = UnityWebRequest.Get(JSONurl);
    //     web.certificateHandler = new ForceAcceptAll();
    //     yield return web.SendWebRequest ();

    //     if (web.result != UnityWebRequest.Result.Success) {
    //         UnityEngine.Debug.Log("Error API: " + web.error);
    //     }
    //     else {
    //         List<Book> bookList = new List<Book>();
    //         bookList = JsonConvert.DeserializeObject<List<Book>>
    //             (web.downloadHandler.text);
    //         LoadBookInfo(BookSelection, bookList);
    //         PlayerPrefs.SetInt("book_no", BookSelection);
    //     }
    // }

    // public void LoadBookInfo(int idBook, List<Book> bookList) {
    //     string book = bookList[idBook - 1].titulo;
    //     PlayerPrefs.SetString("book_name", book);
    //     nameText.text = book;

    //     string autor = bookList[idBook - 1].autor;
    //     PlayerPrefs.SetString("autor", autor);
    //     authorText.text = autor;
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

public class ExpPend_UIControl : MonoBehaviour
{
    // Timer
    public Text timeText;
    public Text startText;
    public float DialogueSpeed = 0.05f;
    public float DelayToWrite = 0.5f;
    int time;

    // Texto de inicio y texto de la llamada
    public Button startBtn;
    public Image startBoard;
    IEnumerator write_text;
    string welcomeText = "¿Tienes ojo para encontrar a la persona perfecta?\nEn este juego tendrás que revisar candidatos, investigar su información dando clic en sus datos y decidir si los contratas o no.\n¡Haz la mayor cantidad de contrataciones correctas antes de que el tiempo se acabe!";
    string callText;

    // Candidato
    private Candidate currCandidate;

    // Texto documentos grandes
    public Text ineText;
    public Text cvText;
    public Text cvNombre;
    public Text cvCarrera;
    public Text cvEducacion;

    public Text certNombre;
    public Text certCarrera;

    

    // Define el tiempo y las vidas
    void Start()
    {
        PlayerPrefs.SetInt("candId", 1);
        StartCoroutine(GetCandidateInfo(1));
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
        startText.text = "";
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

    public void PhoneCall() {
        StopPhoneCall();
        write_text = WriteText(currCandidate.llamada_recomendacion);
        StartCoroutine(write_text);
    }

    void StopPhoneCall() {
        StopCoroutine(write_text);
        startText.text = "";
    }


    public void FunctGetCandidate() {
        int nextCand = Random.Range(1,9);
        while(nextCand == PlayerPrefs.GetInt("candId")) {
            nextCand = Random.Range(1,9);
        }

        // int cont = PlayerPrefs.GetInt("candId");
        // if (cont == 9) cont = 1;
        // else cont++;
        PlayerPrefs.SetInt("candId", nextCand);
        StartCoroutine(GetCandidateInfo(nextCand));
    }

    IEnumerator GetCandidateInfo (int id) {
        string JSONurl = "https://10.22.210.190:7128/Oxxo/GetCandidato/" + id;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest ();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            Candidate tempCand = new Candidate();
            tempCand = JsonConvert.DeserializeObject<Candidate>(web.downloadHandler.text);
            LoadCandidateInfo(tempCand);
        }
    }

    public void LoadCandidateInfo(Candidate cand1) {
        currCandidate = new Candidate(cand1.id_candidato, cand1.nombre, 
        cand1.domicilio, cand1.fecha_nacimiento, cand1.nombre_uni, 
        cand1.carrera, cand1.llamada_recomendacion, cand1.trabajos, 
        cand1.contratar);

        string fechaStr = cand1.fecha_nacimiento.ToString("dd/MM/yyyy");

        ineText.text = cand1.nombre + "\n\n" + cand1.domicilio + "\n\n" + fechaStr;

        certNombre.text = cvNombre.text = cand1.nombre;
        cvCarrera.text = cand1.carrera;
        cvText.text = "Experiencia Laboral\n\n" + cand1.trabajos;
        cvEducacion.text = "Escuela: " + cand1.nombre_uni + " / " + cand1.carrera;

        certCarrera.text = "Por la exitosa finalización de " + cand1.carrera;

        PlayerPrefs.SetInt("contratar", currCandidate.contratar ? 1 : 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

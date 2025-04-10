using UnityEngine;
using UnityEngine.UI;

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

    // IEnumerator GetCandidateInfo (int id) {
    //     string JSONurl = "https://10.22.215.115:7128/Oxxo/GetCandidato/" + id;
    //     UnityWebRequest web = UnityWebRequest.Get(JSONurl);
    //     web.certificateHandler = new ForceAcceptAll();
    //     yield return web.SendWebRequest ();

    //     if (web.result != UnityWebRequest.Result.Success) {
    //         UnityEngine.Debug.Log("Error API: " + web.error);
    //     }
    //     else {
    //         Candidate tempCand = new Candidate();
    //         tempCand = JsonConvert.DeserializeObject<Candidate>(web.downloadHandler.text);
    //         LoadCandidateInfo(tempCand);
    //     }
    // }

    // public void LoadCandidateInfo(Candidate cand1) {
    //     currCandidate = new Candidate(cand1.id_candidato, cand1.nombre, 
    //     cand1.domicilio, cand1.fecha_nacimiento, cand1.nombre_uni, 
    //     cand1.carrera, cand1.llamada_recomendacion, cand1.trabajos, 
    //     cand1.contratar);

    //     string fechaStr = cand1.fecha_nacimiento.ToString("dd/MM/yyyy");

    //     ineText.text = cand1.nombre + "\n\n" + cand1.domicilio + "\n\n" + fechaStr;

    //     certNombre.text = cvNombre.text = cand1.nombre;
    //     cvCarrera.text = cand1.carrera;
    //     cvText.text = "Experiencia Laboral\n\n" + cand1.trabajos;
    //     cvEducacion.text = "Escuela: " + cand1.nombre_uni + " / " + cand1.carrera;

    //     certCarrera.text = "Por la exitosa finalización de " + cand1.carrera;

    //     PlayerPrefs.SetInt("contratar", currCandidate.contratar ? 1 : 0);
    // }
}

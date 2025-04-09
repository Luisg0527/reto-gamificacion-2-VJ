using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using System.Diagnostics;
using Newtonsoft.Json;

public class MariposaGameControl : MonoBehaviour
{


    public int ansTime = 25;
    static public MariposaGameControl Instance;
    public UIController UIController;
    public SFXManager SFXManager;

    preguntaMariposa pregunta = new preguntaMariposa{idPregunta = 1, pregunta = "Default", respuesta1 = "Default ", respuesta2 = "Default ", respuesta3 = "Default ", correcta = 1, indicadorSubir = 1, indicadorBajar = 1};




    public GameObject icono1;
    public GameObject icono2;
    public GameObject icono3;
    public GameObject icono4;
    public GameObject icono5;
    public GameObject icono6;
    List<GameObject> listIconos = new List<GameObject>();




    Vector3 leastTop = new Vector3(0, 1.675926f, 0);
    Vector3 leastBottom = new Vector3(0,-1.851852f,0);
    Vector3 maxTop = new Vector3(0, 2.675926f, 0);
    Vector3 maxBottom = new Vector3(0,0.1481481f,0);

    
    int scoreKeeper = 0;
    int roundTracker = 0;
    int streakTracker =0;



    public float incrementaAltura = 0.5f;
    public float decrementaAltura = 0.5f;




    public Text preguntaText;
    public Text respuesta1Text;
    public Text respuesta2Text;
    public Text respuesta3Text;
    public int preguntaSelection;


    [SerializeField] GameObject correctIcon;
    [SerializeField] GameObject incorrectIcon;






    public void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("ansTime",ansTime);

        
        StartCoroutine(GetPreguntaMariposa());
        fillListaIconos();

        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);



    }

    void SetReferences()
    {

        if (UIController == null)
        {
            UIController = FindFirstObjectByType<UIController>();
        }
        if (SFXManager == null)
        {
            SFXManager = FindFirstObjectByType<SFXManager>();
        }
        PlayerPrefs.SetInt("ansTime", ansTime);
        init();
    }

    void init()
    {
        if (UIController != null)
        {
            UIController.StartTimer();
        }
    }

    public void RestartTimer(){
        UIController.StopTimer();
        PlayerPrefs.SetInt("SavedTime", ansTime); 
        PlayerPrefs.Save(); 
        UIController.StartTimer(); 
    }

    public void fillListaIconos(){
        listIconos.Add(icono1);
        listIconos.Add(icono2);
        listIconos.Add(icono3);
        listIconos.Add(icono4);
        listIconos.Add(icono5);
        listIconos.Add(icono6);
    }












    public void selectPregunta(int _selection){
        preguntaSelection = _selection;
    }

    public void confirmRespuesta(){
        if(pregunta.correcta == preguntaSelection){
            iconoResultado(correctIcon);
            int subir = pregunta.indicadorSubir -1;
            scoreKeeper = scoreKeeper + 1;
            streakTracker = streakTracker +1;
            subirIcon(subir);
            RestartTimer();
        }
        else{
            iconoResultado(incorrectIcon);
            int bajar = pregunta.indicadorBajar -1;
            streakTracker = 0;
            bajarIcon(bajar);
            RestartTimer();
        }

        roundTracker = roundTracker + 1;
        StartCoroutine(GetPreguntaMariposa());
        if(roundTracker == 10){
            ActiveEndScene();
        }
        
    }












    IEnumerator GetPreguntaMariposa(){

        int Randomint = UnityEngine.Random.Range(1,26);  

        string JSONurl = "https://10.22.215.115:7128/Oxxo/GetPreguntaConId/" + Randomint;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success){
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else{
            pregunta = JsonConvert.DeserializeObject<preguntaMariposa>(web.downloadHandler.text);
              UpdatePregunta();
        }
    }





    public void UpdatePregunta(){

        if (preguntaText != null)
            preguntaText.text = pregunta.pregunta;

        if (respuesta1Text != null)
            respuesta1Text.text = pregunta.respuesta1;

        if (respuesta2Text != null)
            respuesta2Text.text = pregunta.respuesta2;

        if (respuesta3Text != null)
            respuesta3Text.text = pregunta.respuesta3;


    }













    public void subirIcon(int valor){
        
        Vector3 posOG =listIconos[valor].transform.position;

        UnityEngine.Debug.Log(posOG.y);

        if(valor == 0| valor ==1| valor==2){
            if(posOG.y < maxTop.y){
                listIconos[valor].transform.position += new Vector3(0,incrementaAltura,0);
            }
        }  
        else{
            if(posOG.y < maxBottom.y){
                listIconos[valor].transform.position += new Vector3(0,incrementaAltura,0);
            }
        }

    }


    public void bajarIcon(int valor){
        Vector3 posOG =listIconos[valor].transform.position;
        UnityEngine.Debug.Log(posOG.y);
        
        if(valor == 0| valor ==1| valor==2){
            if(posOG.y > leastTop.y){
                listIconos[valor].transform.position -= new Vector3(0,decrementaAltura,0);
            }
        }  
        else{
            if(posOG.y > leastBottom.y){
                listIconos[valor].transform.position -= new Vector3(0,decrementaAltura,0);
            }
        }


    }


    public void iconoResultado(GameObject iconRes){

    Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));

    GameObject popUpCorrect = Instantiate(iconRes, centerScreen, Quaternion.identity);

    popUpCorrect.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f); 

    Destroy(popUpCorrect, 0.5f);
}







    public void ActiveEndScene()
    {
        PlayerPrefs.SetInt("scoreKeeper",scoreKeeper);
        PlayerPrefs.SetInt("streak",streakTracker);
        SceneManager.LoadScene("EndScene1");

    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MenuScene1");
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }


}

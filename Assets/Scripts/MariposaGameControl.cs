using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MariposaGameControl : MonoBehaviour
{


    public int ansTime = 15;
    static public MariposaGameControl Instance;
    public UIController UIController;
    public SFXManager SFXManager;

    preguntaMariposa preguntaPrueba = new preguntaMariposa{idPregunta = 1, pregunta = "Si Aa balalalsaldsaldsa", respuesta1 = "5 ", respuesta2 = "1 ", respuesta3 = " 6 ", correcta = 3, indicadorSubir = 1, indicadorBajar = 1};




    public GameObject icono1;
    public GameObject icono2;
    public GameObject icono3;
    public GameObject icono4;
    public GameObject icono5;
    public GameObject icono6;
    List<GameObject> listIconos = new List<GameObject>();




    Vector3 leastTop = new Vector3(0, 1.175962f, 0);
    Vector3 leastBottom = new Vector3(0,-1.851852f,0);
    Vector3 maxTop = new Vector3(0, 2.7f, 0);
    Vector3 maxBottom = new Vector3(0,-0.3518519f,0);

    
    int scoreKeeper = 0;
    int roundTracker = 0;



    public float incrementaAltura = 0.5f;
    public float decrementaAltura = 0.5f;




    public Text preguntaText;
    public Text respuesta1Text;
    public Text respuesta2Text;
    public Text respuesta3Text;
    public int preguntaSelection;


    [SerializeField] GameObject correctIcon;
    [SerializeField] GameObject incorrectIcon;

    [SerializeField] GameObject info;
    GameObject popUp;





    public void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("ansTime", PlayerPrefs.GetInt("ansTime", ansTime));

        
        LoadinfoScreen();
        loadPreguntaMariposa();
        fillListaIconos();
        UpdatePregunta();


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
        ansTime = PlayerPrefs.GetInt("ansTime", 15);
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
        if(preguntaPrueba.correcta == preguntaSelection){
            iconoResultado(correctIcon);
            int subir = preguntaPrueba.indicadorSubir -1;
            scoreKeeper = scoreKeeper + 1;
            Debug.Log(scoreKeeper);
            subirIcon(subir);
            RestartTimer();
        }
        else{
            iconoResultado(incorrectIcon);
            int bajar = preguntaPrueba.indicadorBajar -1;
            bajarIcon(bajar);
            RestartTimer();
        }

        roundTracker = roundTracker + 1;

        if(roundTracker == 10){
            ActiveEndScene();
        }
        
    }












    public void loadPreguntaMariposa(){
        // if{

        // }
        // else{
            // List<preguntaMariposa> listPreguntas = new List<preguntaMariposa>();

            // bookList = JsonConvert.DeserializeObject<List<libro>>(web.downloadHandler.text);

        // }
    }




    public void UpdatePregunta(){

        preguntaText.text = preguntaPrueba.pregunta;

        respuesta1Text.text = preguntaPrueba.respuesta1;

        respuesta2Text.text = preguntaPrueba.respuesta2;

        respuesta3Text.text = preguntaPrueba.respuesta3;
    }













    public void subirIcon(int valor){
        
        Vector3 posOG =listIconos[valor].transform.position;

        if(valor == 0| valor ==1| valor==2){
            if(posOG.y < maxTop.y){
                listIconos[valor].transform.position += new Vector3(0,incrementaAltura,0);
            }
        }  
        else{
            if(posOG.y < leastBottom.y){
                listIconos[valor].transform.position -= new Vector3(0,decrementaAltura,0);
            }
        }

    }


    public void bajarIcon(int valor){
        Vector3 posOG =listIconos[valor].transform.position;
        
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

    Destroy(popUpCorrect, 1f);
}

    public void LoadinfoScreen(){

        Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));
        popUp = Instantiate(info, centerScreen, Quaternion.identity);
        popUp.transform.localScale = new Vector3(1f, 1f, 1f); 
        popUp.gameObject.SetActive(false);

    }


    public void infoScreenOpen(){
        popUp.gameObject.SetActive(true);
    }

    public void infoScreenClose(GameObject popUp){
        popUp.gameObject.SetActive(false);
    }







    public void ActiveEndScene()
    {
        PlayerPrefs.SetInt("scoreKeeper",scoreKeeper);
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

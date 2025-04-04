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

    preguntaMariposa preguntaPrueba = new preguntaMariposa{idPregunta = 1, pregunta = "Esta es la pregunta que estoy usando para probar el pedo jajajajaja", respuesta1 = "Respuesta numero 1", respuesta2 = "dfdsaf", respuesta3 = "asfddsafdsa", correcta = 2, indicadorSubir = 1, indicadorBajar = 1};


    public GameObject icono1;
    public GameObject icono2;
    public GameObject icono3;
    public GameObject icono4;
    public GameObject icono5;
    public GameObject icono6;

    List<GameObject> listIconos = new List<GameObject>();




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
        PlayerPrefs.SetInt("ansTime", PlayerPrefs.GetInt("ansTime", ansTime));

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
            subirIcon(subir);
            RestartTimer();

        }
        else{
            iconoResultado(incorrectIcon);
            int bajar = preguntaPrueba.indicadorBajar -1;
            bajarIcon(bajar);
            RestartTimer();
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
        listIconos[valor].transform.position += new Vector3(0,incrementaAltura,0);
    }
    public void bajarIcon(int valor){
        listIconos[valor].transform.position -= new Vector3(0,decrementaAltura,0);
    }


    public void iconoResultado(GameObject iconRes){

    Vector3 centerScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));

    GameObject popUpCorrect = Instantiate(iconRes, centerScreen, Quaternion.identity);

    popUpCorrect.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f); 

    Destroy(popUpCorrect, 1f);
}








    public void ActiveEndScene()
    {
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

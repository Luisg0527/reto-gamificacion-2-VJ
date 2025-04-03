using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MariposaGameControl : MonoBehaviour
{
   public int ansTime = 15;
    static public MariposaGameControl Instance;
    public UIController UIController;
    public SFXManager SFXManager;

    preguntaMariposa preguntaPrueba = new preguntaMariposa{idPregunta = 1, pregunta = "Esta es la pregunta que estoy usando para probar el pedo jajajajaja", respuesta1 = "Respuesta numero 1", respuesta2 = "dfdsaf", respuesta3 = "asfddsafdsa", correcta = 2, indicadorSubir = 0, indicadorBajar = 0};


    public GameObject iconos;



    public float incrementaAltura = 0.5f;
    public float decrementaAltura = 0.5f;

    public Text preguntaText;
    public Text respuesta1Text;
    public Text respuesta2Text;
    public Text respuesta3Text;

    public int preguntaSelection;

    public void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("ansTime", PlayerPrefs.GetInt("ansTime", ansTime));

        loadPreguntaMariposa();
        UpdatePregunta();

        Instance = this;
        Instance.SetReferences();
        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

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



    public void selectPregunta(int _selection){
        preguntaSelection = _selection;
        Debug.Log("Escogido");
    }

    public void confirmRespuesta(){
        if(preguntaPrueba.correcta == preguntaSelection){
            subirIcon();
        }
        else{
            bajarIcon();
        }
        
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

    public void loadPreguntaMariposa(){
        
    }

    public void UpdatePregunta(){
        preguntaText.text = preguntaPrueba.pregunta;

        respuesta1Text.text = preguntaPrueba.respuesta1;

        respuesta2Text.text = preguntaPrueba.respuesta2;

        respuesta3Text.text = preguntaPrueba.respuesta3;
    }



    public void subirIcon(){
        iconos.transform.position += new Vector3(0,incrementaAltura,0);
    }
    public void bajarIcon(){
        iconos.transform.position += new Vector3(0,decrementaAltura,0);
    }



}

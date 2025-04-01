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

    public Transform icono1;
    public Transform icono2;
    public Transform icono3;
    public Transform icono4;
    public Transform icono5;
    public Transform icono6;


    public float incrementaAltura = 1f;

    public Text preguntaText;
    public Text respuesta1Text;
    public Text respuesta2Text;
    public Text respuesta3Text;



    public int preguntaSelection;

    public void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("ansTime", PlayerPrefs.GetInt("ansTime", ansTime));

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

    public void Select(int _selection){
        preguntaSelection = _selection;
        Debug.Log("Escogido");
        LoadPregunta();
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

    public void LoadPregunta(){
        preguntaText.text = "Nueva Pregunta";

        respuesta1Text.text = "Nueva Respuesta";

        respuesta2Text.text = "Nueva Respuesta";

        respuesta3Text.text = "Nueva Respuesta";
    }

    public void moverIcon(int i){


    }



}

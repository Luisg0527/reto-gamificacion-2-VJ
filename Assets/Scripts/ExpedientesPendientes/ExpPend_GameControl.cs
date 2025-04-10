using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExpPend_GameControl : MonoBehaviour
{
    public int timeToWin = 60;
    static public ExpPend_GameControl instance;
    public ExpPend_UIControl uiController;

    public ExpPend_SFXManager SFXManager;

    private int totalContrataciones;

    public void Awake()
    {
        StopAllCoroutines();
        PlayerPrefs.SetInt("timeToWin", timeToWin);
        instance = this;
        instance.SetReference();
        DontDestroyOnLoad(this.gameObject);
    }

    void SetReference()
    {
        if (uiController == null){
            uiController = FindFirstObjectByType<ExpPend_UIControl>();
        }
        if (SFXManager == null) {
            SFXManager = FindFirstObjectByType<ExpPend_SFXManager>();
        }
        timeToWin = PlayerPrefs.GetInt("timeToWin", 60);
    }

    public void BeginPlaying() {
        uiController.StartTimer();
    }

    public void ActiveEndScene() {
        SceneManager.LoadScene("EndScene3");
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

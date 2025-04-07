using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int ansTime = 15;
    static public GameControl Instance;
    public UIController UIController;
    public SFXManager SFXManager;

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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public TMP_Text winLoseText;
    public SFXManager sound;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Poner cantidad de respuestas correctas
        sound.finalSound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //Application.Quit();
    }

}

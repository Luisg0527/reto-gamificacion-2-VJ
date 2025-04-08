using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMariposa : MonoBehaviour
{
    public TMP_Text pointsTotal;
    public SFXManager sound;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointsTotal.text = PlayerPrefs.GetInt("scoreKeeper")+"/10";
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
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit();
    }

}

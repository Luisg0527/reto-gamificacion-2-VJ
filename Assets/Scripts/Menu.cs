using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{



    // Método que se llama para iniciar el juego y cargar la escena "SampleScene"
    public void StartToplay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void PlayMiniGame1()
    {
        SceneManager.LoadScene("MiniGame1");
    }

    public void PlayMiniGame2()
    {
        SceneManager.LoadScene("MiniGame2");
    }

    public void PlayMiniGame3()
    {
        SceneManager.LoadScene("MiniGame3");
    }

    public void PlayEndScene3()
    {
        SceneManager.LoadScene("EndScene3");
    }

    public void GotoMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Método que se llama para salir del juego
    public void ExitGame(){
        // En el editor de Unity, esto detendría el modo de juego
        UnityEditor.EditorApplication.isPlaying = false;
        
        // En una compilación, esto cerrará la aplicación
        //Application.Quit();
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;


public class GameOverMariposa : MonoBehaviour
{
    public TMP_Text pointsTotal;
    public Text coinsText;
    public SFXManager sound;
    int coins;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("idUsuario",1);
        coins = (PlayerPrefs.GetInt("scoreKeeper") * (PlayerPrefs.GetInt("streak")/10 +1 ))*2;
        PlayerPrefs.SetInt("gameCoins",coins+ PlayerPrefs.GetInt("gameCoins"));
        coinsText.text = "+"+coins.ToString();
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


    IEnumerator MandarMonedas(int nCoins){
        byte[] bodyData = new byte[0];
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateCoins/"+ nCoins + "/" + PlayerPrefs.GetInt("idUsuario");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();
    }

}

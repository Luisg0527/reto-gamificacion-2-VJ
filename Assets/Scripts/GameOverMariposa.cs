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
        StartCoroutine(MandarMonedas(PlayerPrefs.GetInt("gameCoins")));
        PlayerPrefs.SetFloat("nivel", 1 + PlayerPrefs.GetFloat("nivel"));
            StartCoroutine(ActualizarNivel(PlayerPrefs.GetFloat("nivel")));
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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        //Application.Quit();
    }


    IEnumerator MandarMonedas(int nCoins){
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes("{}");
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateCoins/"+ nCoins + "/" + PlayerPrefs.GetInt("usuario_id");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            //Debug.Log(nCoins);
            //Debug.Log("Monedas actualizadas");
        }
    }

    IEnumerator ActualizarNivel(float nivel){
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes("{}");
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateLevel/"+ nivel + "/" + PlayerPrefs.GetInt("usuario_id");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            //Debug.Log(nCoins);
            //Debug.Log("Monedas actualizadas");
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCoins = 0;
    public GameObject quitPanel;
    public Text levelText;
    public Text coinText;

    Building building;
    void Awake()
    {
        coinText.text = PlayerPrefs.GetInt("gameCoins").ToString("N0");
        levelText.text = PlayerPrefs.GetFloat("nivel").ToString();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
        // Ocultar el panel de salida al inicio
        quitPanel.SetActive(false);
        PlayerPrefs.SetInt("quitPanel", 0);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && quitPanel.activeSelf)
        {
            quitPanel.SetActive(false);
        }
    }

    public bool HasEnoughCoins(int amount)
    {
        return playerCoins >= amount;
    }

    public void SpendCoins(int amount)
    {
        Debug.Log(amount);
        int coinsAux = PlayerPrefs.GetInt("gameCoins") - amount;
        if (HasEnoughCoins(amount))
        {
            PlayerPrefs.SetInt("gameCoins", coinsAux);
            StartCoroutine(MandarMonedas(coinsAux));

            GetCoins();
        }
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

        public void ShowQuitPanel()
    {
        if(PlayerPrefs.GetInt("quitPanel") == 0) {
            quitPanel.SetActive(true);
            PlayerPrefs.SetInt("quitPanel", 1);
        }
        else if(PlayerPrefs.GetInt("quitPanel") == 1){
            quitPanel.SetActive(false);
            PlayerPrefs.SetInt("quitPanel", 0);
        }
        
    }

    public void HideQuitPanel()
    {
        quitPanel.SetActive(false);
    }

    public void GetCoins(){
        coinText.text = PlayerPrefs.GetInt("gameCoins").ToString("N0");
    }

    public void GetNivel(){
        levelText.text = PlayerPrefs.GetFloat("nivel").ToString();
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
}

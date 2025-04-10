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
    void Awake()
    {
        coinText.text = PlayerPrefs.GetInt("gameCoins").ToString("N0");
        levelText.text = PlayerPrefs.GetInt("nivel").ToString();
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
        // Ocultar el panel de salida al inicio
        quitPanel.SetActive(false);
        
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
        if (HasEnoughCoins(amount))
        {
            playerCoins = playerCoins- amount;
            PlayerPrefs.SetInt("gameCoins",playerCoins);
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
        quitPanel.SetActive(true);
    }

    public void HideQuitPanel()
    {
        quitPanel.SetActive(false);
    }

    public void GetCoins(){
        coinText.text = PlayerPrefs.GetInt("gameCoins").ToString("N0");
    }

    public void GetNivel(){
        levelText.text = PlayerPrefs.GetInt("nivel").ToString();
    }
}

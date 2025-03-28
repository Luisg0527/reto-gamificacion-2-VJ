using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int playerCoins = 500;
    public GameObject quitPanel;
    void Awake()
    {
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
            playerCoins -= amount;
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
}

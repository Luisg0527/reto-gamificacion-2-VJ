using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Diagnostics;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

public class Building : MonoBehaviour
{
    public int cost = 100;
    public Animator animator;
    public GameObject interactionUI;
    public GameObject minigameIcon; // Icon that shows the chosen minigame
    public int idTienda = 0;

    private bool isBuilt = false;
    private int chosenMinigame = -1; // -1 means not assigned


    void Start()
    {
        interactionUI.SetActive(false);
        minigameIcon.SetActive(false);
    }

    void OnMouseDown()
    {
        if (!isBuilt)
        {
            interactionUI.SetActive(true);
        }
        else
        {
            LoadMinigame();
        }
    }

    public void BuyBuilding()
    {
        if (GameManager.instance.HasEnoughCoins(cost))
        {
            GameManager.instance.SpendCoins(cost);
            isBuilt = true;
            interactionUI.SetActive(false);
            animator.SetTrigger("Built");

            AssignRandomMinigame();
            StartCoroutine(ComprarTienda(PlayerPrefs.GetInt("usuario_id"), idTienda));
        }
        else
        {
            interactionUI.SetActive(false);
            Debug.Log("Not enough coins!");
        }
    }

    public void LoadBuilding() {
        isBuilt = true;
        interactionUI.SetActive(false);
        animator.SetTrigger("Built");
        AssignRandomMinigame();
    }

    private void AssignRandomMinigame()
    {
        chosenMinigame = Random.Range(0, 3); // Randomly pick between 0, 1, or 2
        minigameIcon.SetActive(true);
        minigameIcon.GetComponent<Animator>().SetInteger("MinigameType", chosenMinigame);
    }

    private void LoadMinigame()
    {
        string[] minigameScenes = { "MenuScene1", "MenuScene2", "MenuScene3" };
        if (chosenMinigame >= 0 && chosenMinigame < minigameScenes.Length)
        {
            SceneManager.LoadScene(minigameScenes[chosenMinigame]);
        }
    }

    [System.Obsolete]
    IEnumerator ComprarTienda (int idUsr, int idTiend) {
        string JSONurl = "https://192.168.1.78:7128/Oxxo/ComprarTienda/" + idUsr + "/" + idTiend;
        UnityWebRequest web = UnityWebRequest.Post(JSONurl," ");
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest ();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Building : MonoBehaviour
{
    public int cost = 100;
    public Animator animator;
    public GameObject interactionUI;
    public GameObject minigameIcon; // Icon that shows the chosen minigame

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
        }
        else
        {
            interactionUI.SetActive(false);
            Debug.Log("Not enough coins!");
        }
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
}

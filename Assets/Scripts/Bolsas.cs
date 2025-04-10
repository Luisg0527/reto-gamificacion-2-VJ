using UnityEngine;

public class Bolsa : MonoBehaviour
{
    private bool atrapada = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!atrapada && other.CompareTag("Player"))
        {
            atrapada = true;
            FindFirstObjectByType<BolsasController>()?.AgarrarBolsa(gameObject);
        }
    }
}

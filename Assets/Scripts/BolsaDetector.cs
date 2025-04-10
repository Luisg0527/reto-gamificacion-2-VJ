using UnityEngine;

public class BolsaDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bolsa"))
        {
            FindFirstObjectByType<BolsasController>().BolsaPerdida();
            Destroy(other.gameObject);
        }
    }
}

using UnityEngine;

public class MovimientoParabolico : MonoBehaviour
{
    public Vector3 destino;
    public float velocidad = 0.35f;
    private float alturaMaxima = 3f;
    private Vector3 inicio;
    private float progreso = 0f;

    void Start()
    {
        inicio = transform.position;
    }

    void Update()
    {
        progreso += Time.deltaTime * velocidad;

        Vector3 posicionBase = Vector3.Lerp(inicio, destino, progreso);
        float arco = 4 * alturaMaxima * (progreso - progreso * progreso);
        transform.position = new Vector3(posicionBase.x, posicionBase.y + arco, posicionBase.z);

        if (progreso >= 1f)
        {
            FindFirstObjectByType<BolsasController>()?.BolsaPerdida();
            Destroy(gameObject);
        }
    }
}

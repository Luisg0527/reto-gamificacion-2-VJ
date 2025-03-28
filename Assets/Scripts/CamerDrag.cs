using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    public Camera cam;
    public Transform background; // Assign the background sprite's Transform
    public float minZoom = 3f;
    public float maxZoom = 10f;
    public float zoomSpeed = 1f;

    private Vector3 dragOrigin;
    private float minX, maxX, minY, maxY;

    void Start()
    {
        CalculateBounds();
    }

    void Update()
    {
        HandleDragging();
        HandleZooming();
    }

    private void HandleDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    private void HandleZooming()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            cam.orthographicSize -= scroll * zoomSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
            CalculateBounds();
        }
    }

    private void CalculateBounds()
    {
        SpriteRenderer bgRenderer = background.GetComponent<SpriteRenderer>();
        if (bgRenderer != null)
        {
            float bgWidth = bgRenderer.bounds.size.x;
            float bgHeight = bgRenderer.bounds.size.y;

            float camHeight = cam.orthographicSize * 2;
            float camWidth = camHeight * cam.aspect;

            minX = background.position.x - (bgWidth / 2) + (camWidth / 2);
            maxX = background.position.x + (bgWidth / 2) - (camWidth / 2);
            minY = background.position.y - (bgHeight / 2) + (camHeight / 2);
            maxY = background.position.y + (bgHeight / 2) - (camHeight / 2);
        }
    }

    private Vector3 ClampCamera(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);
        return targetPos;
    }
}

using UnityEngine;

public class ExpPend_PhoneController : MonoBehaviour
{
    public Sprite normalSprite;
    public Sprite pickedUpSprite;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;

public class HoverSpriteChange : MonoBehaviour
{
    public Sprite normalSprite;    // The default sprite
    public Sprite hoverSprite;     // The sprite to show when hovered

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite to the normal sprite
        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    void OnMouseEnter()
    {
        // Change the sprite to the hover sprite when the mouse enters the GameObject
        if (spriteRenderer != null && hoverSprite != null)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }

    void OnMouseExit()
    {
        // Revert back to the normal sprite when the mouse exits the GameObject
        if (spriteRenderer != null && normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    void OnMouseDown()
    {
       
    }
}
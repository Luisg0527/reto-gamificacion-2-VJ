using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HoverSpriteChange : MonoBehaviour
{
    public Sprite normalSprite;    // The default sprite
    public Sprite hoverSprite;     // The sprite to show when hovered
    public Sprite pickedUpSprite;

    private SpriteRenderer spriteRenderer;

    public bool isPhoneActive = false;
    public Image startBoard;

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
        if (spriteRenderer != null && hoverSprite != null && !isPhoneActive)
        {
            spriteRenderer.sprite = hoverSprite;
        }
    }

    void OnMouseExit()
    {
        // Revert back to the normal sprite when the mouse exits the GameObject
        if (spriteRenderer != null && normalSprite != null&& !isPhoneActive)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    public void PickedUpPhone() {
        if (spriteRenderer != null && pickedUpSprite != null && !isPhoneActive)
        {
            spriteRenderer.sprite = pickedUpSprite;
            isPhoneActive = true;
            //uiController.PhoneUp();
            startBoard.gameObject.SetActive(true);
        }
        else if(spriteRenderer != null && pickedUpSprite != null && isPhoneActive) {
            spriteRenderer.sprite = normalSprite;
            isPhoneActive = false;
            //uiController.PhoneDown();
            startBoard.gameObject.SetActive(false);
        }
    }

    void OnMouseDown()
    {
       
    }
}
using UnityEngine;

public class ExpPend_DegController : MonoBehaviour
{
    Animator animatorController;
    public GameObject Deg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DegButton()
    {
        float docPosition = Deg.transform.position.y;
        Debug.Log(docPosition);
        if(docPosition < -5) {
            UpdateAnimation(degAnimations._degIn);
        }
        else {
            UpdateAnimation(degAnimations._degOut);
        }
    }

    public enum degAnimations
    {
        _degIn, _degOut, initial
    }
    void UpdateAnimation(degAnimations nameAnimation)
    {
        switch (nameAnimation)
        {
            case degAnimations.initial:
                animatorController.SetBool("degGoingIn", false);
                animatorController.SetBool("degGoingOut", false);
                break;
            case degAnimations._degIn:
                animatorController.SetBool("degGoingIn", true);
                animatorController.SetBool("degGoingOut", false);
                break;
            case degAnimations._degOut:
                animatorController.SetBool("degGoingOut", true);
                animatorController.SetBool("degGoingIn", false);
                break;
        }
    }
}

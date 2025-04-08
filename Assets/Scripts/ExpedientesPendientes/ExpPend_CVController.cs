using UnityEngine;

public class ExpPend_CVController : MonoBehaviour
{
    Animator animatorController;
    public GameObject CV;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CVButton()
    {
        float docPosition = CV.transform.position.y;
        Debug.Log(docPosition);
        if(docPosition < -5) {
            UpdateAnimation(cvAnimations._cvIn);
            Debug.Log("In");
        }
        else {
            Debug.Log("Out");
            UpdateAnimation(cvAnimations._cvOut);
        }
        
    }

    public enum cvAnimations
    {
        _cvIn, _cvOut, initial
    }
    void UpdateAnimation(cvAnimations nameAnimation)
    {
        switch (nameAnimation)
        {
            case cvAnimations.initial:
                animatorController.SetBool("cvGoingIn", false);
                animatorController.SetBool("cvGoingOut", false);
                break;
            case cvAnimations._cvIn:
                animatorController.SetBool("cvGoingIn", true);
                animatorController.SetBool("cvGoingOut", false);
                break;
            case cvAnimations._cvOut:
                animatorController.SetBool("cvGoingOut", true);
                animatorController.SetBool("cvGoingIn", false);
                break;
        }
    }
}

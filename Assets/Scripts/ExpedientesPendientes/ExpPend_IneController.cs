using UnityEngine;

public class ExpPend_IneController : MonoBehaviour
{
    Animator animatorController;
    public GameObject Doc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DocButton()
    {
        float docPosition = Doc.transform.position.y;
        Debug.Log(docPosition);
        if(docPosition < 0) {
            UpdateAnimation(docsAminations._in);
            Debug.Log("In");
        }
        else {
            Debug.Log("Out");
            UpdateAnimation(docsAminations._out);
        }
        
    }

    public enum docsAminations
    {
        _in, _out, initial
    }
    void UpdateAnimation(docsAminations nameAnimation)
    {
        switch (nameAnimation)
        {
            case docsAminations.initial:
                animatorController.SetBool("GoingIn", false);
                animatorController.SetBool("GoingOut", false);
                break;
            case docsAminations._in:
                animatorController.SetBool("GoingIn", true);
                animatorController.SetBool("GoingOut", false);
                break;
            case docsAminations._out:
                animatorController.SetBool("GoingOut", true);
                animatorController.SetBool("GoingIn", false);
                break;
        }
    }
}

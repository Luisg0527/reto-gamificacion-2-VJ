using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExpPend_CandidateController : MonoBehaviour
{
    Animator animatorController;
    public GameObject Candidate;
    public Sprite sprt1;
    public Sprite sprt2;
    public Sprite sprt3;
    public Sprite sprt4;
    public Sprite sprt5;
    public Sprite sprt6;
    public Sprite sprt8;
    public Sprite sprt9;
    private SpriteRenderer spriteRenderer;

    // Auxiliares
    List<Sprite> sprtList = new List<Sprite>();
    int lastSprite;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprtList.AddRange(new Sprite[] { sprt1, sprt2, sprt3, sprt4, sprt5, sprt6, sprt8, sprt9 });
        PlayerPrefs.SetInt("lastSprite", 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animatorController = GetComponent<Animator>();
        UpdateAnimation(candAnimations._candIn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RechazarCandidato() {
        int currSprite = Random.Range(1,9);
        while(currSprite == PlayerPrefs.GetInt("lastSprite", lastSprite)) {
            currSprite = Random.Range(1,9);
        }
        PlayerPrefs.SetInt("lastSprite", currSprite);
    }

    // public void ContratarCandidato() {
        
    // }

    public enum candAnimations
    {
        _candIn, _candOut, _start
    }
    void UpdateAnimation(candAnimations nameAnimation)
    {
        switch (nameAnimation)
        {
            case candAnimations._start:
                animatorController.SetBool("SpriteIn", false);
                animatorController.SetBool("SpriteOut", false);
                break;
            case candAnimations._candIn:
                animatorController.SetBool("SpriteIn", true);
                animatorController.SetBool("SpriteOut", false);
                break;
            case candAnimations._candOut:
                animatorController.SetBool("degGoingOut", true);
                animatorController.SetBool("SpriteOut", false);
                break;
        }
    }
}

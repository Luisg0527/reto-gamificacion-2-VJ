using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ExpPend_CandidateController : MonoBehaviour
{
    Animator animatorController;
    public GameObject Candidate;

    // Sprites para cambiar
    public GameObject angryBubble;
    public GameObject happyBubble;
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

    HoverSpriteChange hangUpAuxiliar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angryBubble.gameObject.SetActive(false);
        happyBubble.gameObject.SetActive(false);
        PlayerPrefs.SetInt("buenasContrataciones", 0);
        PlayerPrefs.SetInt("malasContrataciones", 0);
        PlayerPrefs.SetInt("Contrataciones", 0);
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

    public void AceptarCandidato() {
        StartCoroutine(ContratarRoutine());
        int contratAux = PlayerPrefs.GetInt("Contrataciones") + 1;
        PlayerPrefs.SetInt("Contrataciones", contratAux);

        if(PlayerPrefs.GetInt("contratar") == 1) {
            int aux = PlayerPrefs.GetInt("buenasContrataciones") + 1;
            PlayerPrefs.SetInt("buenasContrataciones", aux);
        }
        else {
            int aux = PlayerPrefs.GetInt("malasContrataciones") + 1;
            PlayerPrefs.SetInt("malasContrataciones", aux);
        }
    }

    public void RechazarCandidato() {
        StartCoroutine(RechazarRoutine());
        if(PlayerPrefs.GetInt("contratar") == 0) {
            int aux = PlayerPrefs.GetInt("buenasContrataciones") + 1;
            PlayerPrefs.SetInt("buenasContrataciones", aux);
            Debug.Log("buenas: " + PlayerPrefs.GetInt("buenasContrataciones"));
        }
        else {
            int aux = PlayerPrefs.GetInt("malasContrataciones") + 1;
            PlayerPrefs.SetInt("malasContrataciones", aux);
            Debug.Log("malas: " + PlayerPrefs.GetInt("malasContrataciones"));
        }
    }

    IEnumerator ContratarRoutine() {
        happyBubble.gameObject.SetActive(true);
        int currSprite = Random.Range(1,9);
        while(currSprite == PlayerPrefs.GetInt("lastSprite", lastSprite)) {
            currSprite = Random.Range(1,9);
        }
        PlayerPrefs.SetInt("lastSprite", currSprite);
        UpdateAnimation(candAnimations._candOut);
        //hangUpAuxiliar.HangUpPhone();
        yield return new WaitForSeconds(1f);

        spriteRenderer.sprite = sprtList[currSprite - 1];
        happyBubble.gameObject.SetActive(false);
        UpdateAnimation(candAnimations._candIn);
    }

    IEnumerator RechazarRoutine() {
        angryBubble.gameObject.SetActive(true);
        int currSprite = Random.Range(1,9);
        while(currSprite == PlayerPrefs.GetInt("lastSprite", lastSprite)) {
            currSprite = Random.Range(1,9);
        }
        PlayerPrefs.SetInt("lastSprite", currSprite);
        UpdateAnimation(candAnimations._candAngryOut);
        ExpPend_GameControl.instance.SFXManager.AngryOutSound();
        yield return new WaitForSeconds(1.2f);
        spriteRenderer.sprite = sprtList[currSprite - 1];
        angryBubble.gameObject.SetActive(false);
        UpdateAnimation(candAnimations._candIn);
    }

    public enum candAnimations
    {
        _candIn, _candOut, _start, _candAngryOut
    }
    void UpdateAnimation(candAnimations nameAnimation)
    {
        switch (nameAnimation)
        {
            case candAnimations._start:
                animatorController.SetBool("SpriteIn", false);
                animatorController.SetBool("SpriteOut", false);
                animatorController.SetBool("SpriteAngryOut", false);
                break;
            case candAnimations._candIn:
                animatorController.SetBool("SpriteIn", true);
                animatorController.SetBool("SpriteOut", false);
                animatorController.SetBool("SpriteAngryOut", false);
                break;
            case candAnimations._candOut:
                animatorController.SetBool("SpriteOut", true);
                animatorController.SetBool("SpriteIn", false);
                break;
            case candAnimations._candAngryOut:
                animatorController.SetBool("SpriteAngryOut", true);
                animatorController.SetBool("SpriteOut", false);
                animatorController.SetBool("SpriteIn", false);
                break;
        }
    }
}

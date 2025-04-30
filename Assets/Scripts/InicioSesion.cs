using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;
using TMPro;

public class InicioSesion : MonoBehaviour
{
    public TMP_InputField userField;
    public TMP_InputField passwordField;
    public Text errorText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Submit() {
        string userInput = userField.text;
        string passInput = passwordField.text;

        StartCoroutine(VerificarUsuario(userInput, passInput));
        StartCoroutine(GetUserInfo(userInput));
    }


    IEnumerator VerificarUsuario (string usr, string contra) {
        string JSONurl = "https://10.22.210.190:7128/Oxxo/VerificarUsuario/" + usr + "/" + contra;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest ();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            bool sesionCorrecta = false;
            sesionCorrecta = JsonConvert.DeserializeObject<bool>(web.downloadHandler.text);
            VerificarSesion(sesionCorrecta, usr);
        }
    }

    IEnumerator GetUserInfo (string userName) {
        string JSONurl = "https://10.22.210.190:7128/Oxxo/GetUsuario/" + userName;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest ();

        if (web.result != UnityWebRequest.Result.Success) {
            errorText.text = "Error API: " + web.error;

        }
        else {
            Usuario usuario = new Usuario();
            usuario = JsonConvert.DeserializeObject<Usuario>(web.downloadHandler.text);
            definirUsuario(usuario);
        }
    }

    public void VerificarSesion(bool existe, string usr) {
        if(existe) {
            PlayerPrefs.SetString("userName", usr);
            StartCoroutine(GetUserInfo(usr));
            SceneManager.LoadScene("MenuScene");
        }
        else {
            errorText.text = "Usuario o contraseña incorrectos";
            userField.text = "";
            passwordField.text = "";
        }
    }

    public void definirUsuario(Usuario usr1) {
        PlayerPrefs.SetInt("usuario_id", usr1.id_usuario);
        PlayerPrefs.SetInt("gameCoins", usr1.monedas);
        PlayerPrefs.SetFloat("nivel", usr1.nivel);
        Debug.Log(usr1.monedas);
    }
}

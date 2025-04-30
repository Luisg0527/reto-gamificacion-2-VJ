using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections;

public class EndSceneController2 : MonoBehaviour
{
    public Text puntuacionCompletaTexto; 
    public Text monedasGanadas; 

    void Start()
    {
        SFXManager2.Instance.ReproducirAmbienteFinal();
        int puntos = PlayerPrefs.GetInt("PuntosFinales", 0);
        int monedas = puntos;
        string rango = ObtenerRango(puntos);
        monedasGanadas.text = puntos.ToString();

        string textoFinal = $"PUNTUACION = {puntos}\nRANGO = {rango.ToUpper()}\nMONEDAS = {monedas}";
        PlayerPrefs.SetInt("gameCoins",monedas+ PlayerPrefs.GetInt("gameCoins"));
        StartCoroutine(MandarMonedas(PlayerPrefs.GetInt("gameCoins")));

        if (puntuacionCompletaTexto != null)
            puntuacionCompletaTexto.text = textoFinal;

        PlayerPrefs.SetInt("Monedas", PlayerPrefs.GetInt("Monedas", 0) + monedas);
        PlayerPrefs.SetFloat("nivel", 1 + PlayerPrefs.GetFloat("nivel"));
        StartCoroutine(ActualizarNivel(PlayerPrefs.GetFloat("nivel")));
        PlayerPrefs.Save();
    }

    string ObtenerRango(int puntos)
    {
        if (puntos >= 40)
            return "Dios del OXXO";
        else if (puntos >= 30)
            return "Leyenda";
        else if (puntos >= 20)
            return "Experto";
        else if (puntos >= 10)
            return "Aprendiz";
        else
            return "Principiante";
    }

    public void StartToPlay()
    {
        SceneManager.LoadScene("GameScene");
    }

IEnumerator MandarMonedas(int nCoins){
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes("{}");
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateCoins/"+ nCoins + "/" + PlayerPrefs.GetInt("usuario_id");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            //Debug.Log(nCoins);
            //Debug.Log("Monedas actualizadas");
        }
    }

    IEnumerator ActualizarNivel(float nivel){
        byte[] bodyData = System.Text.Encoding.UTF8.GetBytes("{}");
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateLevel/"+ nivel + "/" + PlayerPrefs.GetInt("usuario_id");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            //Debug.Log(nCoins);
            //Debug.Log("Monedas actualizadas");
        }
    }
}

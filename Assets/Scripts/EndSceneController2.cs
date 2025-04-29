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

    void Start()
    {
        SFXManager2.Instance.ReproducirAmbienteFinal();
        int puntos = PlayerPrefs.GetInt("PuntosFinales", 0);
        int monedas = puntos;
        string rango = ObtenerRango(puntos);

        string textoFinal = $"PUNTUACION = {puntos}\nRANGO = {rango.ToUpper()}\nMONEDAS = {monedas}";
        MandarMonedas(monedas);
        PlayerPrefs.SetInt("gameCoins",monedas+ PlayerPrefs.GetInt("gameCoins"));

        if (puntuacionCompletaTexto != null)
            puntuacionCompletaTexto.text = textoFinal;

        PlayerPrefs.SetInt("Monedas", PlayerPrefs.GetInt("Monedas", 0) + monedas);
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

    IEnumerator MandarMonedas(int nCoins){
        byte[] bodyData = new byte[0];
        string JSONurl = "https://10.22.210.190:7128/Oxxo/UpdateCoins/"+ nCoins + "/" + PlayerPrefs.GetInt("idUsuario");
        UnityWebRequest web = UnityWebRequest.Put(JSONurl,bodyData);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest();
    }
}

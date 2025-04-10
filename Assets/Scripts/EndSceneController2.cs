using UnityEngine;
using UnityEngine.UI;

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
}

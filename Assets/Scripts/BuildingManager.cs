using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Diagnostics;
using Newtonsoft.Json;

public class BuildingManager : MonoBehaviour
{
    public Building oxxo1;
    public Building oxxo2;
    public Building oxxo3;
    public Building oxxo4;
    public Building oxxo5;
    public Building oxxo6;
    List<Building> buildingList = new List<Building>();
    private List<Tiendas> userBuildings;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buildingList.AddRange(new Building[] {oxxo1, oxxo2, oxxo3, oxxo4, oxxo5, oxxo6});
        StartCoroutine(GetTiendasUsuario(PlayerPrefs.GetInt("usuario_id")));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadUserBuildings() {
        foreach(Tiendas tienda in userBuildings) {
            buildingList[tienda.id_tienda - 1].LoadBuilding();
        }
    }

    IEnumerator GetTiendasUsuario (int id) {
        string JSONurl = "https://192.168.1.78:7128/Oxxo/GetTiendasUsuario/" + id;
        UnityWebRequest web = UnityWebRequest.Get(JSONurl);
        web.certificateHandler = new ForceAcceptAll();
        yield return web.SendWebRequest ();

        if (web.result != UnityWebRequest.Result.Success) {
            UnityEngine.Debug.Log("Error API: " + web.error);
        }
        else {
            List<Tiendas> tempStores = new List<Tiendas>();
            tempStores = JsonConvert.DeserializeObject<List<Tiendas>>(web.downloadHandler.text);
            userBuildings = new List<Tiendas>(tempStores);
            LoadUserBuildings();
            //LoadTiendas(PlayerPrefs.GetInt("usuario_id"), tempStores);
        }
    }

    // public void LoadTiendas(int idUsr, List<Tiendas> tempStores) {
    //     string book = bookList[idBook - 1].titulo;
    //     PlayerPrefs.SetString("book_name", book);
    //     nameText.text = book;

    //     string autor = bookList[idBook - 1].autor;
    //     PlayerPrefs.SetString("autor", autor);
    //     authorText.text = autor;
    // }
}

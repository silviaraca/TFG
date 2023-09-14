using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;

public class Treasure1 : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        if(PlayerPrefs.HasKey("Tumba")){
            dialogue.desactivaDialogo();
        }
    }

    void Update()
    {
        if(!PlayerPrefs.HasKey("Tumba") && dialogue.indexFin()){
            dialogue.desactivaDialogo();
            List<string> inventory = new List<string>();
            if(PlayerPrefs.HasKey("InventoryCards")){
                string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
                inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
            }
            inventory.Add("Tumba");
            string inventoryData2 = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("InventoryCards", inventoryData2);
            PlayerPrefs.Save();
            string mina = "done";
            PlayerPrefs.SetString("Tumba", mina);
            PlayerPrefs.Save();
        }
    }
}

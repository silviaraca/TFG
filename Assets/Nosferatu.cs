using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Nosferatu : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        
    }

    void Update()
    {
        if(!PlayerPrefs.HasKey("Nosferatu") && dialogue.indexFin()){
            List<string> inventory = new List<string>();
            if(PlayerPrefs.HasKey("InventoryCards")){
                string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
                inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
            }
            inventory.Add("Nosferatu");
            string inventoryData2 = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("InventoryCards", inventoryData2);
            PlayerPrefs.Save();
            string mina = "done";
            PlayerPrefs.SetString("Nosferatu", mina);
            PlayerPrefs.Save();
        }
    }
}

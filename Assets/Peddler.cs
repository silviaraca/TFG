using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Peddler : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        
    }

    void Update()
    {
        if(!PlayerPrefs.HasKey("Carreta") && dialogue.indexFin()){
            List<string> inventory = new List<string>();
            if(PlayerPrefs.HasKey("InventoryCards")){
                string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
                inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
            }
            inventory.Add("Carreta");
            string inventoryData2 = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("InventoryCards", inventoryData2);
            PlayerPrefs.Save();
            string mina = "done";
            PlayerPrefs.SetString("Carreta", mina);
            PlayerPrefs.Save();
        }
    }
}

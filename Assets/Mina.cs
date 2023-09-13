using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Mina : MonoBehaviour
{
    public Dialogue dialogue;
    void Start()
    {
        
    }

    void Update()
    {
        if(!PlayerPrefs.HasKey("Mina") && dialogue.indexFin()){
            List<string> inventory = new List<string>();
            if(PlayerPrefs.HasKey("InventoryCards")){
                string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
                inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
            }
            inventory.Add("Mina");
            inventory.Add("Cuchillo");
            string inventoryData2 = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("InventoryCards", inventoryData2);
            PlayerPrefs.Save();
            string mina = "done";
            PlayerPrefs.SetString("Mina", mina);
            PlayerPrefs.Save();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class Jabs : MonoBehaviour
{
    public static int num;
    public Dialogue dialogueScript1;
    public Dialogue dialogueScript2; 
    private bool cartaDada;
    // Start is called before the first frame update

    void Start()
    {
        cartaDada = false;
        if (PlayerPrefs.HasKey("NumJabs"))
        {
            string number = PlayerPrefs.GetString("NumJabs");
            num = int.Parse(number);      
        }
        else
        {
            num = 0;
        }

        Debug.Log(num);
    }

    // Update is called once per frame
    void Update()
    {
        if(num == 3)
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = true;
        }
        else
        {
            dialogueScript1.enabled = true;
            dialogueScript2.enabled = false;
        }
        if(dialogueScript2.indexFin() && !cartaDada){
            List<string> inventory = new List<string>();
            if(PlayerPrefs.HasKey("InventoryCards")){
                string inventoryData1 = PlayerPrefs.GetString("InventoryCards");
                inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData1);
            }
            inventory.Add("Aldeano");
            string inventoryData2 = JsonConvert.SerializeObject(inventory);
            PlayerPrefs.SetString("InventoryCards", inventoryData2);
            PlayerPrefs.Save();
            dialogueScript2.getPlayerExistente().inventory.cargaInventory();
            cartaDada = true;
        }
        string numString = num.ToString();
        PlayerPrefs.SetString("NumJabs", numString);
        PlayerPrefs.Save();
    }
}

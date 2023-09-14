using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class VanHelsing : MonoBehaviour
{
    public DialogoDecisiones dialogue1;
    public Dialogue dialogue2;
    public Dialogue dialogue3;
    private bool activeE;
    private Player player;
    private string rat;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Tutorial") && !PlayerPrefs.HasKey("Recolectadas"))
        {
            dialogue1.enabled = false;
            dialogue2.enabled = true;
            dialogue3.enabled = false;
            if(!PlayerPrefs.HasKey("Tutorial2")){
                dialogue2.activa();
                string tuto = "done";
                PlayerPrefs.SetString("Tutorial2", tuto);
                PlayerPrefs.Save();
            }
        }
        if(PlayerPrefs.HasKey("InventoryCards")){
            List<string> inventory = new List<string>();
            string inventoryData = PlayerPrefs.GetString("InventoryCards");
            inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData);
            if(inventory.Count >= 6){
                string recolect = "done";
                PlayerPrefs.SetString("Recolectadas", recolect);
                PlayerPrefs.Save();
                dialogue1.enabled = false;
                dialogue2.enabled = false;
                dialogue3.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.HasKey("Tutorial") && !PlayerPrefs.HasKey("Tutorial2") && !PlayerPrefs.HasKey("Recolectadas"))
        {
            dialogue1.enabled = true;
            dialogue2.enabled = false;
            dialogue3.enabled = false;
        }
        if(!PlayerPrefs.HasKey("Renfield") && dialogue3.indexFin()){
            string ren = "done";
            PlayerPrefs.SetString("Renfield", ren);
            PlayerPrefs.Save();
        }

    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true; 
        }                   
    }
}

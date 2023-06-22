using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public int numItem;


    private void Awake()
    {
        inventory = new Inventory(24);
        
        //Cargar inventario
        if (PlayerPrefs.HasKey("InventoryData"))
        {
            string inventoryData = PlayerPrefs.GetString("InventoryData");
            inventory = JsonUtility.FromJson<Inventory>(inventoryData);

        }
    }

    public Player()
    {
        inventory = new Inventory(24);
    }

}

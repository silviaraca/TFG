using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public List<string> deck;
    public int numItem;
    public Vector2 playerPosition;
    [SerializeField] private TextMeshProUGUI textoE;


    private void Awake()
    {
        inventory = new Inventory(22);
        textoE.gameObject.SetActive(false);
        //Cargar inventario
        if (PlayerPrefs.HasKey("InventoryData"))
        {
            string inventoryData = PlayerPrefs.GetString("InventoryData");
            inventory = JsonUtility.FromJson<Inventory>(inventoryData);

        }
    }

    public Player()
    {
        inventory = new Inventory(22);
    }

    void Update()
    {
        playerPosition = transform.position;
    }
}

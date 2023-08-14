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
    private string nombreActivo = "";


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

     private void OnTriggerEnter2D(Collider2D collision)
    {
        nombreActivo = collision.gameObject.name;  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nombreActivo = "";          
    }
    public string getActivo(){
        return nombreActivo;
    }
}

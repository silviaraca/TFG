using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

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
        textoE.gameObject.SetActive(false);
        //Cargar inventario
        /*if (PlayerPrefs.HasKey("InventoryData"))
        {
            string inventoryData = PlayerPrefs.GetString("InventoryData");
            print(inventoryData);
            inventory = JsonUtility.FromJson<Inventory>(inventoryData);

        }*/ //Esto está comentado hasta que se haga el json con el nuevo formato de inventario jiji
        if (PlayerPrefs.HasKey("PosicionPlayer"))
        {
            string posicionJSON = PlayerPrefs.GetString("PosicionPlayer");
            Vector3 posicion= JsonUtility.FromJson<Vector3>(posicionJSON);
            this.gameObject.transform.position = posicion;
        }
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

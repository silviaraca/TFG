using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject deckSelectorPanel;
    public Player player;
    public List<GameObject> slots = new List<GameObject>();
    public GameObject slotPrefab;
    public GameObject slotsPanel; 


    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if(PauseMenu.isPaused)
        {
            inventoryPanel.SetActive(false);
        }
    }

    //Abrir el inventario
    public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf && !PauseMenu.isPaused)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f; //Stop moving
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
            for(int i = 0; i < slots.Count; i++){
                Destroy(slots[i]);
            }
            slots.Clear(); 
        }
    }

    public void Setup()
    {
        List<string> listaCartas= new List<string>();
       
        //Extraer info del JSON mazo
        if (PlayerPrefs.HasKey("DeckData"))
        {
            string deckData = PlayerPrefs.GetString("DeckData");
            listaCartas = JsonConvert.DeserializeObject<List<string>>(deckData);
            player.deck = listaCartas;
        }

        slots.Add(slotPrefab);

        for(int i = 1; i < player.inventory.slots.Count; i++)
        {
            GameObject slotUI = Instantiate(slotPrefab, transform);
            slotUI.gameObject.transform.SetParent(slotsPanel.transform, false);
            slotUI.GetComponent<Slots_UI>().nombreCarta = player.inventory.slots[i].GetComponent<Slot>().nombre;
            slots.Add(slotUI);
        }
        slotPrefab.GetComponent<Slots_UI>().nombreCarta = player.inventory.slots[0].GetComponent<Slot>().nombre;
        for(int i = 0; i < player.inventory.slots.Count; i++)
        {
            for(int j = 0; j < listaCartas.Count; j++){
                if(player.inventory.slots[i].GetComponent<Slot>().nombre == listaCartas[j]){
                    player.inventory.slots[i].GetComponent<Slot>().Sumar();
                }
            }
            slots[i].GetComponent<Slots_UI>().Initialize(player.inventory.slots[i].GetComponent<Slot>());
        }
    }

    public void Open()
    {
        deckSelectorPanel.SetActive(false);
        ToggleInventory();
    }
    
    public void End()
    {
        inventoryPanel.SetActive(false);
        for(int i = 1; i < slots.Count; i++){
            Destroy(slots[i]);
        }
        slots.Clear();
        string deckData = JsonConvert.SerializeObject(player.deck);
        PlayerPrefs.SetString("DeckData", deckData);
        PlayerPrefs.Save(); 
        deckSelectorPanel.SetActive(true);
    }

}

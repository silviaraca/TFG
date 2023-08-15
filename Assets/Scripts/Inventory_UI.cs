using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public void ToggleInventory()
    {
        if(!inventoryPanel.activeSelf && !PauseMenu.isPaused)
        {
            inventoryPanel.SetActive(true);
            Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
            for(int i = 0; i < slots.Count; i++){
                Destroy(slots[i]);
            }
            slots.Clear(); 
        }
    }

    public void Setup()
    {
        for(int i = 0; i < player.inventory.slots.Count; i++)
        {
            GameObject slotUI = Instantiate(slotPrefab, transform);
            slotUI.gameObject.transform.SetParent(slotsPanel.transform, false);
            slots.Add(slotUI);
        }
        for(int i = 0; i < player.inventory.slots.Count; i++)
        {
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
        for(int i = 0; i < slots.Count; i++){
            Destroy(slots[i]);
        }
        slots.Clear(); 
        deckSelectorPanel.SetActive(true);
    }

}

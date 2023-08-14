using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<Slots_UI> slots = new List<Slots_UI>();
    public Slots_UI slotPrefab;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();

        }
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
        }
    }

    public void Setup()
    {
        if(slots.Count == player.inventory.slots.Count)
        { 
            foreach (Slot slot in player.inventory.slots)
            {
                if (slot.type != CollectableType.NONE)
                {
                    Slots_UI slotUI = Instantiate(slotPrefab, transform);
                    slotUI.Initialize(slot);
                    slots.Add(slotUI);
                }
            }

        }
    }



}

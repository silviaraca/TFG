using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject slot;
    public int count; // Items ahora mismo
    public int maxSlots;
    public int busySlots;

    // Inicializar
    public void Start()
    {
        count = 0;
        busySlots = 0;
        this.maxSlots = 22;
        for (int i = 0; i < 22; i++)
        {
            GameObject slotAux = Instantiate(slot, transform);
            slots.Add(slotAux);
        }
    }

    // Se añade un item
    public bool Add(Collectable item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            // Items ya existentes
            if (slots[i].GetComponent<Slot>().type == (CollectableType)item.type)
            {
                if (slots[i].GetComponent<Slot>().max > slots[i].GetComponent<Slot>().count)
                {
                    // Se añade el item
                    slots[i].GetComponent<Slot>().AddItem(item);
                    return true;
                }
                else return false;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            // Items nuevos
            if (slots[i].GetComponent<Slot>().type == CollectableType.NONE)
            {
                slots[i].GetComponent<Slot>().AddItem(item);
                busySlots++;
                return true;
            }
        }

        return false;
    }
}

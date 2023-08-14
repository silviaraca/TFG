using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Inventory
{
    public List<Slot> slots = new List<Slot>();
    public int count; // Items ahora mismo
    public int maxSlots;
    public int busySlots;

    // Inicializar
    public Inventory(int num)
    {
        count = 0;
        busySlots = 0;
        this.maxSlots = num;
        for (int i = 0; i < num; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    // Se añade un item
    public bool Add(Collectable item)
    {
        foreach (Slot slot in slots)
        {
            // Items ya existentes
            if (slot.type == (CollectableType)item.type)
            {
                if (slot.max > slot.count)
                {
                    // Se añade el item
                    slot.AddItem(item);
                    return true;
                }
                else return false;
            }
        }

        foreach (Slot slot in slots)
        {
            // Items nuevos
            if (slot.type == CollectableType.NONE)
            {
                slot.AddItem(item);
                busySlots++;
                return true;
            }
        }

        return false;
    }
}

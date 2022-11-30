using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    public int count; //Items ahora mismo
    public int maxSlots;
    public int busySlots;
    public List<Slot> slots = new List<Slot>();

    //Inicializar
    public Inventory(int num)
    {
        count = 0;
        busySlots = 0;
        this.maxSlots = num;
        for(int i = 0; i < num; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    //Se añade un item
    public bool Add(CollectableType itemType)
    {
        foreach(Slot slot in slots)
        {
            //Items ya existentes
            if(slot.type == itemType)
            {
                if(slot.max > slot.count)
                {
                    //Se añade el item
                    slot.type = itemType;
                    slot.count++;
                    count++;
                    return true;
                }
                else return false;
            }
            
        }

        foreach(Slot slot in slots)
        {
            //Items nuevos
            if(slot.type == CollectableType.NONE)
            {
                slot.type = itemType;
                slot.count++;
                count++;
                busySlots++;
                return true;
            }
        }

        return false;
    }
}

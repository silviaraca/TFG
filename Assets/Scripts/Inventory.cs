using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory 
{
    [System.Serializable]
    public class Slot
    {
        public CollectableType type;
        public int count; //Items ahora mismo
        public int max; //Num max de items permitidos
        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            max = 10; //EJEMPLO
        }
    }

    public List<Slot> slots = new List<Slot>();

    //Inicializar
    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(CollectableType itemType)
    {
        foreach(Slot slot in slots)
        {
            if(slot.type == itemType && max > count)
            {
                //Se añade el item
                this.type = itemType;
                count++;
                return;
            }
        }

        foreach(Slot slot in slots)
        {
             if(slot.type == CollectableType.NONE)
            {
                //Se añade el item
                this.type = itemType;
                count++;
                return;
            }
        }
    }
}

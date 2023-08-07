using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class Slot
    {
        public int countAdded; // Items añadidos
        public int countRemaining; // Items por añadir
        public int max; //Num max de items permitidos
        public int count;
        public CollectableType type;
        public Sprite icon;
        
        public Slot()
        {
            type = CollectableType.NONE;
            count = 0;
            max = 4;
            countAdded = 0;
            countRemaining = max;
        }

        public void AddItem(Collectable item)
        {
            this.type = (CollectableType)item.type;
            this.icon = item.icon;
            count++;
             
        }

        public void Sumar()
        {
            if (countRemaining > 0)
            {
                countAdded++;
                countRemaining--;
            }
        }

         public void Restar()
        {
            if (countAdded > 0)
            {
                countAdded--;
                countRemaining++;
            }
        }


    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Slot
{
    public int countAdded; // Items añadidos
    public int countRemaining; // Items por añadir
    public int max; // Num max de items permitidos
    public int count;
    public CollectableType type;
    public Sprite icon; 
    public event Action<int> OnCountAddedChanged;
    public event Action<int> OnCountRemainingChanged;

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
        countAdded++;
        countRemaining--;
    }

    public void Sumar()
    {
        if (countRemaining > 0)
        {
            countAdded++;
            countRemaining--;
              // Disparar el evento OnCountAddedChanged
            OnCountAddedChanged?.Invoke(countAdded);
            // Disparar el evento OnCountRemainingChanged
            OnCountRemainingChanged?.Invoke(countRemaining);
        }
    }

    public void Restar()
    {
        if (countAdded > 0)
        {
            countAdded--;
            countRemaining++;

            
            // Disparar el evento OnCountAddedChanged
            OnCountAddedChanged?.Invoke(countAdded);
            // Disparar el evento OnCountRemainingChanged
            OnCountRemainingChanged?.Invoke(countRemaining);
        }
    }
}

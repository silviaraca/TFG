using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot 
{
    public int count; //Items ahora mismo
    public int max; //Num max de items permitidos
    public CollectableType type;
    
    public Slot()
    {
        count = 0;
        max = 2;
        type = CollectableType.NONE;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory Inventory;

    private void Awake()
    {
        inventory = new Inventory(10);
    }
}

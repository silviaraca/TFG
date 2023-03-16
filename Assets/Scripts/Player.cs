using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventory inventory;
    public int numItem;

    private void Awake()
    {
        inventory = new Inventory(20);
    }

    public Player()
    {
        inventory = new Inventory(20);
    }

}

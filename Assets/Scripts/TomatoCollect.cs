using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType{NONE, TOMATO}

public class TomatoCollect : MonoBehaviour
{
   public CollectableType type;
   
   private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        type = CollectableType.TOMATO;

        if(player)
        {
            if(player.inventory.Add(type))
            {
                Destroy(this.gameObject);
            }
        }
    }
}

 
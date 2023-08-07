using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots_UI : MonoBehaviour
{
   public Image itemIcon;
   public TextMeshProUGUI countAddedText; // Referencia al Text debajo del +
   public TextMeshProUGUI countRemainingText; // Referencia al Text debajo del -

   public void SetItem(Slot slot)
   {
        if(slot != null)    
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1,1,1,1);
            countAddedText.text = slot.countAdded.ToString();
            countRemainingText.text = slot.countRemaining.ToString();
        }
   }

   public void SetEmpty()
   {
        itemIcon.sprite = null;
        itemIcon.color = new Color(1,1,1,0);
        countAddedText.text = "";
        countRemainingText.text = "";
        
   }
}

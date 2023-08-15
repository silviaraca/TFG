using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckSelector_UI : MonoBehaviour
{
    public GameObject deckSelectorPanel;

    void Start()
    {
        deckSelectorPanel.SetActive(true);
    }


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(!deckSelectorPanel.activeSelf && !PauseMenu.isPaused)
            {
                deckSelectorPanel.SetActive(true);

            }
            else
            {
                deckSelectorPanel.SetActive(false);
            }
            
        }

    }
}

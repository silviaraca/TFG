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
        deckSelectorPanel.SetActive(false);
    }


    public void Update()
    {
        if(PauseMenu.isPaused)
        {
            deckSelectorPanel.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Tab) && !PauseMenu.isPaused)
        {
            if(!deckSelectorPanel.activeSelf && !PauseMenu.isPaused)
            {
                deckSelectorPanel.SetActive(true);
                Time.timeScale = 0f; //Stop moving

            }
            else
            {
                deckSelectorPanel.SetActive(false);
                Time.timeScale = 1f;
            }
            
        }

    }
}

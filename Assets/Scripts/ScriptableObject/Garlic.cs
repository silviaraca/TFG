using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Garlic : MonoBehaviour
{
    public TextMeshProUGUI textoE;
    private SpriteRenderer spriteRenderer;
    private Player player;
    private bool dejable;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        textoE.gameObject.SetActive(false);
        isVisible = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player){
            textoE.gameObject.SetActive(true);
            dejable = true;
        }                  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            textoE.gameObject.SetActive(false);
            dejable = false;
        } 
    }

    void Update()
    {
        if (dejable && Input.GetKeyDown(KeyCode.E))
        {
            spriteRenderer.enabled = true;
        }
    }
}

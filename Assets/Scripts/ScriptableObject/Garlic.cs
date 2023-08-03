using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Garlic : MonoBehaviour
{
    public TextMeshProUGUI textoE;
    public BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Player player;
    private bool dejable;
    private bool done;

    void Start()
    {
        done = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        textoE.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(player && !done){
            textoE.gameObject.SetActive(true);
            dejable = true;
        }                  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player") && !done){
            textoE.gameObject.SetActive(false);
            dejable = false;
        } 
    }

    void Update()
    {
        if (dejable && Input.GetKeyDown(KeyCode.E) && !done)
        {
            spriteRenderer.enabled = true;
            textoE.gameObject.SetActive(false);
            done = true;
            boxCollider.enabled = false;

        }
    }
}

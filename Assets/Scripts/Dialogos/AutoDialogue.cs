using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoDialogue : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private Movement move;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textName;
    public string[] lines;
    public string[] names;
    public float textSpeed;

    private int index;
    private bool activeE;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        panel.gameObject.SetActive(false);
        textComponent.text = string.Empty;
        textName.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeE){
            move.allowMove = false;
            gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            StartDialogue(); 
            textName.text = names[index];
            activeE = false;
        }

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                if(textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponent.text = lines[index];
                }
            }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            textName.text = names[index];
            StartCoroutine(TypeLine());
        }
        else
        {
            panel.gameObject.SetActive(false);
            move.allowMove = true;
            textComponent.text = string.Empty;
            textName.text = string.Empty;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true;
            move.allowMove = false; 
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = false;
            Destroy(this.gameObject);
        }            
    }
}

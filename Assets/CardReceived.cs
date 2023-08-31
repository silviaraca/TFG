using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReceived : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject director;
    private Player player;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            director.gameObject.SetActive(true);
        }                   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){

        }            
    }
}

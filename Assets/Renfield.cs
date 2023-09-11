using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renfield : MonoBehaviour
{
    private bool activeE;
    private string done;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        activeE = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(activeE)
        {
            done = "done";
            PlayerPrefs.SetString("RenfieldDoor", done);
            PlayerPrefs.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.name.Equals("Player")){
            activeE = true; 
        }                   
    }
}

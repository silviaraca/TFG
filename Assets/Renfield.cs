using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renfield : MonoBehaviour
{
    private bool activeE;
    private string done;
    public Player player;
    public DialogoDecisiones dialogue1;
    public Dialogue dialogue2;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("WinCombate") && !PlayerPrefs.HasKey("RenfieldDoor")){
            dialogue1.enabled = true;
            dialogue2.enabled = false;
        }
        else{
            dialogue1.enabled = false;
            dialogue2.enabled = true;
            dialogue2.activa();
            PlayerPrefs.DeleteKey("WinCombate");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.HasKey("RenfieldDoor") && dialogue2.indexFin())
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

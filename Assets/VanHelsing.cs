using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanHelsing : MonoBehaviour
{
    public DialogoDecisiones dialogue1;
    public Dialogue dialogue2;
    private bool activeE;
    private Player player;
    private string rat;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Tutorial"))
        {
            dialogue1.enabled = false;
            dialogue2.enabled = true;
            if(!PlayerPrefs.HasKey("Tutorial2")){
                dialogue2.activa();
                string tuto = "done";
                PlayerPrefs.SetString("Tutorial2", tuto);
                PlayerPrefs.Save();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerPrefs.HasKey("Tutorial") && !PlayerPrefs.HasKey("Tutorial2"))
        {
            dialogue1.enabled = true;
            dialogue2.enabled = false;
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

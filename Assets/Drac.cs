using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drac : MonoBehaviour
{

    public Dialogue dialogue;
    private string drac;
    // Start is called before the first frame update
    void Start()
    {
        drac = "done";
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogue.indexFin())
        {
            if(!PlayerPrefs.HasKey("OfficeDoor")) PlayerPrefs.SetString("OfficeDoor", drac);
        }
    }
}

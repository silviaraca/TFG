using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabs : MonoBehaviour
{
    public static int num;
    public Dialogue dialogueScript1;
    public Dialogue dialogueScript2; 
    // Start is called before the first frame update

    void Start()
    {
        if (PlayerPrefs.HasKey("NumJabs"))
        {
            string number = PlayerPrefs.GetString("NumJabs");
            num = int.Parse(number); 
        }
        else
        {
            num = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(num == 3)
        {
            dialogueScript1.enabled = false;
            dialogueScript2.enabled = true;
        }
        else
        {
            dialogueScript1.enabled = true;
            dialogueScript2.enabled = false;
        }

        string numString = num.ToString();
        PlayerPrefs.SetString("NumJabs", numString);
        PlayerPrefs.Save();
    }
}

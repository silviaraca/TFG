using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jabs : MonoBehaviour
{
    public static int num;
    // Start is called before the first frame update

    void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(num == 3)
        {
            Debug.Log("Hecho");
        }
    }
}

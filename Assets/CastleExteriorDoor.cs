using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleExteriorDoor : MonoBehaviour
{
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("RenfieldDoor"))
        {
            collider.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging = false;


    void Start()
    {
        
    }

    public void startDrag(){
        isDragging = true;
    }

    public void endDrag(){
        isDragging = false;
    }
    void Update()
    {
        if(isDragging){
            transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }
    }

    
}

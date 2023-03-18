using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zoom : MonoBehaviour
{
    public GameManager gm;
    public GameObject Carta;

    private GameObject carta;
    private Sprite zoom;

    public void Awake(){
        zoom = gameObject.GetComponent<Image>().sprite;
    }

    public void OnHoverEnter(){
        carta = Instantiate(Carta);
        carta.transform.SetParent(gm.Canvas.transform, true);
        carta.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y + 150);
        carta.GetComponent<Image>().sprite = zoom;

        RectTransform rect = carta.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(10000, 10000);
    }

    public void OnHoverExit(){
        Destroy(carta);
    }
}

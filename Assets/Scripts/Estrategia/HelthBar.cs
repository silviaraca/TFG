using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelthBar : MonoBehaviour
{
    public GameObject bar;
    public Image healthBar;
    public float maxHealth;
    private List<Image> barras = new List<Image>();
    private float posIni, dist, widthA;
    void Start()
    {
        int x = 2; //Trabajar en esta x
        RectTransform rt = healthBar.rectTransform;
        widthA = ((rt.rect.width - (2 + (maxHealth-1))*5)) / maxHealth; //No tocar
        posIni = (healthBar.transform.position.x - (rt.rect.width/2)) + (rt.rect.width / (maxHealth + 1) + x*maxHealth);
        print(posIni);
        print(healthBar.transform.position.x);
        for(int i = 0; i < maxHealth; i++){
            GameObject a = Instantiate(bar);
            a.transform.SetParent(healthBar.transform, false);
            a.transform.position = new Vector2(posIni + ((widthA - widthA/(maxHealth + 1) + x)*i), healthBar.transform.position.y);
            a.gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(widthA, a.gameObject.GetComponent<Image>().rectTransform.sizeDelta.y +2);
        }
    }
    void Update()
    {

    }

    /*
        GameObject imgObject = new GameObject("testAAA");

        RectTransform trans = imgObject.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(0.5f, 0.5f);
        trans.localPosition = new Vector3(0, 0, 0);
        trans.position = new Vector3(0, 0, 0);


        Image image = imgObject.AddComponent<Image>();
        Texture2D tex = Resources.Load<Texture2D>("red");
        image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        imgObject.transform.SetParent(canvas.transform);
    */
}

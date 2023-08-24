using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Slots_UI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI countAddedText;
    public TextMeshProUGUI countRemainingText;
    public string nombreCarta;
    [SerializeField] private Slot associatedSlot;

    public void Initialize(Slot slot)
    {
        associatedSlot = slot;
        UpdateUI();
    }

    private void UpdateUI()
    {
        itemIcon.sprite = associatedSlot.icon;
        itemIcon.color = new Color(1, 1, 1, 1);
        UpdateCountAddedText(associatedSlot.getCountAdded());
        UpdateCountRemainingText(associatedSlot.getCountRemaining());
    }

    private void UpdateCountAddedText(int newValue)
    {
        countAddedText.text = newValue.ToString();
    }

    private void UpdateCountRemainingText(int newValue)
    {
        countRemainingText.text = newValue.ToString();
    }

    public void OnPlusButtonClick()
    {
        associatedSlot.Sumar();
        Player player = FindObjectOfType<Player>();
        player.deck.Add(nombreCarta);
        UpdateUI();
    }

    public void OnMinusButtonClick()
    {
        associatedSlot.Restar();
        Player player = FindObjectOfType<Player>();
        player.deck.Remove(nombreCarta);
        UpdateUI();
    }
}

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
        UpdateUI();
    }

    public void OnMinusButtonClick()
    {
        associatedSlot.Restar();
        UpdateUI();
    }

    private void OnDestroy()
    {
        // Darse de baja de los eventos cuando el objeto se destruye
        if (associatedSlot != null)
        {
            associatedSlot.OnCountAddedChanged -= UpdateCountAddedText;
            associatedSlot.OnCountRemainingChanged -= UpdateCountRemainingText;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class Inventory : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject slot;
    public int count; // Items ahora mismo
    public int maxSlots;
    public int busySlots;
    public Sprite Agua, Estaca, Sangre, Tumba, Mina, Aldeano, Ajo, Nosferatu, Lamias, Logan, Carreta, Cuchillo, Ratonela;
    private List<string> inventory = new List<string>();

    // Inicializar
    public void Start()
    {
        count = 0;
        busySlots = 0;
        this.maxSlots = 22;
        slots.Add(slot);
        for (int i = 1; i < 22; i++)
        {
            GameObject slotAux = Instantiate(slot, transform);
            slots.Add(slotAux);
        }
        cargaInventory();
    }

    //Cargar el JSON si existente
    public void cargaInventory(){
        if(PlayerPrefs.HasKey("InventoryCards")){
            string inventoryData = PlayerPrefs.GetString("InventoryCards");
            inventory = JsonConvert.DeserializeObject<List<string>>(inventoryData);
        }
        for(int i = 0; i < inventory.Count; i++){
            slots[i].gameObject.GetComponent<Slot>().nombre = inventory[i];
            insertaSprite(slots[i].gameObject.GetComponent<Slot>(), inventory[i]);
        }
    }

    // Se añade un item
    public bool Add(Collectable item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            // Items ya existentes
            if (slots[i].GetComponent<Slot>().type == (CollectableType)item.type)
            {
                if (slots[i].GetComponent<Slot>().max > slots[i].GetComponent<Slot>().count)
                {
                    // Se añade el item
                    slots[i].GetComponent<Slot>().AddItem(item);
                    return true;
                }
                else return false;
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            // Items nuevos
            if (slots[i].GetComponent<Slot>().type == CollectableType.NONE)
            {
                slots[i].GetComponent<Slot>().AddItem(item);
                busySlots++;
                return true;
            }
        }

        return false;
    }

    private void insertaSprite(Slot s, string nombre){
        if(nombre == "Agua"){
            s.icon = Agua;
        }
        else if(nombre == "Estaca"){
            s.icon = Estaca;
        }
        else if(nombre == "Sangre"){
            s.icon = Sangre;
        }
        else if(nombre == "Tumba"){
            s.icon = Tumba;
        }
        else if(nombre == "Mina"){
            s.icon = Mina;
        }
        else if(nombre == "Aldeano"){
            s.icon = Aldeano;
        }
        else if(nombre == "Ajo"){
            s.icon = Ajo;
        }
        else if(nombre == "Nosferatu"){
            s.icon = Nosferatu;
        }
        else if(nombre == "Lamias"){
            s.icon = Lamias;
        }
        else if(nombre == "Logan"){
            s.icon = Logan;
        }
        else if(nombre == "Carreta"){
            s.icon = Carreta;
        }
        else if(nombre == "Cuchillo"){
            s.icon = Cuchillo;
        }
        else if(nombre == "Ratonela"){
            s.icon = Ratonela;
        }
    }

    
}

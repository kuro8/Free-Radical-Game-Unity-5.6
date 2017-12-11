using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour {
  [SerializeField] private GameObject parent;
  [SerializeField] private GameObject itemSlot;
  private Inventory inventory;
  private List<ItemGUI>itemsGUI = new List<ItemGUI>();
  public ItemGUI currentItemGUI = null;
  public bool isBusy = false;
  public Text dialogText;
  private BattleManager manager;

  void Start(){
    manager = FindObjectOfType<BattleManager>();
    inventory = FindObjectOfType<Inventory>();
    List<Item>items = inventory.items;
    for(int i=0; i<items.Count; i++)
      AddItemGUI(items[i]);
  }

  public void AddItemGUI(Item item){
    GameObject newSlot = (GameObject) Instantiate(itemSlot, itemSlot.transform.position, itemSlot.transform.rotation);
    //set item, text and image
    ItemGUI gui = newSlot.GetComponent<ItemGUI>();
    gui.item = item;
    gui.text.text = LocalizationManager.Instance.getLocalizatedValue(item.key);
    gui.life = item.life;
    if(gui.life > 0)
      gui.text.text += " x"+ gui.life;
    gui.image.sprite = item.sprite;
    //set parent
    newSlot.transform.SetParent(parent.transform, false);
    //add to list
    itemsGUI.Add(gui);
  }

  void RemoveItemGUI(Item item){
    //remove item from inventory
    int index = inventory.RemoveItem(item);
    //destroy item gui
    GameObject temp = itemsGUI[index].gameObject;
    itemsGUI.RemoveAt(index);
    Destroy(temp);
  }

  public void useItemInNPC(){
    if(!isBusy && currentItemGUI != null){
      if(currentItemGUI.item.filter < 1 || currentItemGUI.item.antoxidant > 0 || currentItemGUI.item.damage > 0){
        manager.hpSlider.value -= currentItemGUI.item.damage;
        manager.antoxidant += currentItemGUI.item.antoxidant;
        manager.filter = (currentItemGUI.item.filter == 1)? manager.filter : currentItemGUI.item.filter;
        if(currentItemGUI.life != 0){
            currentItemGUI.life--;
            currentItemGUI.text.text = LocalizationManager.Instance.getLocalizatedValue(currentItemGUI.item.key);
          if(currentItemGUI.life > 0)
            currentItemGUI.text.text += " x" + currentItemGUI.life;
          else{
            RemoveItemGUI(currentItemGUI.item);
            currentItemGUI = null;
          }
        }
      }
      else{
        StartCoroutine(displayText());
      }
    }
  }

  IEnumerator displayText(){
    dialogText.enabled = true;
    isBusy = true;
    yield return new WaitForSeconds(1f);
    currentItemGUI.backImage.enabled = false;
    currentItemGUI = null;
    dialogText.enabled = false;
    isBusy = false;
  }

}

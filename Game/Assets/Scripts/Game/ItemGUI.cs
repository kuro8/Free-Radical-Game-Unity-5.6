using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGUI : MonoBehaviour {
  public Text text;
  public Image image;
  public Image backImage;
  public Item item;
  public int life = 0;

  private InventoryGUI inventoryGUI;

  void Start(){
    inventoryGUI = FindObjectOfType<InventoryGUI>();
  }

  public void SelectItem(){
    if(!inventoryGUI.isBusy && Time.timeScale == 1){
      if(inventoryGUI.currentItemGUI == this){
        backImage.enabled = false;
        inventoryGUI.currentItemGUI = null;
      }
      else{
        if(inventoryGUI.currentItemGUI != null)
          inventoryGUI.currentItemGUI.backImage.enabled = false;
        inventoryGUI.currentItemGUI = this;
        backImage.enabled = true;
      }
    }
  }
}

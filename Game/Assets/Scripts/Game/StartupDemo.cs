using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartupDemo : MonoBehaviour {
	public Text text;
	public BattleManager manager;
	public List<Item>items = new List<Item>();
	public Inventory inventory;

	void Awake(){
		text.text += string.Format(LocalizationManager.Instance.getLocalizatedValue("survive"), manager.time);
		for(int i=0; i<items.Count; i++)
			inventory.AddItem(items[i]);
	}

	void Start(){
		Time.timeScale= 0;
	}

	public void unpause(){
		Time.timeScale= 1;
	}



}

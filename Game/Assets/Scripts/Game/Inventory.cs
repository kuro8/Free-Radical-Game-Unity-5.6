using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public List<Item>items = new List<Item>();

	public void AddItem(Item item){
		items.Add(item);
	}

	public int RemoveItem(Item item){
		int i = items.IndexOf(item);
		if(i < items.Count)
			items.RemoveAt(i);
		return i;
	}

}

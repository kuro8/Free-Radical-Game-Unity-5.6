using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {
	public Sprite sprite;
	public string key;
	public int life = 1;
	public int spf;
	public float filter = 1;
	public int antoxidant;
	public int damage;
}

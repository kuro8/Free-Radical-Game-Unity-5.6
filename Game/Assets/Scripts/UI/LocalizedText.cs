using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour {
	public string key;
	Text text;

	void Start () {
		text = GetComponent<Text> ();
		updateText ();
	}

	public void updateText(){
		text.text = LocalizationManager.Instance.getLocalizatedValue (key);
	}

}

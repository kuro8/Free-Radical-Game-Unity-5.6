using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager: Singleton<LocalizationManager> {
	private Dictionary<string, string> localizedUI;
	private string currentLanguage = "";
	private bool isReady = false;

	protected LocalizationManager(){}

	void Awake(){
		if (PlayerPrefs.HasKey ("lang")) {
			LoadLocalizedText (PlayerPrefs.GetString ("lang"));
		} else {
			LoadLocalizedText (Application.systemLanguage.ToString ());
		}
	}

	public void LoadLocalizedText(string lang){
		string dataAsJson = "";
		try {
			dataAsJson = Resources.Load<TextAsset> ("UI-" + lang).text;
		} catch {
			lang = "English";
			dataAsJson = Resources.Load<TextAsset> ("UI-" + "English").text;
		}
		currentLanguage = lang;
		PlayerPrefs.SetString ("lang", lang);

		localizedUI = new Dictionary<string, string> ();
		LocalizationText data = JsonUtility.FromJson<LocalizationText> (dataAsJson);
		for (int i = 0; i < data.items.Length; i++) {
			localizedUI.Add (data.items [i].key, data.items [i].value);
		}
		isReady = true;
	}


	public string getLocalizatedValue(string key){
		if (!localizedUI.ContainsKey (key)) {
			Debug.Log ("Error: Missing text for key \"" + key + "\" [" + currentLanguage + "]");
			return "Missing text";
		}
		return localizedUI [key];
	}

	public bool GetIsReady(){
		return isReady;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : MonoBehaviour {
	public GameObject loadScreen;
	public Toggle bgm;
	public Toggle sfx;
	public Text versionText;
	public Text progressText;
	private LocalizedText[] texts;

	void Awake(){
		bgm.isOn = AudioManager.Instance.GetVolume("bgm") == 0f;
		sfx.isOn = AudioManager.Instance.GetVolume("sfx") == 0f;
		texts = FindObjectsOfType<LocalizedText> ();
		versionText.text = "Ver." + Application.version;

		bgm.onValueChanged.AddListener ( (value) => {
			if(value) AudioManager.Instance.SavePrefs("bgm", 0f);
			else AudioManager.Instance.SavePrefs("bgm", -80f);
		});
		sfx.onValueChanged.AddListener ( (value) => {
			if(value) AudioManager.Instance.SavePrefs("sfx", 0f);
			else AudioManager.Instance.SavePrefs("sfx", -80f);
		});

		float percent = ProgressManager.Instance.getProgress();
		progressText.text = (percent > -1)? "Best: " + string.Format("{0:0.##}", (percent*100))+ "%" : "";
	}

	void Update(){
		if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
			ExitApplication();
	}

	public void ExitApplication(){
		Application.Quit ();
	}

	public void ModifyLanguage(string lang){
		LocalizationManager.Instance.LoadLocalizedText (lang);
		for (int i = 0; i < texts.Length; i++)
			texts [i].updateText ();
	}

	public void PlayGame(int scene){
		loadScreen.SetActive (true);
		StartCoroutine (LoadNewScene (scene));
	}

	IEnumerator LoadNewScene(int scene){
		AsyncOperation async = SceneManager.LoadSceneAsync (scene);
		while(!async.isDone)
			yield return async.isDone;
	}
}

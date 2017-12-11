using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour {

	public void PauseGame(){
		if(Time.timeScale == 0){
			Time.timeScale = 1;
			AudioManager.Instance.SavePrefs("gbgm", 0f);
			AudioManager.Instance.SavePrefs("gsfx", 0f);
		}
		else{
			Time.timeScale = 0;
			AudioManager.Instance.SavePrefs("gbgm", -50f);
			AudioManager.Instance.SavePrefs("gsfx", -20f);
		}
	}

	public void LoadScene(string scene){
		UnityEngine.SceneManagement.SceneManager.LoadScene (scene);
	}
}

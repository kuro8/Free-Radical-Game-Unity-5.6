using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour {

	IEnumerator Start () {
		while (!LocalizationManager.Instance.GetIsReady () ||
			!AudioManager.Instance.GetIsReady () ||
			!ProgressManager.Instance.GetIsReady())
			yield return null;
		DontDestroyOnLoad (gameObject);
		AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("Menu");
		while(!async.isDone)
			yield return async.isDone;
	}
}

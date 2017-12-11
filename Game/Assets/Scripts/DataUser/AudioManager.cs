using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>  {
	[SerializeField] private AudioMixer mixer;
	private bool isReady = false;

	protected AudioManager(){}

	void Awake(){
		LoadAudioPrefs ();
	}

	void LoadAudioPrefs(){
		float bgm = 0f;
		float sfx = 0f;
		if (PlayerPrefs.HasKey ("bgm")) {
			bgm = PlayerPrefs.GetFloat ("bgm");
			sfx = PlayerPrefs.GetFloat ("sfx");
		}
		SavePrefs("bgm", bgm);
		SavePrefs("sfx", sfx);
		isReady = true;
	}

	public void SavePrefs(string key, float value){
		PlayerPrefs.SetFloat (key.ToLower(), value);
		SetVolume(key, value);
	}

	public void SetVolume(string key, float value){
		mixer.SetFloat (key.ToUpper(), value);
	}

	public float GetVolume(string key){
		float value = 0;
		mixer.GetFloat(key.ToUpper(), out value);
		return value;
	}

	public bool GetIsReady(){
		return isReady;
	}
}

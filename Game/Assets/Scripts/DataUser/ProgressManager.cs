using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ProgressManager : Singleton<ProgressManager> {
	private string filePath;
	private Progress dataProgress;
	private bool isReady = false;

	protected ProgressManager(){}

	void Awake(){
		filePath = Path.Combine(Application.persistentDataPath, "demo.json");
		LoadDataProgress ();
	}

	void LoadDataProgress(){
		if(File.Exists(filePath)){
			string dataAsJson = File.ReadAllText(filePath);
			dataProgress =  JsonUtility.FromJson<Progress>(dataAsJson);
		}
		else{
			dataProgress = new Progress();
			dataProgress.id = 2;
			dataProgress.finalHP = -1f;
		}
		isReady = true;
	}

	public void SaveProgress(int id, float finalHP){
		if(finalHP > dataProgress.finalHP){
			dataProgress.finalHP = finalHP;
			string dataAsJson = JsonUtility.ToJson(dataProgress);
			File.WriteAllText(filePath, dataAsJson);
		}
	}

	public bool GetIsReady(){
		return isReady;
	}

	public float getProgress(){
		return dataProgress.finalHP;
	}
}

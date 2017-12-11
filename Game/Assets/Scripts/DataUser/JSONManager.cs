using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONManager : Singleton<JSONManager> {

	protected JSONManager(){}

	public T LoadJSON<T>(string filePath) where T : new(){
		if(File.Exists(filePath)){
			string dataAsJson = File.ReadAllText(filePath);
			T data =  JsonUtility.FromJson<T>(dataAsJson);
			return data;
		}
		return default(T);
	}

	public void SaveJSON<T>(string filePath, T dataToSave){
		string dataAsJson = JsonUtility.ToJson(dataToSave);
		File.WriteAllText(filePath, dataAsJson);
	}
}

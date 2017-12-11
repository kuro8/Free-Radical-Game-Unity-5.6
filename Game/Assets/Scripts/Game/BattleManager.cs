using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {
	public Text uvText;
	public Slider hpSlider;
	public Slider frSlider;
	public Slider filterSlider;
	public Text timeText;
	public GameObject finishCanvas;
	public Text finishText;

	public GameObject molecule;
	public CanvasGroup fader;
	private bool isFading = false;

	public int uv = 0;
	public float filter = 1f;
	private float filterWeight = 0.5f;
	public int spawnValue = 3;
	public int antoxidant = 35;
	public float time = 90f;

	private List<Molecule> molecules = new List<Molecule>();
	private const int maxMolecules = 25;

	private bool isBusy = false;
	private float startTime;

	void Awake(){
		startTime = Time.time;
		uvText.text = "UV : " + string.Format("{0,2}", uv);

		for(int i=0; i<maxMolecules; i++){
			InstantiateGameObject();
		}
	}

	void FixedUpdate(){
		//display time
		float timeRemain = startTime + time - Time.time;
		timeText.text = string.Format("{0:#0}", timeRemain);

		if(timeRemain > 0 && hpSlider.value > 0){
			StartCoroutine(EnemyTurn());
		}
		else{
			Time.timeScale = 0;
			//save the progress
			ProgressManager.Instance.SaveProgress(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, (hpSlider.value/hpSlider.maxValue));
			//display the result
			if(hpSlider.value/hpSlider.maxValue >= 0.5f){
				finishText.text = LocalizationManager.Instance.getLocalizatedValue("success");
			}
			else if(hpSlider.value/hpSlider.maxValue >= 0.25f){
				finishText.text = LocalizationManager.Instance.getLocalizatedValue("good");
			}
			else if(hpSlider.value/hpSlider.maxValue > 0){
				finishText.text = LocalizationManager.Instance.getLocalizatedValue("acceptable");
			}
			else {
				finishText.text = LocalizationManager.Instance.getLocalizatedValue("failed");
			}
			finishCanvas.SetActive(true);
		}
	}

	IEnumerator EnemyTurn(){
		if(!isBusy){
			isBusy = true;
			yield return new WaitForSeconds (1f);
			int spawned = SpawnNewFR();
			int damage = StealEletrons((int)frSlider.value + spawned);
			yield return StartCoroutine( AnimateBar(frSlider, spawned) );
			yield return StartCoroutine( AnimateBar(hpSlider, (int)(-damage*0.75)) );
			if(isFading)
				yield return StartCoroutine(AnimateField(0.25f));
			yield return StartCoroutine( AnimateMolecule(damage) );
			isBusy = false;
		}
	}

	public int StealEletrons(int numFreeRadicals){
		return Random.Range((int)(numFreeRadicals/10), numFreeRadicals);
	}

	int SpawnNewFR(){
		//use antoxidants
		int heal = Random.Range(0, Mathf.Min(antoxidant, (int)frSlider.value));
		antoxidant -= heal;
		if(heal > 0)
			isFading = true;
		//exogen
		int fex= (int)(uv * filter * filterWeight * spawnValue) - heal*2;
		//endogen
		int fen = spawnValue;
		//filter
		filter = Mathf.Min((0.0075f*uv*filter) + filter, 1);
		filterSlider.value = 1f - filter;
		return (fen + fex - heal);
	}

	IEnumerator AnimateMolecule(int damage){
		int maxFR = (int)(frSlider.value * maxMolecules / frSlider.maxValue);

		int i=0;
		//FR
		while(i<maxFR){
			molecules[i].animState = -1;
			molecules[i++].anim.SetInteger("change", -1);
			yield return new WaitForSeconds(0.01f);
		}
		while(i<maxMolecules){
			molecules[i].animState = 1;
			molecules[i++].anim.SetInteger("change", 1);
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator AnimateBar(Slider slider, int value){
		int i = 0;
		int increment = (value > 0)? 1: -1;
		while(i != value){
			slider.value += increment;
			i += increment;
			yield return new WaitForSeconds(0.01f);
		}
	}

	void InstantiateGameObject(){
		GameObject obj =(GameObject) Instantiate(molecule, molecule.transform.position, molecule.transform.rotation);
		molecules.Add(obj.GetComponent<Molecule>());
	}

	IEnumerator AnimateField(float finalAlpha){
		float fadeSpeed = Mathf.Abs (fader.alpha - finalAlpha) / 0.5f;
		while (!Mathf.Approximately (fader.alpha, finalAlpha)){
			fader.alpha = Mathf.MoveTowards (fader.alpha, finalAlpha,
				fadeSpeed * Time.deltaTime);
			yield return null;
		}
		while (!Mathf.Approximately (fader.alpha, 0f)){
			fader.alpha = Mathf.MoveTowards (fader.alpha, 0f,
				fadeSpeed * Time.deltaTime);
			yield return null;
		}
		isFading = false;
	}

}

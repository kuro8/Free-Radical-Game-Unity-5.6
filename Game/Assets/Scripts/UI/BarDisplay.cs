using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarDisplay : MonoBehaviour {
  private Text text;
  private Slider slider;

  void Awake(){
    text = GetComponent<Text>();
    slider = GetComponentInParent<Slider>();
  }

  void FixedUpdate(){
    text.text = ((int)slider.value).ToString();
  }
}

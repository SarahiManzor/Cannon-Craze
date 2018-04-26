using UnityEngine;
using System.Collections;

public class _ShopButton : MonoBehaviour {

	
	void Awake() {
	}
	
	void OnTouchUp(){
		Time.timeScale = 1f;
		Application.LoadLevel ("shop");
	}
}

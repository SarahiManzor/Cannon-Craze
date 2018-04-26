using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {
	private float startTime;
	
	void Start () {
		startTime = Time.realtimeSinceStartup;
		transform.localScale = new Vector3(0.1f,0.1f,1f);
	}
	
	// Update is called once per frame
	void Update () {
		float timePassed = Time.realtimeSinceStartup - startTime;
		float scaleX = Mathf.Lerp(transform.localScale.x, 1f, timePassed/4f);
		float scaleY = Mathf.Lerp(transform.localScale.y, 1f, timePassed/4f);
		transform.localScale = new Vector3(scaleX, scaleY, 1f);
	}
}

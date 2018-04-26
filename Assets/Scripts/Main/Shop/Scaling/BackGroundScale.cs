using UnityEngine;
using System.Collections;

public class BackGroundScale : MonoBehaviour {

	private Camera cam;
	private float screenWidth;
	
	public bool constant = false;
	
	void Awake () 
	{
		cam = Camera.main;
		screenWidth = cam.orthographicSize * Screen.width/Screen.height;
	}
	
	void Start()
	{
		float scale = 1.775f;
		float currentScale = screenWidth/cam.orthographicSize;
		float moveScale = currentScale/scale;
		moveScale = Mathf.Clamp01(moveScale);
		
		transform.localScale = new Vector3(moveScale,1f,1f);
	}
	
	void Update()
	{
		if(constant)
		{
			float scale = 1.775f;
			screenWidth = cam.orthographicSize * Screen.width/Screen.height;
			float currentScale = screenWidth/cam.orthographicSize;
			float moveScale = currentScale/scale;
			moveScale = Mathf.Clamp01(moveScale);
			
			transform.localScale = new Vector3(moveScale,1f,1f);
		}
	}
}

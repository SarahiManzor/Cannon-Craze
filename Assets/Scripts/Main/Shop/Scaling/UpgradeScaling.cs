using UnityEngine;
using System.Collections;

public class UpgradeScaling : MonoBehaviour {
	
	public GameObject[] pages;
	private Camera cam;
	private float screenWidth;
	//private UpgradeDrag ud;
	
	public float leftSidePosX;
	
	void Awake () 
	{
		cam = Camera.main;
		screenWidth = cam.orthographicSize * Screen.width/Screen.height;
		//ud = GameObject.Find ("UpgradeDrag").GetComponent<UpgradeDrag>();
	}
	
	void Start()
	{
		float scale = 1.775f;
		float currentScale = screenWidth/cam.orthographicSize;
		float moveScale = currentScale/scale;
		
		for(int i = 0; i < pages.Length;i++)
		{
			float initialPos = leftSidePosX-3.55f;
			float targetPos = initialPos*moveScale + 3.55f;
			pages[i].transform.position = new Vector3(targetPos,pages[i].transform.position.y,pages[i].transform.position.z);
		}
	}
}

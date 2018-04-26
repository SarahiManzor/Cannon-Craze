using UnityEngine;
using System.Collections;

public class Scaling : MonoBehaviour {

	public GameObject[] itemsToMove;
	private Camera cam;
	private float screenWidth;
	
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
		for(int i = 0; i < itemsToMove.Length;i++)
		{
			float initialPos = itemsToMove[i].transform.position.x - cam.transform.position.x;
			float targetPos = initialPos*moveScale + cam.transform.position.x;
			itemsToMove[i].transform.position = new Vector3(targetPos,itemsToMove[i].transform.position.y,itemsToMove[i].transform.position.z);
		}
	}
}

using UnityEngine;
using System.Collections;

public class LabelPosit : MonoBehaviour {

	public float offSetX = 0.1f;
	public float offSetY = 0.1f;
	
	private Camera cam;
	
	void Awake () 
	{
		cam = Camera.main;
	}
	
	void Start () 
	{
		float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
		Debug.Log (camHorizontalExtend);
		Debug.Log (cam.orthographicSize);
		
		float newX = cam.transform.position.x - camHorizontalExtend + offSetX;
		float newY = cam.transform.position.y + cam.orthographicSize + offSetY;
		
		transform.position = new Vector3(newX,newY,transform.position.z);
	}
}

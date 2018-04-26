using UnityEngine;
using System.Collections;

public class LabelPositioner : MonoBehaviour {

	public GameObject [] children;
	private float lastPos = 0f;
	
	public float spacingY = 0.1f;
	public float offSetX = 0.1f;
	public float offSetY = 0.1f;
	
	private Camera cam;

	void Awake () 
	{
		cam = Camera.main;
		lastPos = cam.transform.position.y + cam.orthographicSize - offSetY;
	}
	
	void Start () 
	{
		float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
		for (int i = 0; i < children.Length; i++)
		{
			Bounds mRend = children[i].GetComponent<TextMesh>().GetComponent<Renderer>().bounds;
			float newX = cam.transform.position.x - camHorizontalExtend + mRend.size.x + offSetX;
			float newY = lastPos;
			children[i].transform.position = new Vector3(newX,newY,children[i].transform.position.z);
			lastPos = children[i].transform.position.y - mRend.size.y - spacingY;
		}
	}
}

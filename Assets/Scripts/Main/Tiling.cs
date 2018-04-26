using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public bool hasRightBuddy = false;
	public bool hasLeftBuddy = false;

	public bool reverseScale = false;

	private float spriteWidth = 0f;
	private Camera cam;
	private Transform myTransform;
	private float camHorizontalExtend;

	void Awake() {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () 
	{
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x * Mathf.Abs(transform.localScale.x);
		camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!hasRightBuddy)
		{
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
	
			if(cam.transform.position.x >= edgeVisiblePositionRight)
			{
				makeNewBuddy();
				hasRightBuddy = true;
			}
		}
		else
		{
			float rightEdge = myTransform.position.x + spriteWidth/1.5f;
			//Leftmost side of camera
			float camLeft = cam.transform.position.x - camHorizontalExtend;
			
			if(camLeft > rightEdge){
				Destroy (gameObject);
			}
		}
	}

	void makeNewBuddy ()
	{
		Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth - 0.01f, myTransform.position.y, myTransform.position.z);
		Transform newBuddy = (Transform) Instantiate (myTransform, newPosition, myTransform.rotation);

		if (reverseScale == true)
		{
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
	}
}

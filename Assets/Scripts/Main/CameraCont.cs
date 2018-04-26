using UnityEngine;
using System.Collections;

public class CameraCont : MonoBehaviour 
{
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.
	
	private float camHorizontalExtend;
	
	private Transform player;		// Reference to the player's transform.
	private Camera cam;
	
	
	void Awake ()
	{
		cam = Camera.main;
		camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		minXAndY.x += camHorizontalExtend-0.3f;
		Debug.Log ((cam.orthographicSize * Screen.width/Screen.height)/cam.orthographicSize);
		FixedUpdate();
	}
	
	
	void FixedUpdate ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		//float targetX = transform.position.x;
		//float targetY = transform.position.y;
		
		float targetX = player.position.x + player.GetComponent<Rigidbody2D>().velocity.x/50f + camHorizontalExtend/2f;
		float targetY = player.position.y + player.GetComponent<Rigidbody2D>().velocity.y/50f;
		
		// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
		targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
		targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
		
		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}

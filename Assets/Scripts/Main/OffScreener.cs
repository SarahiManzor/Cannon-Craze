using UnityEngine;
using System.Collections;

public class OffScreener : MonoBehaviour {

	private Camera cam;
	private float spriteWidth;
	private float rightEdge;
	private float camLeft;
	private float camHorizontalExtend;
	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x * Mathf.Abs(transform.localScale.x);
		cam = Camera.main;
		camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		rightEdge = transform.position.x + spriteWidth+1f;
		//Leftmost side of camera
		camLeft = cam.transform.position.x - camHorizontalExtend;
		
		if(camLeft > rightEdge && (GetComponent<AudioSource>()==null||GetComponent<AudioSource>().isPlaying == false))
		{
			Destroy (gameObject);
		}
	}
}

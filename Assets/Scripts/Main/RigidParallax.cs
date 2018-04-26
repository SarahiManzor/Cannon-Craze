using UnityEngine;
using System.Collections;

public class RigidParallax : MonoBehaviour {

	public Transform[] backgrounds;				// Array of all the backgrounds to be parallaxed.
	public float parallaxScale = 1f;
	public float yScaleFactor;
	private float scale;
	
	private Transform player;
	private Camera cam;
	private float camPos;
	
	private float[] startY;
	private float camStart;
	
	void Awake () 
	{
		player = GameObject.Find("Player").transform;
		cam = Camera.main;
	}
	
	void Start()
	{
		startY = new float[backgrounds.Length];
		camStart = cam.transform.position.y - 0.5f;
		
		for(int i = 0; i < backgrounds.Length; i++)
		{
			startY[i] =  backgrounds[i].position.y + camStart/10f;
		}
		camPos = cam.orthographicSize * Screen.width/Screen.height - 2.3f;
	}
	
	void Update () 
	{
		for(int i = 0; i < backgrounds.Length; i++)
		{
			if(cam.transform.position.x > camPos)
			{
				float newX = player.GetComponent<Rigidbody2D>().velocity.x/(10f-backgrounds[i].position.z) * parallaxScale;
				backgrounds[i].GetComponent<Rigidbody2D>().velocity = new Vector2(newX,backgrounds[i].GetComponent<Rigidbody2D>().velocity.y);
			}
			
			float newY = startY[i] + ((cam.transform.position.y-0.5f) - camStart)/10f * backgrounds[i].position.z * yScaleFactor;
			backgrounds[i].position = new Vector3(backgrounds[i].position.x,newY,backgrounds[i].position.z);
				
		}
	}
}

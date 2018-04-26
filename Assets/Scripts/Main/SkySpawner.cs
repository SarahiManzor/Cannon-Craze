using UnityEngine;
using System.Collections;

public class SkySpawner : MonoBehaviour 
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float scale;
	public float minY = 8f;
	public float range = 45f;
	
	public float cloudsPer = 5f;
	public float distanceToReady = 5f;
	private float spawned = 0f;
	private float lastPos;
	private bool ready = true;
	
	public GameObject skyThing;
	
	private Transform player;
	private Camera cam;
	
	private float camHorizontalExtend;
	private float camVerticalExtend;
	
	public bool onScreenOnStart = false;

	void Awake () 
	{
		cam = Camera.main;
		camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
		camVerticalExtend = cam.orthographicSize;
		player = GameObject.Find ("Player").transform;
	}
	
	void Start()
	{
		if(onScreenOnStart)
		{
			SpawnFirstCouple();
		}
		InvokeRepeating("Spawn", 0f, spawnTime);
	}
	
	void SpawnFirstCouple()
	{
		int amountOfClouds = Random.Range (1,3);
		for(int i = 0; i < amountOfClouds; i++)
		{
			float randomX = Random.Range (cam.transform.position.x - camHorizontalExtend, cam.transform.position.x + camHorizontalExtend/2f);
			float randomY = Random.Range (cam.transform.position.y, cam.transform.position.y + camVerticalExtend);
			Vector3 newPos = new Vector3(randomX,randomY,transform.position.z);
			
			GameObject newBuddy = (GameObject) Instantiate (skyThing, newPos, transform.rotation);
			newBuddy.transform.parent = gameObject.transform;
			newBuddy.transform.localScale = new Vector3(scale,scale,1f);
			newBuddy.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,scale*1.75f);
			newBuddy.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
		}
	}
	
	void Spawn () 
	{
		//if(player.position.y > minY - camVerticalExtend)
		if(ready && player.position.z >= -1f)
		{
			float angle = Mathf.Atan2(player.GetComponent<Rigidbody2D>().velocity.y,player.GetComponent<Rigidbody2D>().velocity.x)*Mathf.Rad2Deg;
			if(Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.y) < 5f)
			{
				angle = 0f;
			}
			angle += Random.Range (-range,range);
			angle *= Mathf.Deg2Rad;
			
			float nextY = player.transform.position.y + Mathf.Sin(angle)*4f;
			if(nextY > minY)
			{
				float nextX = player.transform.position.x + Mathf.Cos(angle)*4f;
				Vector3 newPos = new Vector3(nextX,nextY,transform.position.z);
				
				GameObject newBuddy = (GameObject) Instantiate (skyThing, newPos, transform.rotation);
				newBuddy.transform.parent = gameObject.transform;
				newBuddy.transform.localScale = new Vector3(scale,scale,1f);
				newBuddy.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,scale*1.75f);
				newBuddy.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
				spawned++;
				if(spawned >= 5)
				{
					ready = false;
					lastPos = player.transform.position.x;
				}
			}
		}
		else if(player.transform.position.x - lastPos > distanceToReady)
		{
			ready = true;
		}
	}
}

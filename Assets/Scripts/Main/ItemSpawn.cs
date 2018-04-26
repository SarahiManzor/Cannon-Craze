using UnityEngine;
using System.Collections;

public class ItemSpawn : MonoBehaviour
{
	public float spawnChance = 0f;
	
	public GameObject pickupItem;		// Array of enemy prefabs.
	
	public float spawnTime = 0.5f;
	
	private Transform pickupPack;
	
	private Transform player;
	private Camera cam;
	
	private float camHorizontalExtend;
	private float camVerticalExtend;
	
	private bool ready = true;
	private float lastPos = 0f;
	public float distanceToReady = 10f;
	
	void Awake() 
	{
		pickupPack = transform;
		cam = Camera.main;
		camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;
		camVerticalExtend = cam.orthographicSize;
		player = GameObject.Find ("Player").transform;
	}
	
	void Start()
	{
		InvokeRepeating("Spawn",0f,spawnTime);
	}
	
	void Spawn ()
	{
		if(ready)
		{
			if(player.position.z == -0.1f && player.GetComponent<Rigidbody2D>().velocity.x != 0f)
			{
				float spawnRand = Random.Range(0f,100f);//Chance to spawn something
				if(spawnRand <= spawnChance)
				{
					//Calculating Y
					float minPosY = cam.transform.position.y - camVerticalExtend*1.5f;
					
					if(minPosY < 0.25f)
						minPosY = 0.25f;
					
					float randomPosY = Random.Range (minPosY,minPosY + camVerticalExtend*4f);
					
					//Calculating X
					float minPosX;
					if(randomPosY > cam.transform.position.y + camVerticalExtend*1.5f){
						minPosX = cam.transform.position.x;
					}else{
						minPosX = cam.transform.position.x + camHorizontalExtend*1.5f;
					}
					float randomPosX = Random.Range (minPosX,minPosX + camHorizontalExtend*2f);
					
					Vector3 newPos = new Vector3(randomPosX,randomPosY,0f);
					
					GameObject newBuddy = (GameObject) Instantiate(pickupItem, newPos, transform.rotation);
					newBuddy.transform.parent = transform;
					
					//Instantiate(pickups[pickupIndex], newPos, transform.rotation);
					ready = false;
				}
			}
		}
		else
		{
			if(player.position.x - lastPos > distanceToReady)
			{
				lastPos = player.position.x;
				ready = true;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class InteractSpawner : MonoBehaviour
{
	public float interactSpawnChance = 3f;
	public float pickupSpawnChance = 3f;
	
	public GameObject[] interactables;		// Array of enemy prefabs.
	public GameObject[] pickups;
	
	public float spawnTime = 5f;
	
	private Transform interactPack;
	
	private Transform player;
	private Camera cam;
	
	private float camHorizontalExtend;
	private float camVerticalExtend;
	
	private bool ready = true;
	private float lastPos = 0f;
	public float distanceToReady = 1f;
	
	void Awake() 
	{
		interactPack = GameObject.Find ("InteractPack").transform;
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
			//SPAWNING OF MONSTER
				float spawnRand = Random.Range(0f,100f);//Chance to spawn something
				if(spawnRand <= interactSpawnChance){//If enemies spawned and chance to spawn is 1
					// Instantiate a random enemy.S
					int interactIndex = Random.Range(0, interactables.Length);
					
					float minPosX = player.position.x + camHorizontalExtend*2;
					float randomPosX = Random.Range (minPosX,minPosX + camHorizontalExtend*2);
					
					float minPosY = player.position.y - camVerticalExtend*2;
					
					if(minPosY < 0.25f)
					{
						minPosY = 0.25f;
					}
					float randomPosY = Random.Range (minPosY,minPosY + camVerticalExtend*2);
					Vector3 newPos;
					
					if(interactIndex == 2)
					{
						newPos = new Vector3(randomPosX ,randomPosY,0f);
					}
					else
					{
						newPos = new Vector3(randomPosX,-0.65f,player.position.z);
					}	
					GameObject newBuddy = (GameObject) Instantiate(interactables[interactIndex], newPos, transform.rotation);
					newBuddy.transform.parent = interactPack;
					
					//Instantiate(interactables[interactIndex], newPos, transform.rotation);
					ready = false;
				}
			
			//SPAWNING OF ITEM
				spawnRand = Random.Range(0f,100f);//Chance to spawn something
				if(spawnRand <= pickupSpawnChance){
					int pickupIndex = Random.Range(0, pickups.Length);
					
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
					
					GameObject newBuddy = (GameObject) Instantiate(pickups[pickupIndex], newPos, transform.rotation);
					newBuddy.transform.parent = interactPack;
					
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
		if(ready)
		{
			Debug.Log ("Hi");
		}
	}
}

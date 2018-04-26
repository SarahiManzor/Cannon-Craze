using UnityEngine;
using System.Collections;

public class InteractSpawn : MonoBehaviour
{
	public float spawnChance = 0f;
	
	public GameObject interactable;		// Array of enemy prefabs.
	
	public float spawnTime = 0.5f;
	
	private Transform interactPack;
	
	private Transform player;
	private Camera cam;
	
	private float camHorizontalExtend;
	private float camVerticalExtend;
	
	private bool ready = true;
	public bool groundInteract = true;
	private float lastPos = 0f;
	public float distanceToReady = 15f;
	public bool randomDirection = false;
	
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
				if(spawnRand <= spawnChance){//If enemies spawned and chance to spawn is 1
					// Instantiate a random enemy.S
					
					float minPosX = player.position.x + camHorizontalExtend*2;
					float randomPosX = Random.Range (minPosX,minPosX + camHorizontalExtend*2);
					
					float minPosY = player.position.y - camVerticalExtend*2;
					
					if(minPosY < 0.25f)
					{
						minPosY = 0.25f;
					}
					float randomPosY = Random.Range (minPosY,minPosY + camVerticalExtend*2);
					Vector3 newPos;
					
					if(groundInteract == false)
					{
						newPos = new Vector3(randomPosX ,randomPosY,0f);
					}
					else
					{
						newPos = new Vector3(randomPosX,-0.65f,player.position.z);
					}	
					
					GameObject newBuddy = (GameObject) Instantiate(interactable, newPos, transform.rotation);
					newBuddy.transform.parent = transform;
					
					if(randomDirection)
					{
						int dir = Random.Range(0,2);
						if(dir == 1)
						{
							newBuddy.transform.localScale = new Vector3(-1f,1f,1f);
						}
					}
					//Instantiate(interactables[interactIndex], newPos, transform.rotation);
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

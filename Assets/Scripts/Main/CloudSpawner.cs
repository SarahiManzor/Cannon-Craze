using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour 
{
	public float skySpawnChance = 3f;
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public float scale;
	public float minY = 8f;
	
	public GameObject skyThing;
	
	//private Transform cloudPack;
	private Transform player;
	private Camera cam;
	
	private float camHorizontalExtend;
	private float camVerticalExtend;
	
	public bool onScreenOnStart = false;
	
	void Awake () 
	{
		//cloudPack = GameObject.Find ("CloudPack").transform;
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
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}
	
	void SpawnFirstCouple()
	{
		int amountOfClouds = Random.Range (1,2);
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
	
	// Update is called once per frame
	void Spawn ()
	{
		if(player.position.y > minY || minY < 2f)
		{
			float spawnRand = Random.Range(0f,10f);//Chance to spawn something
			float skySpawnChanceZZ = (player.GetComponent<Rigidbody2D>().velocity.x + Mathf.Abs (player.GetComponent<Rigidbody2D>().velocity.y))*skySpawnChance;
	
			if(spawnRand <= skySpawnChanceZZ)
			{
				float minPosY = cam.transform.position.y - camVerticalExtend*3.5f;
				float maxPosY = cam.transform.position.y + camVerticalExtend*3.5f;
				
				float randomPosY = Random.Range (minPosY,maxPosY);
				
				if(randomPosY > minY && randomPosY < 160f)
				{
					//Calculating X
					float minPosX;
					if(randomPosY > cam.transform.position.y + camVerticalExtend*1.5f || randomPosY < cam.transform.position.y - camVerticalExtend*1.5f)
					{
						minPosX = cam.transform.position.x;
					}
					else
					{
						minPosX = cam.transform.position.x + camHorizontalExtend*1.5f;
					}
					float randomPosX = Random.Range (minPosX,minPosX + camHorizontalExtend*2f);
					
					//Instantiating
					Vector3 newPos = new Vector3(randomPosX,randomPosY - 1f,transform.position.z);
					
					GameObject newBuddy = (GameObject) Instantiate (skyThing, newPos, transform.rotation);
					newBuddy.transform.parent = gameObject.transform;
					newBuddy.transform.localScale = new Vector3(scale,scale,1f);
					newBuddy.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,scale*1.75f);
					newBuddy.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder;
				}
			}
		}
	}
}

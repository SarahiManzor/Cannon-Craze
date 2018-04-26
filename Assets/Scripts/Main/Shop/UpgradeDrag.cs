using UnityEngine;
using System.Collections;

public class UpgradeDrag : MonoBehaviour {

	public GameObject[] objectsToDisable;

	private Transform upgradePage;
	
	private bool touchValid = false;
	private bool moving = false;
	
	private float startPosX;
	private float timeForDragStop;
	
	public float sizeX = 0f;
	private float offsetX;
	
	private int itemNumber;
	
	private float minX = -8.5f;
	private float maxX = 4.2f;
	
	private bool checkStop;
	
	private Camera cam;
	
	private float zeScaleNumber;

	void Awake () 
	{
		upgradePage = GameObject.Find ("Upgrades1").transform;
		cam = Camera.main;
	}
	
	void Start()
	{
		float scale = (cam.orthographicSize * Screen.width/Screen.height)/cam.orthographicSize;
		scale = Mathf.Clamp(scale,0f,1.775f);
		offsetX = (1.9125f - scale) * 1.454545f;
		zeScaleNumber = (Screen.width/6.8f);
		minX = -8.5f;
		maxX = GameObject.Find ("Upgrades4").transform.position.x + 0.1f;
	}
	
	public void TouchDown(float posX)
	{
		touchValid = true;
		moving = true;
		
		startPosX = posX;
		
		for(int i = 0;i<objectsToDisable.Length;i++)
		{
			objectsToDisable[i].GetComponent<Collider2D>().enabled = false;
		}
	}
	
	public void TouchUp()
	{
		touchValid = false;
		moving = false;
		
		for(int i = 0;i<objectsToDisable.Length;i++)
		{
			objectsToDisable[i].GetComponent<Collider2D>().enabled = true;
		}
	}
	
	public void SetCurrent(Transform up)
	{
		upgradePage = up;
		minX = maxX - up.GetComponent<UpgradePageDetails>().itemsInCategory * 1.27f;
	}
	
	public void TouchStay (float posX) 
	{
		if(touchValid)
		{
			float touchX = posX;
			
			if(moving)
			{
				//lastPosX = posX;
				//Debug.Log(posX);
				float difference = touchX - startPosX;
				Debug.Log (Screen.width);
				float newX = upgradePage.position.x + difference/zeScaleNumber;
				
				//newX = Mathf.Clamp(newX,0f,-4.36f);
				//newX = Mathf.Clamp(newX,-4.36f,0f);
				newX = Mathf.Clamp(newX,minX,maxX);
				
				upgradePage.position = new Vector3(newX,upgradePage.position.y,upgradePage.position.z);
				startPosX = touchX;
			}
		}
	}
	
	void Update()
	{
		if(!moving)
		{
			float posX = upgradePage.position.x;
			itemNumber = Mathf.RoundToInt(upgradePage.position.x/sizeX);
			float targetPos = itemNumber * sizeX + offsetX;
			posX = Mathf.Lerp(posX,targetPos,Time.deltaTime*10f);
			
			upgradePage.position = new Vector3 (posX,upgradePage.position.y,upgradePage.position.z);
		}
		/*
		if(lastPosX == testPosX)
		{
			checkStop = true;
		}
		else
		{
			timeForDragStop = Time.realtimeSinceStartup;
			checkStop = false;
		}
		if(Time.realtimeSinceStartup - timeForDragStop > 0.5f && checkStop)
		{
			touchValid = false;
			moving = false;
		}
		*/
	}
	
	public float OffsetX
	{
		get{return offsetX;}
		set{offsetX = value;}
	}
	
	public float SizeX
	{
		get{return sizeX;}
		set{sizeX = value;}
	}
}

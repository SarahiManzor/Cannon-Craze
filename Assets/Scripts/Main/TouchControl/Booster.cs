using UnityEngine;
using System.Collections;

public class Booster : MonoBehaviour {

	private PlayerBehaviour playerBehave;
	private DataManager dm;
	
	private bool removed = false;
	
	void Awake () 
	{
		playerBehave = GameObject.Find ("Player").GetComponent<PlayerBehaviour>();
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
	}
	
	void Start()
	{
		if(dm.JetpackLevel == 0)
		{
			transform.position = new Vector3(transform.position.x,transform.position.y - 10f,transform.position.z);
		}
	}
	
	void OnTouchStay()
	{
		playerBehave.setBoost(true);
	}
	
	void OnTouchUp()
	{
		playerBehave.setBoost(false);
	}
	
	void OnTouchExit()
	{
		playerBehave.setBoost(false);
	}
	
	void Update()
	{
		if(playerBehave.getGameStatus() == false && !removed)
		{
			transform.position = new Vector3(transform.position.x,transform.position.y - 10f,transform.position.z);
			removed = true;
		}
	}
	
	/*void Update()
	{
		if(ready)
		{
			float newY = Mathf.Lerp (transform.position.y,cam.transform.position.y,Time.deltaTime);
			if(newY > -1f)
			{
				ready = false;
				newY = cam.transform.position.y;
			}
			transform.position = new Vector3(cam.transform.position.x,newY,0f);
		}
	}
	
	public void startBoost()
	{
		ready = true;
	}*/
}

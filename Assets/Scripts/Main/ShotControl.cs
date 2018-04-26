using UnityEngine;
using System.Collections;

public class ShotControl : MonoBehaviour {

	private float startTime;
	
	void Awake () 
	{
	
	}
	
	void Start()
	{
		startTime = Time.realtimeSinceStartup;
	}
	
	void Update()
	{
		if(Time.realtimeSinceStartup - startTime > 0.1f)
		{
			Destroy(gameObject);
			//gameObject.GetComponent<CircleCollider2D>().enabled = false;
		}
	}
	
}

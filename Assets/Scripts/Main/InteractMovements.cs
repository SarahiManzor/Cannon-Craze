using UnityEngine;
using System.Collections;

public class InteractMovements : MonoBehaviour {

	public float maxY = 0f;
	public float minY = 0f;
	
	public float xVel = 0f;
	public float yVel = 0f;
	
	public float rotation = 0f;
	
	public bool randomFacing = false;
	private bool moving = false;
	
	private Vector3 initialPos;
	
	// Update is called once per frame
	void Awake()
	{
		initialPos = transform.position;
	}
	
	void Start()
	{
		if(randomFacing)
		{
			xVel*=-1f;
			rotation*=-1f;
			transform.localScale = new Vector3(-1f,1f,1f);
		}
		if(rotation > 0f)
		{
			transform.GetComponent<Rigidbody2D>().angularVelocity = rotation;
		}
		if(xVel != 0f|| yVel != 0f)
		{
			moving=true;
		}
	}
	
	void Update () 
	{
		if(moving)
		{
			Vector3 difference = transform.position - initialPos;
			if(difference.y > maxY){
				yVel *= -1f;
				transform.position = new Vector3(transform.position.x,initialPos.y + maxY, transform.position.z);
			}
			else if(difference.y < minY){
				transform.position = new Vector3(transform.position.x,initialPos.y + minY, transform.position.z);
				yVel *= -1f;
			}
				
			transform.GetComponent<Rigidbody2D>().velocity = new Vector3(xVel, yVel, transform.position.z);	
		}
	}
}

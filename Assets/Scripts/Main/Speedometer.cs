using UnityEngine;
using System.Collections;

public class Speedometer : MonoBehaviour {

	private Transform player;
	private float startPos;
	
	void Awake () 
	{
		player = GameObject.Find ("Player").transform;
	}
	
	void Start()
	{
		startPos = 100f;
		transform.rotation = Quaternion.Euler (0f, 0f, startPos);
	}
	
	void Update () 
	{
		float zRotation = startPos - player.GetComponent<Rigidbody2D>().velocity.x*2.5f;
		zRotation = Mathf.Clamp (zRotation,-100f,100f);
		
		float targetZ = Mathf.LerpAngle (transform.rotation.eulerAngles.z,zRotation,Time.deltaTime*3f);
		transform.rotation = Quaternion.Euler (0f, 0f, targetZ);
	}
}

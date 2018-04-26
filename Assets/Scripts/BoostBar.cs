using UnityEngine;
using System.Collections;

public class BoostBar : MonoBehaviour {

	private PlayerBehaviour pb;
	private DataManager dm;
	
	void Awake () 
	{
		pb = GameObject.Find ("Player").GetComponent<PlayerBehaviour>();
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float newTargetX = pb.getFuel()/dm.Fuel;
		newTargetX = Mathf.Clamp01(newTargetX);
		float newX = Mathf.Lerp(transform.localScale.x,newTargetX,Time.deltaTime*20f);
		transform.localScale = new Vector3(newX,1f,1f);
	}
}

using UnityEngine;
using System.Collections;

public class JetpackUpgrade : MonoBehaviour {

	private DataManager dm;
	private int jetpackLevel;
	private int money;
	
	void Awake () 
	{
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
	}
	
	void OnTouchUp () 
	{
		jetpackLevel = (int)dm.JetpackLevel;
		money = dm.Money;
		
		int jetpackCost = ((jetpackLevel+1)*(jetpackLevel+1)*500);
		
		if(money >= jetpackCost){
			dm.JetpackLevel = jetpackLevel + 1;
			dm.Money  = money - jetpackCost;
		}
	}
}

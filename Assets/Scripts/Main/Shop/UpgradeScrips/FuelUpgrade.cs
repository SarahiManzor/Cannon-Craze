using UnityEngine;
using System.Collections;

public class FuelUpgrade : MonoBehaviour {

	private DataManager dm;
	private float fuelLevel;
	private int money;
	
	void Awake () 
	{
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
	}
	
	void OnTouchUp () 
	{
		fuelLevel = dm.Fuel;
		money = dm.Money;
		
		int fuelCost = (int)(fuelLevel/50*fuelLevel/50*500);
		
		if(money >= fuelCost){
			dm.Fuel = fuelLevel + 25;
			dm.Money = money - fuelCost;
		}
	}
}

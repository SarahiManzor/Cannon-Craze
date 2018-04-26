using UnityEngine;
using System.Collections;

public class BuyButton : MonoBehaviour 
{

	private ShopManager sm;
	void Awake () 
	{
		sm = GameObject.Find ("_SM").GetComponent<ShopManager>();
	}
	
	// Update is called once per frame
	void OnTouchUp() 
	{
		sm.Purchase();
	}
}

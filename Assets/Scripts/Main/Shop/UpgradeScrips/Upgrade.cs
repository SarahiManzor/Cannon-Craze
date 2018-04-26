using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour 
{
	private ShopManager sm;
	private ShopInfo si;
	private UpgradeDrag ug;
	
	public int category;
	public int itemNumber;
	
	private BoxCollider2D boxCol;
	private Vector2 initialSize;
	
	private float touchDownTime;
	private GameObject highlight;
	
	void Awake ()
	{
		sm = GameObject.Find ("_SM").GetComponent<ShopManager>();
		si = GameObject.Find ("_SI").GetComponent<ShopInfo>();
		ug = GameObject.Find ("UpgradeDrag").GetComponent<UpgradeDrag>();
		boxCol = gameObject.GetComponent<BoxCollider2D>();
		highlight = GameObject.Find("Highlighter");
	}
	
	void Start()
	{
		initialSize = boxCol.size;
		
		if(category != 3)
		{
			if(si.getEquipped(category,itemNumber))
			{
				if(category==0)
				{
					sm.SetCurrent(category,itemNumber);
				}
				sm.UpdateEquips(category,itemNumber);
				float newX = transform.parent.transform.position.x - itemNumber * ug.SizeX;
				transform.parent.transform.position = new Vector3(newX,transform.parent.transform.position.y,transform.parent.transform.position.z);
			}
		}
	}
	
	void OnTouchUp ()
	{
		if(Time.realtimeSinceStartup - touchDownTime < 0.15f)
		{
			sm.SetCurrent(category,itemNumber);
			highlight.transform.position = transform.position;
			highlight.transform.parent = transform.parent;
		}
		ug.TouchUp ();
		boxCol.size = initialSize;
	}
	
	void OnTouchDown (Vector2 pos) 
	{
		touchDownTime = Time.realtimeSinceStartup;
		ug.TouchDown (pos.x);
		boxCol.size = new Vector2(10f,10f);
	}
	
	void OnTouchStay (Vector2 pos) 
	{
		ug.TouchStay (pos.x);
	}
	
	public bool IsBought()
	{
		return si.getPurchased(category,itemNumber);
	}
	
	public bool IsEquipped()
	{
		return si.getEquipped(category,itemNumber);
	}
}

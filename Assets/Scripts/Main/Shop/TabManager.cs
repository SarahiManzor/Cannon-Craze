using UnityEngine;
using System.Collections;

public class TabManager : MonoBehaviour {

	public GameObject [] tabs;
	private GameObject [] upgradePages;
	
	private UpgradeDrag ud;
	private ShopManager sm;
	
	private float chooseTime;
	private bool chosen;
	
	private Transform currentTab;
	
	void Awake () 
	{
		upgradePages = GameObject.FindGameObjectsWithTag("UpgradePage");
		ud = GameObject.Find("UpgradeDrag").GetComponent<UpgradeDrag>();
		sm = GameObject.Find ("_SM").GetComponent<ShopManager>();
	}
	
	void Start()
	{
		currentTab = GameObject.Find ("Tab1").transform;
	}
	
	public void Organise(Transform tab)
	{
		if(tab!=currentTab)
		{
			currentTab = tab;
			for(int i = 0; i < tabs.Length; i++)
			{
				tabs[i].transform.position = new Vector3(tabs[i].transform.position.x,tabs[i].transform.position.y,-1f);
			}
			currentTab.position = new Vector3(currentTab.position.x,currentTab.position.y,0f);
			chooseTime = Time.realtimeSinceStartup;
			chosen = false;
			sm.SetCurrentStart(tab.GetComponent<TabCategory>().TabCat);
			sm.Highlight();
		}
	}
	
	void Update()
	{
		if(Time.realtimeSinceStartup - chooseTime < 0.75f)
		{
			for(int i = 0; i < upgradePages.Length; i++)
			{
				Vector3 pos = upgradePages[i].transform.position;
				float currentY = pos.y;
				float targetY = (pos.z*3f) + 0.5f;
				currentY = Mathf.Lerp(currentY,targetY,Time.deltaTime*5f);
				upgradePages[i].transform.position = new Vector3(pos.x,currentY,upgradePages[i].transform.position.z);
			}
		}
		else if (!chosen)
		{
			for(int i = 0; i < upgradePages.Length; i++)
			{
				if(upgradePages[i].transform.position.y > -1f)
				{
					ud.SetCurrent(upgradePages[i].transform);
					chosen = true;
				}
			}
		}
	}
}

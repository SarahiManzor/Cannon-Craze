using UnityEngine;
using System.Collections;

public class TabControl : MonoBehaviour {

	private TabManager tm;

	void Awake () 
	{
		tm = transform.root.gameObject.GetComponent<TabManager>();
	}
	
	void OnTouchUp()
	{
		tm.Organise(transform.parent.gameObject.transform);
	}
}

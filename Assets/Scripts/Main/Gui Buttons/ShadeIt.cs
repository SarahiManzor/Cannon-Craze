using UnityEngine;
using System.Collections;

public class ShadeIt : MonoBehaviour 
{
	private Color currentColor;
	public GameObject[] children;
	
	void Awake () 
	{
		currentColor = transform.GetComponent<SpriteRenderer>().color;
	}
	
	void OnTouchDown()
	{
		transform.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
		foreach(GameObject child in children)
		{
			child.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
		}
	}
	
	void OnTouchStay()
	{
		OnTouchDown();
	}
	
	void OnTouchUp()
	{
		transform.GetComponent<SpriteRenderer>().color = currentColor;
		foreach(GameObject child in children)
		{
			child.GetComponent<SpriteRenderer>().color = currentColor;
		}
	}
	
	void OnTouchExit()
	{
		OnTouchUp ();
	}
}

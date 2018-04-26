using UnityEngine;
using System.Collections;

public class ButtonTouch : MonoBehaviour 
{
	private Color textColor;
	private Color boxColor;
	public bool hasParent = true;
	
	void Awake () 
	{
		textColor = transform.GetComponent<TextMesh>().color;
		if(hasParent)
		{
			boxColor = transform.parent.GetComponent<SpriteRenderer>().color;
		}
	}
	
	void OnTouchDown()
	{
		transform.GetComponent<TextMesh>().color = new Color(0.75f,0.75f,0.75f);
		if(hasParent)
		{
			transform.parent.GetComponent<SpriteRenderer>().color = new Color(0.75f,0.75f,0.75f);
		}
	}
	
	void OnTouchStay()
	{
		OnTouchDown();
	}
	
	void OnTouchUp()
	{
		transform.GetComponent<TextMesh>().color = textColor;
		if(hasParent)
		{
			transform.parent.GetComponent<SpriteRenderer>().color = boxColor;
		}
	}
	
	void OnTouchExit()
	{
		OnTouchUp ();
	}
}

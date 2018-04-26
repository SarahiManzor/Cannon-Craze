using UnityEngine;
using System.Collections;

public class Opacity : MonoBehaviour {

	public float opacity = 1f;
	// Use this for initialization
	void Awake ()
	{
		transform.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,opacity);
	}
}

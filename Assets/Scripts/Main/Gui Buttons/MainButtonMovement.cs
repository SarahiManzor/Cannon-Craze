using UnityEngine;
using System.Collections;

public class MainButtonMovement : MonoBehaviour {

	public GameObject nextItemToSee;
	void OnTouchUp()
	{
		Vector3 nextPos = nextItemToSee.transform.position;
		Camera.main.transform.position = new Vector3(nextPos.x,nextPos.y,-10f);
	}
}

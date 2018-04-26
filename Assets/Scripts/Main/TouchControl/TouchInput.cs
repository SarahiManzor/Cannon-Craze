using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {
	
	public LayerMask touchInputMask;
	
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	
	// Update is called once per frame
	void Update () {
	
#if UNITY_EDITOR || UNITY_WEBPLAYER
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown (0) || Input.GetMouseButtonUp(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider!=null)
			{
				touchesOld = new GameObject[touchList.Count];
				touchList.CopyTo (touchesOld);
				touchList.Clear();
				
				GameObject recepient = hit.transform.gameObject;
				touchList.Add (recepient);
				
				Vector2 mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
					
				if(Input.GetMouseButtonDown (0)){
					recepient.SendMessage("OnTouchDown",mousePos,SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButtonUp(0)){
					recepient.SendMessage("OnTouchUp",mousePos,SendMessageOptions.DontRequireReceiver);
				}
				if(Input.GetMouseButton(0)){
					recepient.SendMessage("OnTouchStay",mousePos,SendMessageOptions.DontRequireReceiver);
				}
			}
			
			if(touchesOld!=null)
			{
				foreach (GameObject g in touchesOld)
				{
					if(g!=null)
					{
						if(!touchList.Contains(g))
						{
							g.SendMessage("OnTouchExit",SendMessageOptions.DontRequireReceiver);
						}
					}
				}
			}
		}	
#endif
		if(Input.touchCount > 0)
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear();
			foreach (Touch touch in Input.touches)
			{
				RaycastHit2D hit = Physics2D.Raycast(GetComponent<Camera>().ScreenToWorldPoint(touch.position), Vector2.zero);
				
				if(hit.collider!=null)
				{
				
					GameObject recepient = hit.transform.gameObject;
					touchList.Add (recepient);
				
					if(touch.phase == TouchPhase.Began){
						recepient.SendMessage("OnTouchDown",touch.position,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Ended){
						recepient.SendMessage("OnTouchUp",touch.position,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved){
						recepient.SendMessage("OnTouchStay",touch.position,SendMessageOptions.DontRequireReceiver);
					}
					if(touch.phase == TouchPhase.Canceled){
						recepient.SendMessage("OnTouchExit",touch.position,SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			foreach (GameObject g in touchesOld)
			{
				if(!touchList.Contains(g))
				{
					g.SendMessage("OnTouchExit",SendMessageOptions.DontRequireReceiver);
				}
			}
		}	
	}
}

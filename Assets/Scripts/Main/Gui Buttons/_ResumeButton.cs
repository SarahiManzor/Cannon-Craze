using UnityEngine;
using System.Collections;

public class _ResumeButton : MonoBehaviour {

	private _PauseButton pb;
	
	void Awake () 
	{
	 	pb = GameObject.Find ("_PauseButton").GetComponent<_PauseButton>();
	}
	
	void OnTouchUp () 
	{
		pb.Resume();
	}
}

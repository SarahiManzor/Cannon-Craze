using UnityEngine;
using System.Collections;

public class _ResetButton : MonoBehaviour 
{
	void OnTouchUp()
	{
		Time.timeScale = 1f;
		Application.LoadLevel ("main");
	}
}

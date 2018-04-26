using UnityEngine;
using System.Collections;

public class MainStart : MonoBehaviour 
{
	void OnTouchUp()
	{
		Vector3 loadingPos = GameObject.Find ("loading").transform.position;
		Camera.main.transform.position = new Vector3(loadingPos.x,loadingPos.y,-10f);
		
		Time.timeScale = 1f;
		Application.LoadLevel ("main");
	}
}

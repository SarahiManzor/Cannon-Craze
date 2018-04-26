using UnityEngine;
using System.Collections;

public class _PauseButton : MonoBehaviour {

	private Camera cam;
	private bool paused = false;
	
	private GameManager gm;
	public GameObject pauseMenu;
	
	void Awake() 
	{
		gm = GameObject.Find ("_GM").GetComponent<GameManager>();
		cam = Camera.main;
	}
	
	void Start () 
	{
		//float initialPosX = cam.orthographicSize * Screen.width/Screen.height - boxCol.size.x/2;
		//float initialPosY = cam.orthographicSize - boxCol.size.y/2;
		
		//transform.position = new Vector3(initialPosX + cam.transform.position.x,initialPosY + cam.transform.position.y,transform.position.z);
	}
	
	void OnTouchUp(){
		/*if(Time.realtimeSinceStartup - startTime > 0.25f && !paused)
		{*/
		if(!paused)
		{
			Vector3 newPos = new Vector3(cam.transform.position.x,cam.transform.position.y,-2f);
			Instantiate (pauseMenu,newPos,cam.transform.rotation);
			Time.timeScale = 0f;
			paused = true;
		}
		/*}	
		else if(Time.realtimeSinceStartup - startTime > 0.25f && paused)
		{
			startTime = Time.realtimeSinceStartup;
			GameObject [] des = GameObject.FindGameObjectsWithTag("PauseMenu");
			Destroy(des[0]);
			Time.timeScale = 1f;
			paused = false;
		}*/
	}
	
	public void Resume()
	{
		GameObject des = GameObject.FindGameObjectWithTag("PauseMenu");
		Destroy(des);
		Time.timeScale = 1f;
		paused = false;
	}
	
	void Update()
	{
		if(gm.getDeath())
		{
			transform.position = new Vector3(-2f,-2f,0f);
		}
	}
}

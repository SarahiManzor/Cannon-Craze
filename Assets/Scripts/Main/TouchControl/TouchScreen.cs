using UnityEngine;
using System.Collections;

public class TouchScreen : MonoBehaviour {

	private CannonControl cannonCont;
	private PlayerBehaviour pb;
	
	public GameObject bullet;
	private Camera cam;
	
	private AudioSource shotSound;

	void Start () 
	{
		cannonCont = GameObject.Find ("Cannon").GetComponent<CannonControl>();
		pb = GameObject.Find ("Player").GetComponent<PlayerBehaviour>();
		cam = Camera.main;
		shotSound = GameObject.Find("ShotSound").GetComponent<AudioSource>();
	}
	
	void OnTouchStay()
	{
		if(pb.transform.position.z < -2)
		{
			cannonCont.setCannon(Input.mousePosition);
		}
	}
	
	void OnTouchDown(Vector2 position)
	{
		if(pb.getGameStatus() && Time.timeScale == 1f)
		{
			if(pb.transform.position.z > -2f && pb.getAmmo() > 0)
			{
				Vector3 newPos = cam.ScreenToWorldPoint(new Vector3(position.x,position.y,1f));
				GameObject newKid = (GameObject) Instantiate(bullet, newPos, transform.rotation);
				newKid.transform.parent = cam.transform;
				pb.BulletUsed();
				shotSound.GetComponent<AudioSource>().Play();
			}	
		}
	}
}

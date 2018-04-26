using UnityEngine;
using System.Collections;

public class CannonControl : MonoBehaviour
{
	private bool launched = false;

	private float power = 2;
	private float powerChange = 0f;
	
	private float powerLevel;
	
	private SpriteRenderer powerBar;
	private SpriteRenderer cannonSprite;
	private SpriteRenderer baseSprite;
	
	public Sprite [] cannons;
	public Sprite [] canBase;
	
	private Transform launchPos;
	private Transform player;
	private Vector3 difference;
	private Vector3 differenceCan;
	
	private DataManager dm;
	private float startVel;
	
	//private Booster booster;
	
	void Awake()
	{
		launchPos = GameObject.Find ("LaunchPosition").transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		powerBar = GameObject.Find("Power").GetComponent <SpriteRenderer>();
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
		
		cannonSprite = GetComponent<SpriteRenderer>();
		baseSprite = GameObject.Find ("CannonPack").GetComponent<SpriteRenderer>();	
		//booster = GameObject.Find ("Booster").GetComponent<Booster>();
	}
	
	void Start()
	{
		powerLevel = dm.PowerLevel;
		
		for(int i = 0;i<powerLevel-1;i++)
		{
			if(i < cannons.Length)
				if(cannons[i] != null)
					cannonSprite.sprite = cannons[i];
			
			if(i < canBase.Length)
				if(canBase[i] != null)
					baseSprite.sprite = canBase[i];
		}
	}
	
	void OnTouchDown()
	{
		if(!launched&&Time.timeScale == 1f)
		{
			GetComponent<AudioSource>().Play();
			launched=true;
			player.GetComponent<Rigidbody2D>().gravityScale = dm.Gravity;
			differenceCan = (launchPos.transform.position - transform.position);
			player.transform.position = launchPos.position;
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(differenceCan.x*power/100f*powerLevel*dm.GameSpeed,differenceCan.y*power/100f*powerLevel*dm.GameSpeed,0f);
			player.GetComponent<Rigidbody2D>().angularVelocity =-3.6f*player.GetComponent<Rigidbody2D>().velocity.x;
		}
		//booster.startBoost();
	}
	
	void Update ()
	{
		if (launched == false)
		{
			if(power>=100)
			{
				powerChange = -1.00f;
			}
			else if (power <= 2)
			{
				power = 2;
				powerChange = 1.00f;
			}
			
			if(powerChange == 1f)
			{
				power *= 1f+Time.deltaTime*6f;
			}
			else if (powerChange == -1f)
			{
				power *= 1f-Time.deltaTime*6f;
			}
			power = Mathf.Clamp(power,0f,100f);
			updatePowerBar();
			
			difference.Normalize ();
			float zRotation = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
			
			//if (zRotation < 0)
			//	zRotation = 0;
			//else if (zRotation > 75)
			//	zRotation = 75;
			
			zRotation = Mathf.Clamp (zRotation,0f,75f);
			
			//float targetZ = zRotation;
			float targetZ = Mathf.LerpAngle(transform.eulerAngles.z,zRotation,Time.deltaTime*10f);
			transform.rotation = Quaternion.Euler (0f, 0f, targetZ);
		}
	}
	
	public void setCannon(Vector3 pos)
	{
		difference = Camera.main.ScreenToWorldPoint (pos) - transform.position;
	}
		
	void updatePowerBar()
	{
		powerBar.material.color = Color.Lerp(Color.green, Color.red, 1 - power * 0.01f);
		
		powerBar.transform.localScale = new Vector3(power * 0.01f, 1, 1);
	}
}

using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	private float gameSpeed;
	private float bounciness;
	private float friction;
	private float braking;
	private int moneyBagMoney = 0;
	
	private bool playing = true;
	private bool jetPack = false;
	private float fuel;
	private float jetpackPower;
	
	private int ammo;
	private float gunPower;
	
	private float posCol = -0.535f;
	
	private float velX;
	private float velY;
	
	private DataManager dm;
	private GameManager gm;
	
	private Transform flame;
	
	public float enemyFactor = 2f;//Factor all interact boosts multiple by
	
	private new Collider2D collider;
	private bool hitInteract = false;
	private bool hitPickup = false;
	private bool hitGround = false;
	private bool shot = false;
	
	private AudioSource shotSound;
	private float totalBoost;
	
	private HighScoreSign highScoreSign;
	
	private bool hasBounced = false;
	private bool hasHit = false;
	
	void Awake()
	{
		flame = GameObject.Find ("Flame").transform;
		dm = GameObject.Find ("_DM").transform.root.GetComponent<DataManager>();
		gm = GameObject.Find ("_GM").transform.root.GetComponent<GameManager>();
		shotSound = GameObject.Find("ShotSound").GetComponent<AudioSource>();
		highScoreSign = GameObject.Find ("HighScore").GetComponent<HighScoreSign>();
	}
	
	void Start()
	{
		fuel = dm.Fuel;
		jetpackPower = dm.JetpackLevel;
		ammo = dm.Ammo;
		gunPower = dm.GunPower;
		bounciness = dm.Bounciness;
		friction = dm.Friction;
		braking = dm.Braking;
		gameSpeed = dm.GameSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Interactable" && playing)
		{
			collider = col;
			hitInteract = true;
			col.enabled = false;
		}
		else if (col.gameObject.tag == "Pickup" && playing)
		{
			collider = col;
			hitPickup = true;
			col.enabled = false;
		}
		else if (col.gameObject.tag == "Foreground" && playing && hitInteract == false)
		{
			hitGround = true;
		}
		if(col.gameObject.tag == "ShotCollider" && playing)
		{
			shot = true;
		}
	}
	
	void HitInteract(Collider2D col)
	{
		//velY = rigidbody2D.velocity.y;
		//velX = rigidbody2D.velocity.x;
		
		if (velY<0)
		{
			velY*=-1;
		}
		
		if(col.gameObject.name.Contains ("ghoul"))
		{
			//rigidbody2D.AddForce (new Vector2 (1f*enemyFactor,4f*enemyFactor));
			velX+=0.1f*enemyFactor*gameSpeed;
			velY+=0.4f*enemyFactor*gameSpeed;
		}
		
		if(col.gameObject.name.Contains ("ryu"))
		{
			//rigidbody2D.AddForce (new Vector2 (2f*enemyFactor,6f*enemyFactor));
			velX+=0.2f*enemyFactor*gameSpeed;
			velY+=0.6f*enemyFactor*gameSpeed;
		}	
		
		if(col.gameObject.name.Contains ("bird"))
		{
			//rigidbody2D.AddForce (new Vector2 (4f*enemyFactor,2f*enemyFactor));
			velX+=0.4f*enemyFactor*gameSpeed;
			velY+=0.2f*enemyFactor*gameSpeed;
		}	
		
		GetComponent<Rigidbody2D>().gravityScale = dm.Gravity;
		GetComponent<Rigidbody2D>().velocity = new Vector3(velX, velY, 0f);	
		Animator anim = col.GetComponent<Animator>();
		if(anim != null)
		{
			anim.SetTrigger("Contact");
		}
		if(col.GetComponent<AudioSource>() != null)
		{
			col.GetComponent<AudioSource>().Play();
		}
		
		UpdateRotation();
		
		if(col.gameObject.name.Contains ("spikes"))
		{
			if(hasHit == false && dm.Shield == true)
			{
				hasHit = true;
			}
			else
			{
				transform.parent = col.transform;
				GameOver();
				GetComponent<AudioSource>().Play();
			}
		}
	}
	
	void HitPickup(Collider2D col)
	{
		col.GetComponent<AudioSource>().Play ();
		if(col.gameObject.name.Contains ("fuel"))
		{
			fuel += dm.FuelPickup;
			if(fuel >= dm.Fuel)
			{
				fuel = dm.Fuel;
			}
		}
		if(col.gameObject.name.Contains ("money"))
		{
			moneyBagMoney += dm.CoinPickup;
		}
		if(col.gameObject.name.Contains ("ammo"))
		{
			ammo += dm.AmmoPickup;
		}
		col.transform.position = new Vector3(-2f,-2f,0f);
	}
	
	void HitGround()
	{
		if(hasBounced == false && dm.NoSlowOnFirst == true)
		{
			velX *= 1f;
			velY *= -1f;
			hasBounced = true;
		}
		else
		{
			velX *= friction;
			velY *= -bounciness;
		}
		GetComponent<AudioSource>().pitch = Random.Range(1.0f, 1.4f);
		transform.position = new Vector3(transform.position.x,posCol,transform.position.z);
		
		if(Mathf.Abs(velY) <= 0.6f)
		{
			velY = 0f;
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}
		else 
		{
			GetComponent<AudioSource>().Play();
		}
		
		UpdateRotation();
		
		GetComponent<Rigidbody2D>().velocity = new Vector3(velX, velY, 0f);	
		
	}
	
	void UpdateRotation()
	{
		if(GetComponent<Rigidbody2D>().angularVelocity >= velX*-180)
			GetComponent<Rigidbody2D>().angularVelocity -= velX*36f;
		else
			GetComponent<Rigidbody2D>().angularVelocity = velX*-180;
	}
	
	void OnTouchDown()
	{
		if(ammo > 0&&playing)
		{
			shot = true;
			ammo--;
			shotSound.GetComponent<AudioSource>().Play();
		}
	}
	
	void HitWithTouch()
	{
		if(ammo >= 0)
		{
			if(velY < 0)
			{
				velY *= -1;
				velY += gunPower/12f*gameSpeed;
			}
			else
			{
				velY += gunPower/6f*gameSpeed;
			}
			
			velX += gunPower/20f*gameSpeed;
			GetComponent<Rigidbody2D>().velocity = new Vector3(velX, velY, 0f);	
		}
	}
	
	public void BulletUsed()
	{
		ammo--;
	}
	
	void Update () 
	{
		if(playing)
		{
			velY = GetComponent<Rigidbody2D>().velocity.y;
			velX = GetComponent<Rigidbody2D>().velocity.x;
			
			//If player goes out under map
			if(transform.position.y <= posCol)
			{
				transform.position = new Vector3(transform.position.x,posCol,transform.position.z);
			}
			
			if(velY == 0f&&jetPack==false)
			{
				velX -= velX*(braking*0.1f)*Time.deltaTime;
				
				if(velX*-360f >= -1000f)
					GetComponent<Rigidbody2D>().angularVelocity = velX*-360f;
				else
					GetComponent<Rigidbody2D>().angularVelocity = -1000f;
				
				if(velX<=0.2f && transform.position.x > -2f && transform.position.y <= posCol)
				{
					GameOver();
				}
			}
			else if(jetPack == true && playing)
			{	
				float change = (jetpackPower/250f) * Time.deltaTime*gameSpeed * 0.8f;
				float direction = (Mathf.Atan2 (velY,velX)* Mathf.Rad2Deg);
				float forceX = 0f;
				float forceY = 0f;
				
				flame.position = transform.position;
				flame.rotation = Quaternion.Euler (0f, 0f, direction+90);
				
				if(direction >=0)
				{
					forceX = (90-direction)*change;
					forceY = direction*change;
				}
				else if (direction < 0)
				{
					forceX = (90+direction)*change;
					forceY = direction*change;
				}
				velX += forceX;
				velY += forceY;
				fuel -= 50f*Time.deltaTime * 0.8f;
				totalBoost+=change;
			}
			else
			{
				GetComponent<Rigidbody2D>().gravityScale = dm.Gravity;
			}
			
			if(fuel < 0f)
			{
				flame.position = new Vector3(-2f,-2f,0f);
				fuel = 0f;
			}
			if(hitInteract)
			{
				HitInteract(collider);
				hitInteract = false;
				hitGround = false;
			}
			else if(hitGround)
			{
				HitGround();
				hitGround = false;
			}
			if(hitPickup)
			{
				HitPickup(collider);
				hitPickup = false;
			}
			if(shot)
			{
				HitWithTouch();
				GetComponent<Rigidbody2D>().gravityScale = dm.Gravity;
				shot = false;
			}
			
			GetComponent<Rigidbody2D>().velocity = new Vector3(velX, velY, 0f);
			
			if(fuel <= 0)
			{
				jetPack = false;
			}
		}
	}
	
	void GameOver()
	{
		velX = 0f;
		velY = 0f;
		GetComponent<Rigidbody2D>().angularVelocity  = 0f;
		GetComponent<Rigidbody2D>().gravityScale  = 0f;
		playing = false;
		gm.SetHigh();
		highScoreSign.CheckMove();
		this.enabled = false;
	}
	
	public void setBoost(bool b)
	{
		if(transform.position.z >-1f)
		{
			if(b == true)
			{
				if(jetpackPower > 0f && fuel > 0f)
				{
						jetPack = b;
				}
				else
				{
					flame.position = new Vector3(-2f,-2f,0f);
					jetPack = false;
				}
			}
			else if (b == false)
			{
				flame.position = new Vector3(-2f,-2f,0f);
				jetPack = false;
			}
		}
	}
	
	public bool getGameStatus(){
		return playing;
	}
	
	public float getFuel(){
		return fuel;
	}
	
	public int getAmmo()
	{
		return ammo;
	}
	
	public int getMoneyBagMoney()
	{
		return moneyBagMoney;
	}
}

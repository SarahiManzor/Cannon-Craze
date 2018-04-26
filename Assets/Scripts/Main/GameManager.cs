using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	private float distance;
	private float height;
	private int highScore;
	private float fuel;
	private int ammo;
	private bool death;
	private float preMoney;
	
	public GameObject deathMenu;
	
	private Transform player;
	private PlayerBehaviour playerBehave;
	private DataManager dm;
	
	private Transform cannon;
	private Camera cam;
	
	private TextMesh ammoText;
	private TextMesh distanceText;
	private TextMesh highScoreText;
	private TextMesh heightText;
	private TextMesh moneyText;
	
	private float menuTimer;
	private bool menuStart;
	
	void Awake () 
	{
		player = GameObject.Find("Player").transform;
		playerBehave = GameObject.Find ("Player").GetComponent<PlayerBehaviour>();
		
		dm = GameObject.Find ("_DM").transform.root.GetComponent<DataManager>();
		
		cannon = GameObject.Find ("LaunchPosition").transform;
		
		cam = Camera.main;
		
		ammoText = GameObject.Find ("Ammo").GetComponent<TextMesh>();
		distanceText = GameObject.Find ("Distance").GetComponent<TextMesh>();
		highScoreText = GameObject.Find ("HighScore1").GetComponent<TextMesh>();
		heightText = GameObject.Find ("Height").GetComponent<TextMesh>();
	}
	
	void Start()
	{
		highScore = dm.HighScore;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	 {
			distance = (player.position.x - cannon.transform.position.x)*10;
			height = (player.position.y + 0.5f)*3f;
			if(height<0)
				height = 0;
			if(distance < 0)
				distance = 0;
				
			height = (int)(height*10f) / 10f;
			
			if(distance > highScore)
				highScore = (int) distance;
			
			if(playerBehave.getGameStatus () == false && death == false)
			{
				Vector3 newPos = new Vector3(cam.transform.position.x,cam.transform.position.y,-2f);
				Instantiate(deathMenu,newPos,cam.transform.rotation);
				preMoney = dm.Money;
				dm.Money += (int)((distance) * dm.MoneyMultiplier + playerBehave.getMoneyBagMoney());
				death = true;
				moneyText = GameObject.Find("_Money").GetComponent<TextMesh>();
				moneyText.text = " " + preMoney;
				menuTimer = Time.timeSinceLevelLoad;
				dm.Save();
			}	
	}
	
	void Update()
	{
		highScoreText.text = ("HighScore: " + highScore);
		distanceText.text = ("Distance: " + (int) distance);
		heightText.text = ("Height: " + height);
		ammoText.text = ("x" + playerBehave.getAmmo ());
		
		if(death&&Time.timeSinceLevelLoad-menuTimer > 1f&&menuStart == false)
		{
			GetComponent<AudioSource>().Play();
			menuStart = true;
		}
		if(menuStart)
		{
			int num = (int)preMoney;
			preMoney += Mathf.RoundToInt((dm.Money - preMoney)/50);
			moneyText.text = " " + preMoney;
			if(num == preMoney)
			{
				moneyText.text = " " + dm.Money;
				GetComponent<AudioSource>().Stop();
			}
		}
	}
	
	public void SetHigh()
	{
		dm.HighScore = highScore;
	}
	
	public bool getDeath()
	{
		return death;
	}
	
	public void shop()
	{
		Application.LoadLevel ("shop");
	}
	
	public void retry()
	{
		Time.timeScale = 1f;
		Application.LoadLevel("main");
	}
}
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataManager : MonoBehaviour
{
	private int money = 0;
	public float gravity = 1f;
	public float gameSpeed = 1f;
	private float highScorePos = -4f;
	private int highScore = 0;
	
	private float jetpackLevel = 10f;
	private float powerLevel = 12f;
	private float gunPower = 10f;
	private int ammo = 3;
	private float braking = 10f;
	
	//Perk Related
	private float bounciness = 0.75f;
	private float friction = 0.75f;
	private float moneyMultiplier = 1f;
	private float fuel = 100f;
	private float fuelPickup = 25f;
	private int ammoPickup = 2;
	private int coinPickup = 1000;
	private bool shield = false;
	private bool noSlowOnFirst = false;

	// Use this for initialization
	void Awake () 
	{
		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			GetComponent<AudioSource>().Play();
			Application.targetFrameRate = 60;
			//Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
			//float yRes = (Camera.main.orthographicSize * Screen.width/Screen.height)/Camera.main.orthographicSize;
			//Screen.SetResolution((int)(320*yRes),320,true);
			//Screen.SetResolution((int)(320*yRes),320,true);
			Load();
		}
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
		
		PlayerData data = new PlayerData();
		data.Money = money;
		data.HighScorePos = highScorePos;
		data.HighScore = highScore;
		data.Fuel = fuel;
		data.JetpackLevel = jetpackLevel;
		data.PowerLevel = jetpackLevel;
		data.PowerLevel = powerLevel;
		data.GunPower = gunPower;
		data.Ammo = ammo;
		data.Bounciness = bounciness;
		data.Friction = friction;
		data.Braking = braking;
		data.MoneyMultiplier = moneyMultiplier;
		data.AmmoPickup = ammoPickup;
		data.FuelPickup = fuelPickup;
		data.CoinPickup = coinPickup;
		data.NoSlowOnFirst = noSlowOnFirst;
		data.Shield = shield;
		
		bf.Serialize(file,data);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);
			
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
			
			money = data.Money;
			highScorePos = data.HighScorePos;
			highScore = data.HighScore;
			fuel = data.Fuel;
			jetpackLevel = data.JetpackLevel;
			jetpackLevel = data.PowerLevel;
			powerLevel = data.PowerLevel;
			gunPower = data.GunPower;
			ammo = data.Ammo;
			bounciness = data.Bounciness;
			friction = data.Friction;
			braking = data.Braking;
			moneyMultiplier = data.MoneyMultiplier;
		}
	}
	
	public void playMusic(){
		GetComponent<AudioSource>().Play();
	}
	
	public void stopMusic(){
		GetComponent<AudioSource>().Stop();
	}
	
	public float Gravity
	{
		get { return gravity; }
	}
	
	public int Money
	{
		get { return money; }
		set { money = value; }
	}
	
	public int HighScore
	{
		get { return highScore; }
		set { highScore = value; }
	}
	
	public float PowerLevel
	{
		get { return powerLevel; }
		set { powerLevel = value; }
	}
	
	public float Fuel
	{
		get { return fuel; }
		set { fuel = value; }
	}
	
	public float JetpackLevel
	{
		get { return jetpackLevel; }
		set { jetpackLevel = value; }
	}
	
	public int Ammo
	{
		get { return ammo; }
		set { ammo = value; }
	}
	
	public float GunPower
	{
		get { return gunPower; }
		set { gunPower = value; }
	}
	
	public float HighScorePos
	{
		get { return highScorePos; }
		set { highScorePos = value; }
	}
	
	public float Bounciness
	{
		get { return bounciness; }
		set { bounciness = value; }
	}
	
	public float Friction
	{
		get { return friction; }
		set { friction = value; }
	}
	
	public float Braking
	{
		get { return braking; }
		set { braking = value; }
	}
	
	public float MoneyMultiplier
	{
		get { return moneyMultiplier; }
		set { moneyMultiplier = value; }
	}
	
	
	public float FuelPickup
	{
		get { return fuelPickup; }
		set { fuelPickup = value; }
	}
	
	public int AmmoPickup
	{
		get { return ammoPickup; }
		set { ammoPickup = value; }
	}
	
	public int CoinPickup
	{
		get { return coinPickup; }
		set { coinPickup = value; }
	}
	
	public bool Shield
	{
		get { return shield; }
		set { shield = value; }
	}
	
	public bool NoSlowOnFirst
	{
		get { return noSlowOnFirst; }
		set { noSlowOnFirst = value; }
	}
	
	public float GameSpeed
	{
		get { return gameSpeed; }
		set { gameSpeed = value; }
	}
}

[Serializable]
class PlayerData
{
	private int money;
	private float gravity;
	private float highScorePos;
	private int highScore;
	
	private float jetpackLevel;
	private float powerLevel;
	private float gunPower;
	private int ammo;
	
	private float braking;
	private float bounciness;
	private float friction;
	private float moneyMultiplier;
	private float fuel;
	private float fuelPickup ;
	private int ammoPickup;
	private int coinPickup;
	private bool shield;
	private bool noSlowOnFirst;
	
	public int Money
	{
		get { return money; }
		set { money = value; }
	}
	
	public int HighScore
	{
		get { return highScore; }
		set { highScore = value; }
	}
	
	public float PowerLevel
	{
		get { return powerLevel; }
		set { powerLevel = value; }
	}
	
	public float Gravity
	{
		get { return gravity; }
		set { gravity = value; }
	}
	
	public float Fuel
	{
		get { return fuel; }
		set { fuel = value; }
	}
	
	public float JetpackLevel
	{
		get { return jetpackLevel; }
		set { jetpackLevel = value; }
	}
	
	public int Ammo
	{
		get { return ammo; }
		set { ammo = value; }
	}
	
	public float GunPower
	{
		get { return gunPower; }
		set { gunPower = value; }
	}
	
	public float HighScorePos
	{
		get { return highScorePos; }
		set { highScorePos = value; }
	}
	
	public float Bounciness
	{
		get { return bounciness; }
		set { bounciness = value; }
	}
	
	public float Friction
	{
		get { return friction; }
		set { friction = value; }
	}
	
	public float Braking
	{
		get { return braking; }
		set { braking = value; }
	}
	
	public float MoneyMultiplier
	{
		get { return moneyMultiplier; }
		set { moneyMultiplier = value; }
	}
	
	public float FuelPickup
	{
		get { return fuelPickup; }
		set { fuelPickup = value; }
	}
	
	public int AmmoPickup
	{
		get { return ammoPickup; }
		set { ammoPickup = value; }
	}
	
	public int CoinPickup
	{
		get { return coinPickup; }
		set { coinPickup = value; }
	}
	
	public bool Shield
	{
		get { return shield; }
		set { shield = value; }
	}
	
	public bool NoSlowOnFirst
	{
		get { return noSlowOnFirst; }
		set { noSlowOnFirst = value; }
	}
}

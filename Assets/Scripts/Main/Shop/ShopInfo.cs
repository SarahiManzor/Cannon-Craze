using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ShopInfo : MonoBehaviour 
{
	private string [,] descriptions;
	private bool [,] purchased;
	private bool [,] equipped;
	private int [,] price;
	
	private int categories = 4;
	private int itemsPerCatagory = 10;
	private int perkPrice = 6000;
	
	void Awake()
	{
		DontDestroyOnLoad(this);
		
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			//Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		}
	}
	
	void Start()
	{
		descriptions = new string[categories,itemsPerCatagory];
		purchased = new bool[categories,itemsPerCatagory];
		equipped = new bool[categories,itemsPerCatagory];
		price = new int [categories,itemsPerCatagory];
		
		setDescriptions();
		SetPrices();
		SetBoughtBools();
		SetEquipBools();
		Load();
	}
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/shopData.dat");
		
		ShopData data = new ShopData();
		
		data.Purchased = purchased;
		data.Equipped = equipped;
		data.PerkPrice = perkPrice;
		
		bf.Serialize(file,data);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/shopData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/shopData.dat",FileMode.Open);
			
			ShopData data = (ShopData)bf.Deserialize(file);
			file.Close();
			
			equipped = data.Equipped;
			purchased = data.Purchased;
			perkPrice = data.PerkPrice;
		}
	}
	
	void setDescriptions()
	{
		//First Category
		descriptions [0,0] = "WWI Cannon";
		descriptions [0,1] = "Iron Cannon";
		descriptions [0,2] = "Gold Cannon";
		descriptions [0,3] = "Neon Cannon";
		descriptions [0,4] = "Ultra Cannon";
		descriptions [0,5] = "Zues Cannon";
		descriptions [0,6] = "Gauss Cannon";
		descriptions [0,7] = "Ion Cannon";
		descriptions [0,8] = "Atom Cannon";
		descriptions [0,9] = "Nuclear Cannon";
		
		//Second Category
		descriptions [1,0] = "Jetpack Upgrade1";
		descriptions [1,1] = "Jetpack Upgrade2";
		descriptions [1,2] = "Jetpack Upgrade3";
		descriptions [1,3] = "Jetpack Upgrade4";
		descriptions [1,4] = "Jetpack Upgrade5";
		descriptions [1,5] = "Jetpack Upgrade6";
		descriptions [1,6] = "Jetpack Upgrade7";
		descriptions [1,7] = "Jetpack Upgrade8";
		descriptions [1,8] = "Jetpack Upgrade9";
		descriptions [1,9] = "Jetpack Upgrade10";
		
		//Third Category
		descriptions [2,0] = "Gun1";
		descriptions [2,1] = "Gun2";
		descriptions [2,2] = "Gun3";
		descriptions [2,3] = "Gun4";
		descriptions [2,4] = "Gun5";
		descriptions [2,5] = "Gun6";
		descriptions [2,6] = "Gun7";
		descriptions [2,7] = "Gun8";
		descriptions [2,8] = "Gun9";
		descriptions [2,9] = "Gun10";
		
		//Fourth Category
		descriptions [3,0] = "Coin packs are \nworth 50% more";
		descriptions [3,1] = "Fuel packs are \nworth 50% more";
		descriptions [3,2] = "Ammo packs give \nyou an extra bullet";
		descriptions [3,3] = "Start with 50% \nmore boost";
		descriptions [3,4] = "Immune to first \ndangerous obstacle";
		descriptions [3,5] = "You don't lose any \nspeed on the first bounce";
		descriptions [3,6] = "Earn 10% more per \nrun";
		descriptions [3,7] = "Lose less speed \non each bounce";
		descriptions [3,8] = "Perk9";
		descriptions [3,9] = "Perk10";
	}
	
	void SetPrices()
	{
		//First Category
		price [0,0] = 2000;
		price [0,1] = 4000;
		price [0,2] = 8000;
		price [0,3] = 16000;
		price [0,4] = 32000;
		price [0,5] = 64000;
		price [0,6] = 128000;
		price [0,7] = 256000;
		price [0,8] = 512000;
		price [0,9] = 1024000;
		
		//Second Category
		price [1,0] = 2000;
		price [1,1] = 4000;
		price [1,2] = 8000;
		price [1,3] = 16000;
		price [1,4] = 32000;
		price [1,5] = 64000;
		price [1,6] = 128000;
		price [1,7] = 256000;
		price [1,8] = 512000;
		price [1,9] = 1024000;
		
		//Third Category
		price [2,0] = 2000;
		price [2,1] = 4000;
		price [2,2] = 8000;
		price [2,3] = 16000;
		price [2,4] = 32000;
		price [2,5] = 64000;
		price [2,6] = 128000;
		price [2,7] = 256000;
		price [2,8] = 512000;
		price [2,9] = 1024000;
		
		//Fourth Category
		price [3,0] = perkPrice;
		price [3,1] = perkPrice;
		price [3,2] = perkPrice;
		price [3,3] = perkPrice;
		price [3,4] = perkPrice;
		price [3,5] = perkPrice;
		price [3,6] = perkPrice;
		price [3,7] = perkPrice;
		price [3,8] = perkPrice;
		price [3,9] = perkPrice;
	}
	
	void SetBoughtBools()
	{
		for(int i = 0; i < purchased.GetLength(0); i++)
		{
			for(int r = 0; r < purchased.GetLength(1); r++)
			{
				purchased [i,r] = false;
			}
			if(i!=3)
			{
				purchased [i,0] = true;
			}
		}
	}
	
	void SetEquipBools()
	{
		for(int i = 0; i < purchased.GetLength(0); i++)
		{
			for(int r = 0; r < purchased.GetLength(1); r++)
			{
				equipped [i,r] = false;
			}
			if(i!=3)
			{
				equipped [i,0] = true;
			}
		}
	}
	
	public void UpdatePerkPrices()
	{
		for (int i = 0; i < price.GetLength(1);i++)
		{
			price[3,i]*=2;
		}
		perkPrice *= 2;
	}
	
	public string getDescription(int a, int b)
	{
		return descriptions[a,b];
	}
	
	public int getPrice(int a, int b)
	{
		return price[a,b];
	}
	
	public bool getPurchased(int a, int b)
	{
		return purchased[a,b];
	}
	
	public bool getEquipped(int a, int b)
	{
		return equipped[a,b];
	}
	
	public void setPurchased(int a, int b)
	{
		purchased[a,b] = true;
	}
	
	public void setEquipped(int a, int b, bool booleanVar)
	{
		equipped[a,b] = booleanVar;
	}
	
	public int GetCatLength()
	{
		return itemsPerCatagory;
	}
	
}

[Serializable]
class ShopData
{
	private bool[,] equipped;
	private bool[,] purchased;
	private int perkPrice;
	
	public bool[,] Equipped
	{
		get { return equipped; }
		set { equipped = value; }
	}
	
	public bool[,] Purchased
	{
		get { return purchased; }
		set { purchased = value; }
	}
	
	public int PerkPrice
	{
		get { return perkPrice; }
		set { perkPrice = value; }
	}
}

using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour {
	
	private GameObject[] lockedLabel;
	private TextMesh[] lockedLabelText;
	
	private DataManager dm;
	private ShopInfo si;
	
	private TextMesh moneyLabel;
	private TextMesh cannonLevelLabel;
	private TextMesh jetpackLevelLabel;
	private TextMesh gunLabel;
	
	private TextMesh descriptionLabel;
	private TextMesh priceLabel;
	private TextMesh buyEquipButton;
	
	private int[] equipped;
	private int currentItem = 0;
	private int currentCategory = 0;
	
	private GameObject highlight;
	// Use this for initialization
	void Awake () 
	{
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
		si = GameObject.Find ("_SI").GetComponent<ShopInfo>();
	
		moneyLabel = GameObject.Find ("_Money").GetComponent<TextMesh>();
		
		descriptionLabel = GameObject.Find ("DescriptionLabel").GetComponent<TextMesh>();
		priceLabel = GameObject.Find ("PriceLabel").GetComponent<TextMesh>();
		buyEquipButton = GameObject.Find ("BuyEquipButton").GetComponent<TextMesh>();
		
		lockedLabel = GameObject.FindGameObjectsWithTag("LockLabel");
		equipped = new int [4];
		highlight = GameObject.Find("Highlighter");
	}
	
	void Start()
	{
		lockedLabelText = new TextMesh[lockedLabel.Length];
		for(int i = 0; i < lockedLabel.Length;i++)
		{
			lockedLabelText[i] = lockedLabel[i].GetComponent<TextMesh>();
		}
		
		for(int i = 0; i < lockedLabelText.Length;i++)
		{
			if(lockedLabelText[i].transform.parent.GetComponent<Upgrade>().IsEquipped())
			{
				lockedLabelText[i].text = "Equipped";
			}
			else if(lockedLabelText[i].transform.parent.GetComponent<Upgrade>().IsBought())
			{
				lockedLabelText[i].text = "";
			}
		}
		for(int i = 0;i<equipped.Length;i++)
		{
			Debug.Log (equipped[i]);
		}
	}
	
	public void Purchase()
	{
		if (si.getPurchased(currentCategory,currentItem) == false && dm.Money >= si.getPrice(currentCategory,currentItem))
		{
			si.setPurchased(currentCategory,currentItem);
			dm.Money -= si.getPrice(currentCategory,currentItem);
			buyEquipButton.transform.parent.GetComponent<AudioSource>().GetComponent<AudioSource>().Play();
			UpgradeStatus();
			
			if(currentCategory != 3)
			{
				for(int i = 0; i < si.GetCatLength();i++)
				{
					si.setEquipped(currentCategory,i,false);
					equipped[currentCategory] = currentItem;
				}
			}
			else
			{
				si.UpdatePerkPrices();
			}
			si.setEquipped(currentCategory,currentItem,true);
			
			dm.Save();
			si.Save();
		}
		else if(si.getPurchased(currentCategory,currentItem) && si.getEquipped(currentCategory,currentItem)==false)
		{
			UpgradeStatus();
			buyEquipButton.GetComponent<AudioSource>().Play();
			
			if(currentCategory != 3)
			{
				for(int i = 0; i < si.GetCatLength();i++)
				{
					si.setEquipped(currentCategory,i,false);
					equipped[currentCategory] = currentItem;
				}
			}
			si.setEquipped(currentCategory,currentItem,true);
			
			dm.Save();
			si.Save();
		}
		
		for(int i = 0; i < lockedLabelText.Length;i++)
		{
			if(lockedLabelText[i].transform.parent.GetComponent<Upgrade>().IsEquipped())
			{
				lockedLabelText[i].text = "Equipped";
			}
			else if(lockedLabelText[i].transform.parent.GetComponent<Upgrade>().IsBought())
			{
				lockedLabelText[i].text = "";
			}
		}
	}
	
	void UpgradeStatus()
	{
	//First Category(Cannons)
		if(currentCategory == 0)
		{
			if(currentItem == 0)
			{
				dm.PowerLevel = 12f;
			}
			else if(currentItem == 1)
			{
				dm.PowerLevel = 15f;
			}
			else if(currentItem == 2)
			{
				dm.PowerLevel = 20f;
			}
			else if(currentItem == 3)
			{
				dm.PowerLevel = 25f;
			}
			else if(currentItem == 4)
			{
				dm.PowerLevel = 32f;
			}
			else if(currentItem == 5)
			{
				dm.PowerLevel = 40f;
			}
			else if(currentItem == 6)
			{
				dm.PowerLevel = 51f;
			}
			else if(currentItem == 7)
			{
				dm.PowerLevel = 63f;
			}
			else if(currentItem == 8)
			{
				dm.PowerLevel = 79f;
			}
			else if(currentItem == 9)
			{
				dm.PowerLevel = 99f;
			}
			//dm.PowerLevel = currentItem*10f + 10f;
		}
	//Jetpack Category
		else if (currentCategory == 1)
		{
			if(currentItem == 1)
			{
				dm.JetpackLevel = 10f;
			}
			else if(currentItem == 1)
			{
				dm.JetpackLevel = 15f;
			}
			else if(currentItem == 2)
			{
				dm.JetpackLevel = 20f;
			}
			else if(currentItem == 3)
			{
				dm.JetpackLevel = 25f;
			}
			else if(currentItem == 4)
			{
				dm.JetpackLevel = 32f;
			}
			else if(currentItem == 5)
			{
				dm.JetpackLevel = 40f;
			}
			else if(currentItem == 6)
			{
				dm.JetpackLevel = 51f;
			}
			else if(currentItem == 7)
			{
				dm.JetpackLevel = 63f;
			}
			else if(currentItem == 8)
			{
				dm.JetpackLevel = 79f;
			}
			else if(currentItem == 9)
			{
				dm.JetpackLevel = 99f;
			}
			//dm.JetpackLevel = currentItem*0.66f + 2f;
		}
	//Gun Category
		else if(currentCategory == 2)
		{//Starts at 30 total Power(10 power * 3 ammo)
			if(currentItem == 0)//40 total power (gunPower*Ammo)
			{
				dm.GunPower = 10f;
				dm.Ammo = 3;
			}
			else if(currentItem == 1)//40 total power (gunPower*Ammo)
			{
				dm.GunPower = 10f;
				dm.Ammo = 4;
			}
			else if(currentItem == 2)//60 total power
			{
				dm.GunPower = 15f;
				dm.Ammo = 4;
			}
			else if(currentItem == 3)//90 total power
			{
				dm.GunPower = 18f;
				dm.Ammo = 5;
			}
			else if(currentItem == 4)//130 total power
			{
				dm.GunPower = 18f;
				dm.Ammo = 7;
			}
			else if(currentItem == 5)//180 total power
			{
				dm.GunPower = 30f;
				dm.Ammo = 6;
			}
			else if(currentItem == 6)//240 total power
			{
				dm.GunPower = 24f;
				dm.Ammo = 10;
			}
			else if(currentItem == 7)//310 total power
			{
				dm.GunPower = 31f;
				dm.Ammo = 10;
			}
			else if(currentItem == 8)//390 total power
			{
				dm.GunPower = 78f;
				dm.Ammo = 5;
			}
			else if(currentItem == 9)//480 total power
			{
				dm.GunPower = 48f;
				dm.Ammo = 10;
			}
			//dm.GunPower = 15 + currentItem*5;
			//dm.Ammo = 4 + currentItem;
		}
		else if (currentCategory == 3)
		{
			if(currentItem == 1)
			{
				dm.CoinPickup = 1500;
			}
			else if(currentItem == 2)
			{
				dm.FuelPickup = 150f;
			}
			else if(currentItem == 3)
			{
				dm.AmmoPickup = 3;
			}
			else if(currentItem == 4)
			{
				dm.Fuel = 150;
			}
			else if(currentItem == 5)
			{
				dm.Shield = true;
			}
			else if(currentItem == 6)
			{
				dm.NoSlowOnFirst = true;
			}
			else if(currentItem == 7)
			{
				dm.MoneyMultiplier = 1.1f;
			}
			else if(currentItem == 8)
			{
				dm.Bounciness = 0.8f;
				dm.Friction = 0.8f;
			}
		}
	}
	
	public void SetCurrent(int a, int b)
	{
		currentCategory = a;
		currentItem = b;
		Transform item = GameObject.Find("Upgrade" + (a+1) + (b+1)).transform;
		highlight.transform.position = item.position;
		highlight.transform.parent = item.parent;
	}
	
	public void Highlight()
	{
		Transform item = GameObject.Find("Upgrade" + (currentCategory+1) + (currentItem+1)).transform;
		highlight.transform.position = item.position;
		highlight.transform.parent = item.parent;
	}
	
	// Update is called once per frame
	void Update () 
	{
		moneyLabel.text = "$" + dm.Money;
		
		descriptionLabel.text = si.getDescription(currentCategory,currentItem);
		
		if(si.getPurchased(currentCategory,currentItem))
		{
			priceLabel.text = "Owned";
			if(si.getEquipped(currentCategory,currentItem))
			{
				buyEquipButton.text = "Equipped";
			}
			else
			{
				buyEquipButton.text = "Equip";
			}
		}
		else 
		{
			priceLabel.text = "Price: " + si.getPrice(currentCategory,currentItem);
			buyEquipButton.text = "Buy";
		}
	}
	
	public void SetCurrentStart(int a)
	{
		currentCategory = a;
		currentItem = equipped[a];
	}
	
	public void UpdateEquips(int a, int b)
	{
		equipped[a] = b;
	}
}
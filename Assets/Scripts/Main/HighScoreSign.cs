using UnityEngine;
using System.Collections;

public class HighScoreSign : MonoBehaviour {

	private Transform player;
	private DataManager dm;
	private TextMesh scoreNum;
	private bool move = false;
	
	// Use this for initialization
	void Awake ()
	{
		scoreNum = GameObject.Find("HighScoreNum").GetComponent<TextMesh>();
		dm = GameObject.Find ("_DM").GetComponent<DataManager>();
		player = GameObject.Find ("Player").transform;
	}
	
	void Start ()
	{
		transform.position = new Vector3(dm.HighScorePos,transform.position.y,transform.position.z);
		scoreNum.text = dm.HighScore + "";
	}
	
	public void CheckMove () 
	{
		if(transform.position.x<player.position.x)
		{
			scoreNum.text = dm.HighScore + "";
			dm.HighScorePos = player.position.x;
			move = true;
		}
	}
	
	void Update()
	{
		if(move)
		{
			float posX = transform.position.x;
			posX = Mathf.Lerp(posX,player.position.x,Time.deltaTime*5f);
			transform.position = new Vector3 (posX,transform.position.y,transform.position.z);
		}
	}
}

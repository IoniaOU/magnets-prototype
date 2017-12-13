using UnityEngine;
using System.Collections;

//Ayfer
public class LevelCreator : MonoBehaviour 
{
	public GameObject blueMagnet;
	public GUIStyle blueMagnetStyle;
	
	public GameObject orangeMagnet;
	public GUIStyle orangeMagnetStyle;
	
	public GameObject player;
	public GameObject finish;
	public GameObject wall1;
	public GameObject wall2;
	public GameObject wall3;
	public GameObject wall4;
	public GameObject wall5;
	
	public static Level currentLevel;
	public static int CurrentLevelID = 1;
	void Awake()
	{	
		Level[] levels = new Level[]{
			new Level(
				new LevelItem[]{
					new LevelItem(orangeMagnet,new Vector3(-4,0,-2), Quaternion.identity),
					new LevelItem(blueMagnet,new Vector3(-3,0,0),Quaternion.identity),
					new LevelItem(player,new Vector3(-2,0,0),Quaternion.identity),
					new LevelItem(wall2,new Vector3(0,0,0),Quaternion.Euler(0,25,0)),
					new LevelItem(finish,new Vector3(4,0,00),Quaternion.identity)},
				new PlayerItem[]{
					new PlayerItem(orangeMagnet,2, orangeMagnetStyle),
					new PlayerItem(blueMagnet,1,blueMagnetStyle)}),
			new Level(
				new LevelItem[]{
					new LevelItem(blueMagnet,new Vector3(-5,0,-2), Quaternion.identity),
					new LevelItem(wall1,new Vector3(5.83f,0,4.98f),Quaternion.Euler(0,90,0)),
					new LevelItem(wall3,new Vector3(4.85f,0,2.13f),Quaternion.Euler(0,0,0)),
					new LevelItem(wall4,new Vector3(6.85f,0,1.13f),Quaternion.Euler(0,0,0)),
					new LevelItem(wall4,new Vector3(1,0,-1),Quaternion.Euler(0,90,0)),
					new LevelItem(wall5,new Vector3(2,0,-3),Quaternion.Euler(0,90,0)),
					new LevelItem(player,new Vector3(-4,0,-2), Quaternion.identity),
					new LevelItem(finish,new Vector3(5.85f,0,2.5f),Quaternion.identity)},
				new PlayerItem[]{
					new PlayerItem(orangeMagnet,2, orangeMagnetStyle),
					new PlayerItem(blueMagnet,1,blueMagnetStyle)})};
		
		currentLevel = levels[CurrentLevelID];
		
		foreach (LevelItem l in currentLevel.LevelItemList)
		{
			Instantiate(l.Item,l.LevelItemPosition,l.LevelItemQuaternion);
		}	
	}
	
	void Start ()
	{
		
	}
	void Update ()
	{
	
	}
}

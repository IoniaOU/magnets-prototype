using UnityEngine;
using System.Collections;

//Mustafa
public class Level
{
	public LevelItem[] LevelItemList;
	public PlayerItem[] PlayerItemList;
	public Level(LevelItem[] _levelItemList,PlayerItem[] _playerItemList)
	{
		LevelItemList = _levelItemList;
		PlayerItemList = _playerItemList;
	}
}

using UnityEngine;
using System.Collections;

//Mustafa
public class LevelItem
{
	public GameObject Item;
	public Vector3 LevelItemPosition;
	public Quaternion LevelItemQuaternion;
	public LevelItem(GameObject _item, Vector3 _levelItemPosition, Quaternion _levelItemQuaternion)
	{
		Item = _item;
		LevelItemPosition = _levelItemPosition;
		LevelItemQuaternion = _levelItemQuaternion;
	}
		
}

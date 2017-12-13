using UnityEngine;
using System.Collections;

//Ayfer
public class PlayerItem
{
	public GameObject Item;
	public byte Count;
	public GUIStyle Style;
	
	public PlayerItem(GameObject _item, byte _count, GUIStyle _style)
	{
		Item = _item;
		Count = _count;
		Style = _style;
	}
}
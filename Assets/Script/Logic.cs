using UnityEngine;
using System.Collections;

//Ayfer
public class Logic : MonoBehaviour 
{
	public enum GameStatus {Settlement, Running, Pause, Result}
	public GameStatus _status;
	public static GameStatus status;
	void Start () 
	{
		status = GameStatus.Settlement;
	}
	void Update ()
	{
		status=_status;
		if(status==GameStatus.Running)
		{
			Time.timeScale = 1;
		}
		else
		{
			Time.timeScale = 0;
		}
	}
}
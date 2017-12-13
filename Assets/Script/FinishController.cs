using UnityEngine;
using System.Collections;

//Mustafa
public class FinishController : MonoBehaviour
{

	public bool isFinished=false;
	public float FinishBufferTime;
	void Start ()
	{
		transform.GetComponent<Renderer>().material.color = Color.cyan;
	}
	
	void Update ()
	{
		if(isFinished)
		{
			FinishBufferTime-=Time.deltaTime;
		}
		if(FinishBufferTime < 0)
		{
			Logic.status = Logic.GameStatus.Result;
			isFinished =false;
			FinishBufferTime = 3f;
			transform.GetComponent<Renderer>().material.color = Color.cyan;
		}
	}
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag=="Player")
		{
			isFinished=true;
			transform.GetComponent<Renderer>().material.color = Color.blue;
			col.rigidbody.isKinematic = true;
		}
	}
}

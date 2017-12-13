using UnityEngine;
using System.Collections;

//Mustafa
public class CircleDraw : MonoBehaviour
{
	//public bool _enabled = true;
	public float firstR=0;
	public float maximumR=10;
	public float width=1;
	public float speed=1;
	public int delta;
	private float r;
	void Start ()
	{
		r=firstR;
	
	}
	
	void Update () 
	{
		
		r+=delta*((r/maximumR)/10)*speed;
		if(r<0.5f||r>maximumR)
		{
			r=firstR;
		}
		float x = 0;
		float y = 0;
		//float theta_scale = 0.1f;             //Set lower to add more points
		//double size = (2.0 * Mathf.PI) / theta_scale; //Total number of points in circle.
		
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Shader/lightning"));
		lineRenderer.SetVertexCount(63);
		lineRenderer.SetWidth(((maximumR/r)/50)*width,((maximumR/r)/50)*width);
		
		Vector3 firstPos = Vector3.zero;
		int i = 0;
		for(float theta = 0; theta < 2 * Mathf.PI; theta += 0.1f)
		{
			
    		x = r*Mathf.Cos(theta);
    		y = r*Mathf.Sin(theta);
			Vector3 pos = new Vector3(x+gameObject.transform.position.x, 0.1f, y+gameObject.transform.position.z);
    		lineRenderer.SetPosition(i, pos);
			i+=1;
			if(theta==0)
			{
				firstPos = pos;
			}
		}
		lineRenderer.SetVertexCount(64);
		lineRenderer.SetPosition(i, firstPos);
		/*
		if(Logic.status!=Logic.GameStatus.Settlement)
		{
			lineRenderer.SetVertexCount(0);
			lineRenderer.SetWidth(0,0);
		}
		*/
		
	}
}

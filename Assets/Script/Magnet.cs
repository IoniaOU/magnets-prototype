using UnityEngine;
using System.Collections;

//Mustafa
public class Magnet : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform == gameObject.transform)
                    {
						gameObject.GetComponent<CircleDraw>().enabled=!gameObject.GetComponent<CircleDraw>().enabled;
						gameObject.GetComponent<CircularGravity>().enabled=!gameObject.GetComponent<CircularGravity>().enabled;
						gameObject.GetComponent<LineRenderer>().enabled=!gameObject.GetComponent<LineRenderer>().enabled;
                    }
                }
            }
        }
	
	}
}

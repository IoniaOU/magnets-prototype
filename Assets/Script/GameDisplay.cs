using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Emre
public class GameDisplay : MonoBehaviour
{
    #region Import to Editor
    public Camera _camera;
	//public LevelCreator _levelCreator;
    public GameObject goBlue;
	public GameObject goOrange;
    #endregion

    #region BoxRect
    public Rect BlueBox;
    #endregion

    #region List
    public List<BoxItem> boxItem = new List<BoxItem>();
    private List<Transform> itemListener = new List<Transform>();
    #endregion

    #region GUIStyle
    public GUIStyle boxBg;
    #endregion

    private BoxItem selectedItem = null;
	private BoxItem currSelectedItem = null;
    private BoxItem selectedGameObject = null;
	
	private int itemWaitToSelectTime = 30;
    private bool isChangeToSelect = false;
	
	int time = 0;
	
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
		
        itemListener.Add(transform);
		int j = 0;
		foreach(PlayerItem p in LevelCreator.currentLevel.PlayerItemList){
			boxItem.Add(new BoxItem(p.Style, new Rect(10 * XResolution.WidthFactor, j + 10 * XResolution.HeightFactor, 80 * XResolution.WidthFactor, 80 * XResolution.HeightFactor), p.Item));
			j += 90;
		}
	}

    private float moveX = -999f;
    private float moveY = -999f;

	void Update () {

        if (Input.touchCount == 1)
        {
            Touch touch = Input.touches[0];

            if (touch.phase == TouchPhase.Began && selectedItem == null)
            {
                foreach (BoxItem bi in boxItem)
                {
                    if (bi.BoxItemRect.Contains(new Vector2(touch.position.x, Screen.height - touch.position.y)) && Logic.status == Logic.GameStatus.Settlement )
                    {
                        selectedItem = bi;
                    }
                }
            }
			if (touch.phase == TouchPhase.Began && !isChangeToSelect && Logic.status == Logic.GameStatus.Settlement)
            {
                Ray ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform.tag == "PlayerItem")
                    {
                        foreach (BoxItem bi in boxItem)
                        {
                            if (bi.BoxObject.name == hit.transform.name)
                            {
								Debug.Log("Selected Current Item");
                                isChangeToSelect = true;
                                currSelectedItem = bi;
                            }
                        }
                    }
                }
            }
			if (touch.phase == TouchPhase.Stationary && isChangeToSelect)
            {
                Ray ray = _camera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform.tag == "PlayerItem")
                    {
                        foreach (BoxItem bi in boxItem)
                        {
                            if (bi.BoxObject.name == hit.transform.name)
                            {
                                if (currSelectedItem == bi)
                                {
                                    time++;
                                    if (time >= itemWaitToSelectTime)
                                    {
                                        selectedItem = bi;
                                        Destroy(hit.transform.gameObject);
                                        itemListener.Remove(hit.transform);
                                        time = 0;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (touch.phase == TouchPhase.Moved && selectedItem != null)
            {
                moveX = touch.position.x;
                moveY = touch.position.y;
            }
            if (touch.phase == TouchPhase.Ended && selectedItem != null)
            {
				if(selectedItem != null)
				{
					moveX = -999f;
	                moveX = -999f;
	
	                Ray ray = _camera.ScreenPointToRay(touch.position);
	                RaycastHit hit;
	
	                if(Physics.Raycast(ray, out hit, 1000f)){
	                    if (hit.collider.tag == "Floor") {
	                        GameObject item = Instantiate(selectedItem.BoxObject, new Vector3(hit.point.x, 0, hit.point.z), Quaternion.identity) as GameObject;
	                        
							foreach (BoxItem bi in boxItem)
                            {
                                item.name = selectedItem.BoxObject.name;
                            }
							
	                        foreach (Transform tr in itemListener)
	                        {
	                            if (Vector3.Distance(tr.position, item.transform.position) <= 1.0f)
	                            {
	                                Destroy(item);
	                                return;
	                            }
	                        }
	                        itemListener.Add(item.transform);
	                    }
	                }
	
	                selectedItem = null;
				}
				selectedItem = null;
                currSelectedItem = null;
                isChangeToSelect = false;
                time = 0;
            }
        }
	}
    int i = 0;
    void OnGUI() {

        if(Logic.status == Logic.GameStatus.Settlement){
			GUI.Box(new Rect(0, 0, 100 * XResolution.WidthFactor, 540 * XResolution.HeightFactor), "", boxBg);

	        foreach(BoxItem bi in boxItem){
	            GUI.Box(bi.BoxItemRect, "", bi.BoxItemSytle);
	        }
		}

        if (selectedItem != null) 
        {
            GUI.Box(new Rect(moveX - (selectedItem.BoxItemRect.width) / 2, 
                (Screen.height - moveY) - (selectedItem.BoxItemRect.height) / 2, 
                selectedItem.BoxItemRect.width, 
                selectedItem.BoxItemRect.height), "", 
                selectedItem.BoxItemSytle);
        }
    }
}
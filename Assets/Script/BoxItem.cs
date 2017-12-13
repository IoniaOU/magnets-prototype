using UnityEngine;
using System.Collections;

//Emre
public class BoxItem {

    private GUIStyle boxItemSytle;
    private Rect boxItemRect;
    private GameObject boxObject;
	private bool isHide;

    public BoxItem(GUIStyle guiStyle, Rect rect, GameObject go) {
        boxItemSytle = guiStyle;
        boxItemRect = rect;
        boxObject = go;
    }

    /// <summary>
    /// BoxItem için Sytle bilgilerini verir
    /// </summary>
    public GUIStyle BoxItemSytle
    {
        get
        {
            return boxItemSytle;
        }
    }

    /// <summary>
    /// BoxItem için Rect bilgilerini verir
    /// </summary>
    public Rect BoxItemRect
    {
        get
        {
            return boxItemRect;
        }
    }

    /// <summary>
    /// BoxItem için GameObje yi verir
    /// </summary>
    public GameObject BoxObject
    {
        get
        {
            return boxObject;
        }
    }
	
	public static Rect GetPosition(int index){
		int ks = index * 90;
		return new Rect(10 * XResolution.WidthFactor, ks + (10 * XResolution.HeightFactor), (80 * XResolution.WidthFactor), (80 * XResolution.HeightFactor));
	}
}
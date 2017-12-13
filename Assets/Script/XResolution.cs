using UnityEngine;
using System.Collections;


public class XResolution {
    private const int WIDTH = 960;
    private const int HEIGHT = 540;

    /// <summary>
    /// Ekran çözünürlüğünün genişlik katsayı değerini verir.
    /// </summary>
    public static float WidthFactor
    {
        get
        {
            return (float)Screen.width / (float)WIDTH;
        }
    }

    /// <summary>
    /// Ekran çözünürlüğünün yükseklik katsayı değerini verir.
    /// </summary>
    public static float HeightFactor
    {
        get
        {
            return (float)Screen.height / (float)HEIGHT;
        }
    }
}

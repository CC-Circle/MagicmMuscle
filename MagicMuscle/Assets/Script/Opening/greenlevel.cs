using UnityEngine;
using UnityEngine.Rendering;


public class greenlevel : MonoBehaviour
{
    public static double inValue;//tmpはUnityに入ってきた値
    public static double inMax = 100;//inMaxは
    private float positionx;
    private double scale;

    void Start()
    {
        inValue = 0;
        
        scale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        positionx = this.transform.position.x;
        if (inValue >= inMax * 2.0 / 3)
        {
            scale = 200;

        }
        else
        {
            scale = inValue / inMax * 300 ;
        }
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, (float)scale);  
        this.transform.position = new Vector3(positionx, (float)(scale / 2 + 50), 0);

        inValue += 0.1;


        
    }

}

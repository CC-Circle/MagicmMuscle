using UnityEngine;
using UnityEngine.Rendering;


public class redlevel : MonoBehaviour
{
    
    
    private float positionx;
    private double scale;
    

    void Start()
    {

        
        scale = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        positionx = this.transform.position.x;
        if (greenlevel.inValue < greenlevel.inMax * 5.0 / 6)
        {
            scale = 0;

        } 
        else
        {
            scale = greenlevel.inValue / greenlevel.inMax * 300 -250;
        }
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, (float)scale);  
        this.transform.position = new Vector3(positionx, (float)(scale / 2 + 300), 0);

        greenlevel.inValue += 0.1;


        
    }

}

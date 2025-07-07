using UnityEngine;

public class ScreenToWorldShot : MonoBehaviour
{
    private Vector3 HD = new Vector3(1920,1080,0);
    public Vector3 input = new Vector3(0,0,2);
    private Vector3 screenObj;
    public GameObject gameobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenObj = Camera.main.ScreenToWorldPoint(input);
        Instantiate(gameobject, screenObj, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition+input;
            screenObj = Camera.main.ScreenToWorldPoint(mousePosition);
            GameObject obj = Instantiate(gameobject, screenObj, Quaternion.identity);
            BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
            bms.input = mousePosition;
            
        }
    }
}

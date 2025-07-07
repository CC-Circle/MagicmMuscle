using UnityEngine;

public class BallMoveScreen : MonoBehaviour
{
    ScreenToWorldShot sts;
    private Vector3 HD = new Vector3(1920, 1080, 0);
    public Vector3 input ;
    private Vector3 screenObj;
    public float speed = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenObj = Camera.main.ScreenToWorldPoint(input);
    }

    // Update is called once per frame
    void Update()
    {
        input.z+=speed;
        screenObj = Camera.main.ScreenToWorldPoint(input);
        this.transform.position = screenObj;
    }
}

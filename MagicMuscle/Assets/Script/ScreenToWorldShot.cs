using UnityEngine;

public class ScreenToWorldShot : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip clip1;
    private Vector3 HD = new Vector3(1920,1080,0);
    public Vector3 input = new Vector3(0,0,2);
    private Vector3 screenObj;
    public GameObject gameobject;
    public bool charge = false;
    public static float maxpower = 0;
    private CameraShake camerashake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        audioSource = GetComponent<AudioSource>(); // 必要なら動的に取得
        maxpower = 0;
        screenObj = Camera.main.ScreenToWorldPoint(input);
        Instantiate(gameobject, screenObj, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Serial.strong = Mathf.Abs(Serial.strong);
        Debug.Log(Serial.strong);
        if (Serial.strong > 100)
        {
            if (maxpower < Serial.strong)
            {
                maxpower = Serial.strong;
            }
            charge = true;
            //Debug.Log("power!!!");
        }else if (charge)
        {
            camerashake.Shake();
            audioSource.PlayOneShot(clip1);
            shoot(new Vector3(HD.x/2,HD.y/2,0));
            charge = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            audioSource.PlayOneShot(clip1);
            shoot(Input.mousePosition);
            maxpower = 200;
        }
    }

    public void shoot(Vector3 yourinput)
    {
        Vector3 mousePosition = yourinput + input;
        screenObj = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject obj = Instantiate(gameobject, screenObj, Quaternion.identity);
        BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
        bms.input = mousePosition;
        bms.powerscale = maxpower;
        maxpower = 0;

    }
}

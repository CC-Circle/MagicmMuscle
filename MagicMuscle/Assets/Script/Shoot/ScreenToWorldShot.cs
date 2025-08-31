using UnityEngine;

public class ScreenToWorldShot : MonoBehaviour
{
    public ArmAnimation aruanimation;
    private AudioSource audioSource;
    public AudioClip clip1;
    private Vector3 HD = new Vector3(1920,1080,0);
    //
    public Vector3 input = new Vector3(0,0,2);
    private Vector3 screenObj;
    public GameObject gameobject;
    public GameObject StrongBullet;
    public static bool charge = false;
    public static float maxpower = 0;
    private CameraShake camerashake;

    public SliderCharge slidercharge;
    public bool sliderChargemode;
    public int shakeend = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            slidercharge = GameObject.Find("barsmaster").GetComponent<SliderCharge>();
        }
        catch
        {
            slidercharge = null;
        }
       
        if (slidercharge != null) {
            sliderChargemode = true;
        }
        else
        {
            sliderChargemode = false;
        }

        charge = false;
        aruanimation = GameObject.Find("Arm").GetComponent<ArmAnimation>();
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
        //if (Input.GetKey(KeyCode.Space)) {
        //    //Serial.strong +=10;
        //}
        //else
        //{
        //    if (!Serial.isConect)
        //    {
        //        Serial.strong = 0;
        //    }
        //}

        //Debug.Log(Serial.strong);
        if (sliderChargemode && Serial.shake>0.4) {
            shakeend = 1;
            
        }
        else
        {
            if (shakeend == 1 && OrangePointer.isCatch==1)
            {
                camerashake.Shake();
                //shoot(new Vector3(HD.x/2,HD.y/2,0));
                // 1920x1080のピクセル座標へ変換
                float pixelX = (OrangePointer.pointerX * 1920);
                //float pixelY = (OrangePointer.pointerY * 1080);
                float pixelY = 1080 / 2;

                shoot(new Vector3(pixelX, pixelY));
                shakeend = 0;
            }
            
        }

        //センサーの場合
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
            if (!sliderChargemode) {
                camerashake.Shake();
                //shoot(new Vector3(HD.x/2,HD.y/2,0));
                // 1920x1080のピクセル座標へ変換
                float pixelX = (OrangePointer.pointerX * 1920);
                float pixelY = (OrangePointer.pointerY * 1080);
                shoot(new Vector3(pixelX, pixelY));
            }

           
            charge = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
           
            shoot(Input.mousePosition);
            maxpower = 200;
        }

        
    }
    //発射したい地点を選択
    public void shoot(Vector3 yourinput)
    {
        //効果音
        audioSource.PlayOneShot(clip1);
        aruanimation.StartAnime();
        Vector3 mousePosition = yourinput + input;
        screenObj = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject Bullet=gameobject;
        if (Serial.isConect)
        {
            if (maxpower > YourPower.maxValue-500)
            {
                Bullet = StrongBullet;
            }
        }
        else {
            if (Input.GetKey(KeyCode.S))
            {
                Bullet = StrongBullet;
            }
            
        }
        GameObject obj = Instantiate(Bullet, screenObj, Quaternion.identity);
        BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
        bms.input = mousePosition;

        //bms.powerscale = maxpower;
        maxpower = 0;

    }


}

using UnityEngine;

public class ScreenToWorldShot : MonoBehaviour
{
    public ArmAnimation aruanimation;
    private AudioSource audioSource;
    public AudioClip clip1,clip2;
    private Vector3 HD = new Vector3(1920,1080,0);
    //
    public Vector3 input = new Vector3(0,0,2);
    private Vector3 screenObj;
    public GameObject gameobject;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public GameObject Bullet4;
    public static bool charge = false;
    public static float maxpower = 0;
    private CameraShake camerashake;

    public SliderCharge slidercharge;
    public bool sliderChargemode;
    public int shakeend = 0;
    //モードチェンジ
    public bool isSimple;
    public bool isshakeDeg;

    public SliderPowerCharge sliderpowercharge;
    

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
        try {
            aruanimation = GameObject.Find("Arm").GetComponent<ArmAnimation>();
        }
        catch
        {}
        try
        {
            camerashake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        }
        catch
        {}


        audioSource = GetComponent<AudioSource>(); // 必要なら動的に取得
        maxpower = 0;
        screenObj = Camera.main.ScreenToWorldPoint(input);
        Instantiate(gameobject, screenObj, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {


        Serial.strong = Mathf.Abs(Serial.strong);

        if (isshakeDeg) {
            if (Serial.isDegShake)
            {
                Debug.Log("shake!!!");
                camerashake.Shake();
                //shoot(new Vector3(HD.x/2,HD.y/2,0));
                // 1920x1080のピクセル座標へ変換
                //float pixelX = 1920/2;
                ////float pixelY = (OrangePointer.pointerY * 1080);
                //float pixelY = 1080 / 2;
                float pixelX = 1920 / 2;
                
                float pixelY = 1080 / 2;
                //shoot(new Vector3(pixelX, pixelY));
                shootCharge(new Vector3(pixelX, pixelY));

                //shootshake(new Vector3(pixelX, pixelY));

            }
        }
        else if(!isSimple)
        {
            if (sliderChargemode && Serial.shake > 0.4)
            {
                shakeend = 1;
            }
            else
            {
                if (shakeend == 1 && OrangePointer.isCatch == 1)
                {
                    camerashake.Shake();
                    //shoot(new Vector3(HD.x/2,HD.y/2,0));
                    // 1920x1080のピクセル座標へ変換
                    float pixelX = 1920/2;
                    //float pixelY = (OrangePointer.pointerY * 1080);
                    float pixelY = 1080 / 2;
                    shootCharge(new Vector3(pixelX, pixelY));
                   
                    shakeend = 0;
                }
            }
        }
        else
        {
            if (Serial.isShake)
            {
                //Debug.Log("shake!!!");
                camerashake.Shake();
                //shoot(new Vector3(HD.x/2,HD.y/2,0));
                // 1920x1080のピクセル座標へ変換
                //float pixelX = 1920/2;
                ////float pixelY = (OrangePointer.pointerY * 1080);
                //float pixelY = 1080 / 2;
                float pixelX = (OrangePointer.pointerX * 1920);
                float pixelY = (OrangePointer.pointerY * 1080);
                shoot(new Vector3(pixelX, pixelY));
                shootCharge(new Vector3(pixelX, pixelY));
              
                shootshake(new Vector3(pixelX, pixelY));
               
            }
        }

       

        //センサーの場合
        if (Serial.strong > Serial.chargevalue)
        {
            
            if (maxpower < Serial.strong)
            {
                maxpower = Serial.strong;
            }
            charge = true;
            Debug.Log("power!!!");
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
            if (sliderChargemode)
            {
                shootCharge(Input.mousePosition);

            }
            else {
                shoot(Input.mousePosition);
            }

            
            maxpower = 200;
        }

        
    }
    //発射したい地点を選択
    public void shoot(Vector3 yourinput)
    {
        
        //効果音
        audioSource.PlayOneShot(clip1);
        if (aruanimation != null) {
            aruanimation.StartAnime();
        }
        Vector3 mousePosition = yourinput + input;
        screenObj = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject Bullet=gameobject;

        //if (Serial.isConect)
        //{
        //    if (maxpower > YourPower.maxValue - 500)
        //    {
        //        Bullet = Bullet2;
        //    }
        //    if (sliderpowercharge.endcharge)
        //    {
        //        Bullet = Bullet2;
        //    }
        //}
        //else {
        //    if (Input.GetKey(KeyCode.S))
        //    {
        //        Bullet = Bullet2;
        //    }

        //}
        //if (sliderpowercharge.charge==1)
        //{
        //    Bullet = Bullet2;
        //}

        switch (sliderpowercharge.charge)
        {
            case 0:
                audioSource.PlayOneShot(clip1);
                Bullet = gameobject;
                break;
            case 1:
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet2;
                break;
            case 2:
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet3;
                break;
            case 3:
                audioSource.PlayOneShot(clip2);
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet4;
                break;
            default:
                return;
        }


        GameObject obj = Instantiate(Bullet, screenObj, Quaternion.identity);
        BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
        bms.input = mousePosition;

        //bms.powerscale = maxpower;
        maxpower = 0;
        sliderpowercharge.InitAllSliderValue();

    }
    //発射したい地点を選択
    public void shootshake(Vector3 yourinput)
    {
        Debug.Log("shake");
        //効果音
        audioSource.PlayOneShot(clip1);
        if (aruanimation != null)
        {
            aruanimation.StartAnime();
        }
        Vector3 mousePosition = yourinput + input;
        screenObj = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject Bullet = gameobject;
       
        GameObject obj = Instantiate(Bullet, screenObj, Quaternion.identity);
        BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
        bms.input = mousePosition;

        //bms.powerscale = maxpower;
        maxpower = 0;
        sliderpowercharge.InitAllSliderValue();

    }

    //発射したい地点を選択
    public void shootCharge(Vector3 yourinput)
    {
        //効果音
        if (aruanimation != null)
        {
            aruanimation.StartAnime();
        }
        Vector3 mousePosition = yourinput + input;
        screenObj = Camera.main.ScreenToWorldPoint(mousePosition);

        GameObject Bullet = gameobject;


        switch (slidercharge.charge){
            case 0:
                audioSource.PlayOneShot(clip1);
                Bullet = gameobject;
                break;
            case 1:
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet2;
                break;
            case 2:
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet3;
                break;
            case 3:
                
                audioSource.PlayOneShot(clip2);
                audioSource.PlayOneShot(clip1);
                Bullet = Bullet4;
                break;
            default:
                return;
        }

        GameObject obj = Instantiate(Bullet, screenObj, Quaternion.identity);
        BallMoveScreen bms = obj.GetComponent<BallMoveScreen>();
        bms.input = mousePosition;
        slidercharge.InitAllSliderValue();
        //bms.powerscale = maxpower;
        maxpower = 0;

    }


}

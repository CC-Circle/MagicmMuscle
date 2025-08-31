//using UnityEngine;
//using UnityEngine.Audio;

//public class SliderCharge : MonoBehaviour
//{

//    public UnityEngine.UI.Slider[] slider =  new UnityEngine.UI.Slider[2];
//    public AudioClip audioclip,chargedone;
//    private AudioSource audiosource;
//    private float startpitch = 1;
//    public float pollenPoint;
//    public int pollenReleaseRate;
//    public UnityEngine.UI.Image sliderImage; //connected the Image Fill from the slider
//    [HideInInspector] public bool noseScaleChange = false;
//    //public Color32[] Colors;

//    private bool Entercharge = false;
//    public bool feverFlag;
//    public int charge=0;

//    // Start is called before the first frame update
//    void Start()
//    {
//        audiosource = GetComponent<AudioSource>();
//        audiosource.loop = true; // ループさせる
//        if (YourPower.maxValue == 0) {

//        }

//        int cnt = 0;
//        // 全てのsliderに対して処理
//        foreach (var s in slider)
//        {
//            if (s != null)
//            {
//                s.maxValue = YourPower.maxValue;
//                s.value = 0;
//                slider[0].value = 0;
//                //s.color = Colors[cnt];
//                pollenPoint = 0;
//                feverFlag = false;
//            }
//            cnt++;
//        }

//    }

//    private void Update()
//    {
//        if (slider[charge].value >= slider[charge].maxValue) {
//            Serial.entercharge = false;
//            Serial.strong = 0;
//            audiosource.Stop();
//            audiosource.PlayOneShot(chargedone);
//            FazeChange();
//        }

//        if (Input.GetKey(KeyCode.Space))
//        {
//            //audiosource.pitch = startpitch + charge;
//            //audiosource.Play();

//            UpdateSlider(slider[charge]); 
//        }
//        if (Input.GetKeyDown(KeyCode.Space))
//        {

//            audiosource.pitch = startpitch + charge;
//            audiosource.Play();
//        }

//    }

//    public bool EmittingObject()
//    {

//        if (pollenPoint < pollenReleaseRate)
//        {
//            return false;
//        }
//        return true;
//    }

//    public void FazeChange()
//    {

//        if (charge < slider.Length-1) {
//            charge++;
//        }

//    }

//    // Update is called once per frame
//    private void UpdateSlider(UnityEngine.UI.Slider slider)
//    {
//        if (slider != null)
//        {
//            Debug.Log(Serial.strong);
//            if (slider.value < Serial.strong)
//            {
//                slider.value = Serial.strong;
//            }

//            //if (feverFlag)
//            //    sliderImage.color = new Color32(255, 209, 0, 255);
//            //else if (pollenPoint > 100)
//            //    sliderImage.color = new Color32(255, 209, 0, 255);
//            //else
//            //    sliderImage.color = new Color32(80, 255, 0, 255);
//        }
//    }

//    public void InitAllSliderValue() {
//        // 全てのsliderに対して処理
//        foreach (var s in slider)
//        {
//            if (s != null)
//            {
//                s.maxValue = YourPower.maxValue;
//                s.value = 0;
//                slider[0].value = 0;
//                pollenPoint = 0;
//                feverFlag = false;

//            }
//        }
//    }

//}

using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SliderCharge : MonoBehaviour
{
    public UnityEngine.UI.Slider[] slider = new UnityEngine.UI.Slider[2];
    public AudioClip audioclip, chargedone;
    private AudioSource audiosource;
    private float startpitch = 1;
    public float pollenPoint;
    public int pollenReleaseRate;
    public UnityEngine.UI.Image sliderImage;
    [HideInInspector] public bool noseScaleChange = false;

    public Serial serial;

    public bool feverFlag;
    public int charge = 0;

    public bool endcharge = false;
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.loop = true;

        int cnt = 0;
        foreach (var s in slider)
        {
            if (s != null)
            {
                s.maxValue = YourPower.maxValue*200;
                s.value = 0;
                slider[0].value = 0;
                pollenPoint = 0;
                feverFlag = false;
            }
            cnt++;
        }
    }

    void Update()
    {
        if (slider[charge].value >= slider[charge].maxValue&&!endcharge)
        {
           
            serial.entercharge = false;
            audiosource.Stop();
            audiosource.PlayOneShot(chargedone);
            FazeChange(); // ここで振動させたい
        }
        //serial.entercharge = false;
        if (serial.entercharge)
        {
            
            UpdateSlider(slider[charge]);
        }
        if (serial.ischargedown)
        {
            audiosource.pitch = startpitch + charge/2.0f;
            audiosource.Play();
        }
        if (serial.ischargeup)
        {
            audiosource.Stop();
        }
    }

    public void FazeChange()
    {
        // 全てのスライダーを振動させる
        foreach (var s in slider)
        {
            if (s != null)
            {
                StartCoroutine(ShakeSlider(s));
            }
            else
            {
                endcharge = true;
            }
        }

        if (charge < slider.Length - 1)
        {
            charge++;
        }
        //else {
        //    endcharge = true;
        //}

    }

    private void UpdateSlider(UnityEngine.UI.Slider slider)
    {
        if (slider != null)
        {
            Debug.Log(Serial.strong);
            //if (slider.value < Serial.strong)
            //{
            //    slider.value = Serial.strong;
            //}
            slider.value += Serial.strong;
        }
    }

    public void InitAllSliderValue()
    {
        foreach (var s in slider)
        {
            if (s != null)
            {
                s.maxValue = YourPower.maxValue;
                s.value = 0;
                slider[0].value = 0;
                pollenPoint = 0;
                feverFlag = false;
            }
        }
    }

    // スライダーを振動させるコルーチン
    private IEnumerator ShakeSlider(UnityEngine.UI.Slider s)
    {
        RectTransform rect = s.GetComponent<RectTransform>();
        Vector3 originalPos = rect.localPosition;

        float duration = 0.3f; // 振動時間
        float magnitude = 10f; // 揺れ幅(px)
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            rect.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.localPosition = originalPos; // 最後に元の位置へ戻す
    }
}

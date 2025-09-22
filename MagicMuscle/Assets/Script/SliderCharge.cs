using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SliderCharge : MonoBehaviour
{
    public SpriteFlash spriteflash;
    //バーが一本のみで、シュート後にリセットなどをしないモードはtrue
    public bool IsSingleMode = false;
//スライダーたち
    public UnityEngine.UI.Slider[] slider = new UnityEngine.UI.Slider[2];
//効果音
    public AudioClip audioclip, chargedone;
    private AudioSource audiosource;
    //初期のピッチ
    private float startpitch = 1;
//シリアル通信
    public Serial serial;
//チャージの段階
    public int charge = 0;
//センサーの値を保存するか
 
    public bool endcharge = false;
    //最大の場合は音を鳴らす
    public bool isMaxSound = false;

    //ゲージ更新時にチャージしないようにする
    private bool limitCharge = false;

    private Vector3 StartLocalPos;

    public float sliderPersent = 0;//0~1の値でどれほどチャージできたか
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.loop = true;

        int cnt = 0;
        foreach (var s in slider)
        {
            if (s != null)
            {
                
                s.maxValue = YourPower.maxValue/2+ cnt*(YourPower.maxValue / 4);
                s.value = 0;
                slider[0].value = 0;
                RectTransform rect = s.GetComponent<RectTransform>();
                StartLocalPos = rect.transform.localPosition;
            }
            cnt++;
        }

    }

    void Update()
    {
        

        //serial.entercharge = false;
        if (charge < slider.Length)
        {
            if (slider[charge].value >= slider[charge].maxValue && !endcharge)
            {
                if (isMaxSound)
                {
                    audiosource.PlayOneShot(chargedone);
                    isMaxSound = false;
                }
                
                FazeChange(); // ここで振動させたい
            }
            else {
                isMaxSound = true;
            }

            if (charge < slider.Length )
            {
                if (!limitCharge)
                {
                    UpdateSlider(slider[charge]);
                }
            }

        }
        if (limitCharge && !Serial.ischarge)
        {
            limitCharge = false;
        }

        if (serial.ischargedown)
        {
            audiosource.pitch = startpitch + charge/2.0f;
            audiosource.Play();
        }
        if (serial.ischargeup)
        {
            //audiosource.Stop();
        }
    }

    public void FazeChange()
    {
        if (spriteflash != null)
        {
            spriteflash.Flash();
        }
       
        limitCharge = true;
        // 全てのスライダーを振動させる
        foreach (var s in slider)
        {
            if (s != null)
            {
                StartCoroutine(ShakeSlider(s));
            }
           
        }
        if (charge < slider.Length)
        {
            if (!IsSingleMode)
            {
                audiosource.pitch = startpitch + charge / 2.0f;
                audiosource.Play();
                charge++;
            }
        }
        else
        {
            endcharge = true;
        }

    }

    private void UpdateSlider(UnityEngine.UI.Slider slider)
    {
        if (slider != null)
        {
            
            Debug.Log(Serial.strong);
            slider.value = Serial.strong;
            sliderPersent = slider.value / slider.maxValue;

        }
    }

    public void InitAllSliderValue()
    {
        limitCharge = false;
        sliderPersent = 0;
        audiosource.Stop();
        charge = 0;
        int initall = 0;
        foreach (var a in slider)
        {
            a.value =0;
            if (a.value <= 0)
            {
                a.value = 0;
                initall++;
            }
            if (initall > slider.Length - 1)
            {
                break;
            }
            StartCoroutine(InitValue());
        }
    }

    private IEnumerator InitValue()
    {
            yield return new WaitForSeconds(0.1f);
    }

    // スライダーを振動させるコルーチン
    private IEnumerator ShakeSlider(UnityEngine.UI.Slider s)
    {

        RectTransform rect = s.GetComponent<RectTransform>();
        Vector3 originalPos = StartLocalPos;

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

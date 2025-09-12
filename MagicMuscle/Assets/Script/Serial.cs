//using System.Collections;
//using System.Collections.Generic;
//using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;
using System.Diagnostics;
//using System.Diagnostics;
//using UnityEditor.VersionControl;

public class Serial : MonoBehaviour
{

    private static Serial instance;
    [SerializeField] private string portName;
    [SerializeField] private int baurate;

    public bool conect = false, connect_char = false;
    public SerialPort serial;
    private bool isLoop = true;
    public float xfl = 10000f, zfl = 10000f;
    public string cntx, cntz, x = "50", z = "50";
    public static bool isConect = false;
    public static float strong = 0, shake = 0, deg = 0;

    //チャージ関連

    public static int chargevalue = 300;//チャージ開始閾値
    public static bool ischarge = false;//チャージ中かどうか
    public bool entercharge = false;//チャージ開始時
    public bool ischargedown = false;//チャージを押した時
    public bool ischargeup = false;

    //振る関連
    public static float shakevalue = 0.3f;
    public static bool isShake = false;

    //振った角度の発射
    public static float degshakeval = 0.9f;
    public static bool isDegShake = false;//振った時
    public static bool isDegShakeEnd = false;//振った状態を解除した時
    public static bool isDeg = false;//振った閾値かどうか

    void Awake()
    {
        degshakeval = 0.9f;
        isDegShake = false;//振った時
        isDeg = false;//振った閾値かどうか
        deg = 0;
        isShake = false;
        shakevalue = 1f;
        ischargeup = false;
        ischargedown = false;//チャージを押した時
        ischarge = false;//チャージ中かどうか
        chargevalue = 400;
        entercharge = false;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        strong = 0;
        this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);
        serial.DtrEnable = true;
        try
        {

            UnityEngine.Debug.Log("catch");
            this.serial.Open();
            //別スレッドで実行  
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
            conect = true;
            isConect = true;
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ポートが開けませんでした。設定している値が間違っている場合があります");
            conect = false;
            isConect = false;
        }
    }
    private void Update()
    {
        //一定の角度以上の場合
        if (deg > degshakeval) {
            //振った瞬間を判定
            if (!isDeg) {
                isDegShake = true;
            }
            else
            {
                isDegShake = false;
            }
            //振っている
            isDeg = true;
            isDegShakeEnd = false;
        }
        else
        {
            //振った状態から解除された場合
            if (isDeg) {
                isDegShakeEnd = true;
            }
            else {
                isDegShakeEnd = false;
            }
            isDegShake = false;
            //振っているかどうか
            isDeg = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            deg = deg * 180f;
            if (deg < 180) {
                UnityEngine.Debug.Log("shake!!");
                deg+=10;
            }
            deg = deg / 180f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            deg = deg * 180f;
            if (deg > 0)
            {
                deg-=10;
            }
            deg = deg / 180f;
        }

        if (deg == 0)
        {

        }
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    entercharge = true;
        //}
        if (shake>shakevalue) {
            isShake = true;
        }
        else
        {
            isShake = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            strong += 20;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {

            strong = 0;
        }



        if (strong > chargevalue)
        {
            UnityEngine.Debug.Log("StartCarge!!!");
            if (!ischarge)
            {

                ischargedown = true;
                entercharge = true;

            }
            else
            {
                UnityEngine.Debug.Log("ischargedown");
                ischargedown = false;
            }
            ischarge = true;
        }
        else
        {
            UnityEngine.Debug.Log("Nocharge");
            entercharge = false;
            if (ischarge)
            {
                ischargeup = true;
            }
            else
            {
                ischargeup = false;
            }
            ischarge = false;
        }
        UnityEngine.Debug.Log("Scencer" + entercharge);
        UnityEngine.Debug.Log("Deg:" + deg);

    }
    //データ受信時に呼ばれる
    public void ReadData()
    {
        while (this.isLoop)
        {
            string line = serial.ReadLine().Trim();
            if (string.IsNullOrEmpty(line)) return;

            char header = line[0];           // 先頭文字（W or A）
            string valueStr = line.Substring(1); // 数値部分

            if (float.TryParse(valueStr, out float value))
            {
                if (header == 'W')
                {
                    strong = value;
                    strong = Mathf.Abs(strong);

                    //UnityEngine.Debug.Log("Weight = " + strong);
                }
                else if (header == 'Y')
                {
                    shake = value;
                    //UnityEngine.Debug.Log("shake= " + shake);
                }
                else if (header == 'D')
                {
                    deg = value;
                    if (deg < 0)
                    {
                        if (deg > -90) {
                            deg = 0;
                        }
                        else {
                            deg = 180;
                        }

                    }
                    deg = 180 - deg;
                    deg = deg / 180f;
                    //if (deg > 90)
                    //{
                    //    deg = 90;
                    //}
                    //if (deg < 0)
                    //{
                    //    deg = 0;
                    //}
                    //deg = 90 - deg;
                    //UnityEngine.Debug.Log("shake= " + shake);
                }
            }

        }

        void OnDestroy()
        {

            if (serial != null)
            {
                this.isLoop = false;
                this.serial.Close();
                this.serial.Dispose();
            }
        }

    }
}
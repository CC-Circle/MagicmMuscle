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
    public static float strong = 0, shake = 0;
    void Awake()
    {

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
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("ポートが開けませんでした。設定している値が間違っている場合があります");
            conect = false;
        }
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
                    //UnityEngine.Debug.Log("Weight = " + strong);
                }
                else if (header == 'A')
                {
                    shake = value;
                   // UnityEngine.Debug.Log("AccelStrength = " +shake);
                }
            }
            ////Debug.Log("whilestart");
            //cntx = this.serial.ReadLine();
            //UnityEngine.Debug.Log("x" +cntx);
            //connect_char = true;
            //float.TryParse(cntx, out strong);
        }

        void OnDestroy()
        {
            if (serial != null)
            {
                this.isLoop = false;
                this.serial.Close();
            }
        }

    }
}
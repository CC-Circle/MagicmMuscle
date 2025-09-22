using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // UnityEngine.SceneManagemntの機能を使用
using Unity.VisualScripting;


public class Timer : MonoBehaviour
{
    AudioSource audiosorce;
    public AudioClip timerstart;
    public AudioClip timerzero;

    public GameObject text;
    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;
    public Text timerText;
    public float waitTime = 1000;
    public string scenename;
    //タイマーのていし
    public static bool timerStop;
    void Start()
    {
        timerStop = false;
        //minute = 0;
        //seconds = 0;
        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
        timerText = GetComponentInChildren<Text>();
        audiosorce = GetComponent<AudioSource>();

    }

    float countdown = 3f;
    int count;

    void Update()
    {
        if (!timerStop)
        {
            Countdown();
        }
    }
    public void Countdown()
    {
        if (countdown >= 0)
        {
            countdown -= Time.deltaTime;
            count = (int)countdown;
        }
        if (countdown <= 0)
        {
            //　制限時間が0秒以下なら何もしない
            if (totalTime <= 0f)
            {
                return;
            }
            //　一旦トータルの制限時間を計測；
            totalTime = minute * 60 + seconds;
            totalTime -= Time.deltaTime;

            //　再設定
            minute = (int)totalTime / 60;
            seconds = totalTime - minute * 60;

            //　タイマー表示用UIテキストに時間を表示する
            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;
            //　制限時間以下になったらコンソールに『制限時間終了』という文字列を表示する
            if (totalTime <= 0f)
            {
                audiosorce.PlayOneShot(timerzero);
                Debug.Log("制限時間終了");
                //Instantiate(End);
                StartCoroutine(nameof(LoadScene));
                // ResultSceneに切り替える
            }
        }
    }
    IEnumerator LoadScene()
    {
        //Instantiate(End);
        if (text != null)
        {
            Instantiate(text);
        }
        yield return new WaitForSeconds(waitTime);
        Serial seria;//呼ぶスクリプトにあだ名をつける
        GameObject objc = GameObject.Find("sencer");//Circleというゲームオブジェクトを探す
        seria = objc.GetComponent<Serial>();//スクリプトを取得
        //seria.serial.Close();
        SceneManager.LoadScene(scenename);
        //SceneManager.LoadScene(targetSceneName);
    }
    public void OnTimerStart()
    {
        audiosorce.PlayOneShot(timerstart);
    }
}
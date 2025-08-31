//using UnityEngine;
//using TMPro;
//using System.Collections;
//using UnityEngine.SceneManagement;
//public class YouSrPower : MonoBehaviour
//{
//    [Header("しきい値")]
//    public float threshold = 1.0f; // 計測開始するしきい値
//    [Header("計測時間")]
//    public float measureDuration = 3.0f; // 計測する秒数
//    [Header("UI表示用")]
//    public TextMeshProUGUI messageText; // TMPテキストをアタッチ

//    public static bool isMeasuring = false;
//    public static bool  iscorrect = false;
//    public float measureTimer = 0f;
//    public static float maxValue = 0f;

//    public string scenename = "GameScene";


//    private void Start()
//    {
//        isMeasuring = false;
//        iscorrect = false;
//    }

//    void Update()
//    {
//        float currentValue = Serial.strong; // シリアルから取得した値

//        if (Input.GetKey(KeyCode.Space))
//        {
//            currentValue = 200;
//        }

//        if (!isMeasuring&&!iscorrect)
//        {

//            // しきい値を超えたら計測開始
//            if (currentValue >= threshold)
//            {
//                isMeasuring = true;
//                measureTimer = 0f;
//                maxValue = currentValue; // 計測開始時にリセット
//                messageText.text = "握り続けろ！！"; // 計測開始時の表示
//            }
//            else
//            {
//                // 閾値未満のとき
//                messageText.text = "ステッキを強くにぎれ！！！";
//            }
//        }
//        else if(!iscorrect)
//        {
//            if(currentValue <= threshold) {
//                isMeasuring = false;
//                maxValue = 0;
//            }

//            // 計測中はタイマーを進める
//            measureTimer += Time.deltaTime;

//            // 最大値を更新
//            if (currentValue > maxValue)
//            {
//                maxValue = currentValue;
//            }

//            // 3秒経過したら計測終了
//            if (measureTimer >= measureDuration)
//            {
//                iscorrect = true;
//                isMeasuring = false;
//                messageText.text = "素晴らしい！！"; // 計測終了時の表示
//                Debug.Log("計測終了 最大値: " + maxValue);
//            }
//            else
//            {
//                // 計測中に毎フレーム表示更新（保険）
//                messageText.text = "握り続けろ！！";
//            }
//        }
//        else {
//            StartCoroutine(Scenemove());

//            messageText.text = "素晴らしい！！"; // 計測終了時の表示
//            Debug.Log("計測終了 最大値: " + maxValue);
//        }

//        IEnumerator Scenemove(){
//            yield return new WaitForSeconds(3);
//            iscorrect = false;
//            SceneManager.LoadScene(scenename);


//        }
//    }
//}
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class YourPower : MonoBehaviour
{
    [Header("しきい値")]
    public float threshold = 1.0f; // 計測開始するしきい値
    [Header("計測時間")]
    public float measureDuration = 3.0f; // 計測する秒数
    [Header("UI表示用")]
    public TextMeshProUGUI messageText; // TMPテキストをアタッチ

    [Header("音楽設定")]
    public AudioSource audioSource; // AudioSourceをアタッチ
    public AudioClip correctMusic; // !iscorrectの場合（計測中）の音楽
    public AudioClip successMusic; // elseの場合（成功時）の音楽

    public static bool isMeasuring = false;
    public static bool iscorrect = false;
    public float measureTimer = 0f;
    public static float maxValue = 0f;
    public string scenename = "GameScene";

    private bool successMusicPlayed = false; // 成功音楽が再生されたかの管理

    private void Start()
    {
        isMeasuring = false;
        iscorrect = false;
        successMusicPlayed = false;

        // AudioSourceが設定されていない場合は自動で取得
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        float currentValue = Serial.strong; // シリアルから取得した値

        if (Input.GetKey(KeyCode.Space))
        {
            currentValue = 200;
        }

        if (!isMeasuring && !iscorrect)
        {
            // しきい値を超えたら計測開始
            if (currentValue >= threshold)
            {
                isMeasuring = true;
                measureTimer = 0f;
                maxValue = currentValue; // 計測開始時にリセット
                messageText.text = "握り続けろ！！"; // 計測開始時の表示

                // 計測中の音楽を再生
                PlayCorrectMusic();
            }
            else
            {
                // 閾値未満のとき
                messageText.text = "ステッキを強くにぎれ！！！";
                // 音楽を停止
                StopMusic();
            }
        }
        else if (!iscorrect)
        {
            if (currentValue <= threshold)
            {
                isMeasuring = false;
                maxValue = 0;
                // 音楽を停止
                StopMusic();
            }
            else
            {
                // 計測中はタイマーを進める
                measureTimer += Time.deltaTime;

                // 最大値を更新
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                }

                // 計測中の音楽が再生されていない場合は再生
                PlayCorrectMusic();

                // 3秒経過したら計測終了
                if (measureTimer >= measureDuration)
                {
                    iscorrect = true;
                    isMeasuring = false;
                    messageText.text = "素晴らしい！！"; // 計測終了時の表示
                    Debug.Log("計測終了 最大値: " + maxValue);

                    // 成功時の音楽を1回だけ再生
                    PlaySuccessMusic();
                }
                else
                {
                    // 計測中に毎フレーム表示更新（保険）
                    messageText.text = "握り続けろ！！";
                }
            }
        }
        else
        {
            StartCoroutine(Scenemove());
            messageText.text = "素晴らしい！！"; // 計測終了時の表示
            Debug.Log("計測終了 最大値: " + maxValue);
        }
    }

    // 計測中の音楽を再生
    private void PlayCorrectMusic()
    {
        if (audioSource != null && correctMusic != null)
        {
            if (!audioSource.isPlaying || audioSource.clip != correctMusic)
            {
                audioSource.clip = correctMusic;
                audioSource.loop = true; // ループ再生
                audioSource.Play();
            }
        }
    }

    // 成功時の音楽を1回だけ再生
    private void PlaySuccessMusic()
    {
        if (audioSource != null && successMusic != null && !successMusicPlayed)
        {
            audioSource.Stop(); // 現在の音楽を停止
            audioSource.clip = successMusic;
            audioSource.loop = false; // 1回のみ再生
            audioSource.Play();
            successMusicPlayed = true; // フラグを立てる
        }
    }

    // 音楽を停止
    private void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    IEnumerator Scenemove()
    {
        yield return new WaitForSeconds(3);
        iscorrect = false;
        successMusicPlayed = false; // フラグをリセット
        SceneManager.LoadScene(scenename);
    }
}
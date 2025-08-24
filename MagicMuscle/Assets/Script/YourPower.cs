using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
public class YouSrPower : MonoBehaviour
{
    [Header("しきい値")]
    public float threshold = 1.0f; // 計測開始するしきい値
    [Header("計測時間")]
    public float measureDuration = 3.0f; // 計測する秒数
    [Header("UI表示用")]
    public TextMeshProUGUI messageText; // TMPテキストをアタッチ

    private bool isMeasuring = false;
    private bool iscorrect = false;
    public float measureTimer = 0f;
    public static float maxValue = 0f;

    public string scenename = "GameScene";


    void Update()
    {
        float currentValue = Serial.strong; // シリアルから取得した値

        if (Input.GetKey(KeyCode.Space))
        {
            currentValue = 200;
        }

        if (!isMeasuring&&!iscorrect)
        {
            
            // しきい値を超えたら計測開始
            if (currentValue >= threshold)
            {
                isMeasuring = true;
                measureTimer = 0f;
                maxValue = currentValue; // 計測開始時にリセット
                messageText.text = "握り続けろ！！"; // 計測開始時の表示
            }
            else
            {
                // 閾値未満のとき
                messageText.text = "ステッキを強くにぎれ！！！";
            }
        }
        else if(!iscorrect)
        {
            if(currentValue <= threshold) {
                isMeasuring = false;
                maxValue = 0;
            }

            // 計測中はタイマーを進める
            measureTimer += Time.deltaTime;

            // 最大値を更新
            if (currentValue > maxValue)
            {
                maxValue = currentValue;
            }

            // 3秒経過したら計測終了
            if (measureTimer >= measureDuration)
            {
                iscorrect = true;
                isMeasuring = false;
                messageText.text = "素晴らしい！！"; // 計測終了時の表示
                Debug.Log("計測終了 最大値: " + maxValue);
            }
            else
            {
                // 計測中に毎フレーム表示更新（保険）
                messageText.text = "握り続けろ！！";
            }
        }
        else {
            StartCoroutine(Scenemove());

            messageText.text = "素晴らしい！！"; // 計測終了時の表示
            Debug.Log("計測終了 最大値: " + maxValue);
        }

        IEnumerator Scenemove(){
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(scenename);

        }
    }
}

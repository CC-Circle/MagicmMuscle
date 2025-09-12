//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using TMPro;


//public class end_text : MonoBehaviour
//{
//    public Image resultImage;      // UIのImageコンポーネント
//    public TextMeshProUGUI resultText;        // UIのTextコンポーネント
//    public List<ScoreResult> results; // スコアごとの結果リスト
//    public AudioSource audiosource;


//    public void Update()
//    {
//        ShowResult(end_Score.countup_score);
//    }
//    public void ShowResult(int score)
//    {
//        ScoreResult bestMatch = null;

//        foreach (var r in results)
//        {
//            if (score >= r.minScore)
//            {
//                bestMatch = r; // 条件を満たす中で一番スコアが高いものを採用
//            }
//        }

//        if (bestMatch != null)
//        {
//            resultImage.sprite = bestMatch.image;
//            resultText.text = bestMatch.message;
//            if (bestMatch.isSound)
//            {
//                audiosource.Stop();
//                audiosource.PlayOneShot(bestMatch.audioclip);
//            }
//            bestMatch.isSound = false;


//        }
//        else
//        {
//            // 閾値未満のときの処理
//            resultImage.sprite = null;
//            resultText.text = "スコアが低すぎます...";
//        }
//    }
//}

//using UnityEngine;
//using System.Collections.Generic;

//public class EndResultSpawner : MonoBehaviour
//{
//    public List<ScoreResult> results; // スコアごとの結果リスト
//    public AudioSource audiosource;
//    public Transform spawnParent;     // 生成先（空オブジェクトなどを設定しておく）

//    private GameObject currentInstance; // 今表示しているオブジェクト

//    void Update()
//    {
//        ShowResult(end_Score.countup_score);
//    }

//    public void ShowResult(int score)
//    {
//        ScoreResult bestMatch = null;

//        foreach (var r in results)
//        {
//            if (score >= r.minScore)
//            {
//                bestMatch = r;
//            }
//        }

//        if (bestMatch != null)
//        {
//            // すでに出ているものを削除
//            if (currentInstance != null)
//            {
//                Destroy(currentInstance);
//            }

//            // プレハブを生成
//            currentInstance = Instantiate(bestMatch.prefab, spawnParent);

//            // サウンド再生
//            if (bestMatch.isSound)
//            {
//                audiosource.Stop();
//                audiosource.PlayOneShot(bestMatch.audioclip);
//                bestMatch.isSound = false;
//            }
//        }
//        else
//        {
//            if (currentInstance != null)
//            {
//                Destroy(currentInstance);
//                currentInstance = null;
//            }
//        }
//    }
//}

using UnityEngine;
using System.Collections.Generic;

public class EndResultSpawner : MonoBehaviour
{
    public List<ScoreResult> results; // スコアごとの結果リスト
    public AudioSource audiosource;
    public Transform spawnParent;     // UIを置く親(Canvas配下を指定)
    
    private GameObject currentInstance; // 今表示しているUIプレハブ

    void Update()
    {
        ShowResult(end_Score.countup_score);
    }

    public void ShowResult(int score)
    {
        ScoreResult bestMatch = null;

        foreach (var r in results)
        {
            if (score >= r.minScore)
            {
                bestMatch = r; // 一番スコアが高い条件を採用
            }
        }

        if (bestMatch != null)
        {
            // 表示中のUIを削除
            if (currentInstance != null)
            {
                Destroy(currentInstance);
            }

            // UIプレハブを生成
            currentInstance = Instantiate(bestMatch.prefab, spawnParent);

            // サウンド再生
            if (bestMatch.isSound)
            {
                audiosource.Stop();
                audiosource.PlayOneShot(bestMatch.audioclip);
                bestMatch.isSound = false;
            }
        }
        else
        {
            // どの条件も満たさないときはUIを消す
            if (currentInstance != null)
            {
                Destroy(currentInstance);
                currentInstance = null;
            }
        }
    }
}

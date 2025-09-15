
using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class EndResultSpawner : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public List<ScoreResult> results; // スコアごとの結果リスト
    public AudioSource audiosource;
    public Transform spawnParent;     // UIを置く親(Canvas配下を指定)

    private GameObject currentInstance;   // 今表示しているUIプレハブ
    private ScoreResult lastResult = null; // 最後に表示した結果
    public end_Score endscore;

    public bool IsImageChange = false;

    public bool isChange = false;

    public bool ShowResult(int score)
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
            // 直前と同じプレハブなら処理しない
            if (lastResult == bestMatch)
            {
                isChange = true;
                return false;
            }
            Debug.Log("UIを切り替え");

            
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
            Text.text = bestMatch.text;
            // 現在の結果を記録
            lastResult = bestMatch;
            end_Score.countup_score =  bestMatch.minScore;
            endscore.scoreText.text = end_Score.countup_score + "kcal ";
            return true;
        }
        else
        {
            // どの条件も満たさないときはUIを消す
            if (currentInstance != null)
            {
                Destroy(currentInstance);
                currentInstance = null;
            }
            lastResult = null;
            return true;
        }
    }
}

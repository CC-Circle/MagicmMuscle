using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
public class end_Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコア表示用
    public int finalScore;            // 最終スコア（GameManagerから受け取る）
    public float duration = 2f;       // カウントアップにかける秒数
    public float StopTime;
    public static int countup_score;
    public bool IsCount = true;
    void Start()
    {
        countup_score = 0;
        // ① Scoreからスコアを受け取る
        finalScore = Score.score;

        if(finalScore == 0)
        {
            finalScore=30000;
        }
        // ② カウントアップ開始
        //StartCoroutine(CountUpScore());
    }

    public void StartCount()
    {
        StartCoroutine(CountUpScore());
    }

    public void StopCount()
    {
        Debug.Log("StopCount!!");
        StartCoroutine(StopCountUpScore(StopTime));
    }

    IEnumerator CountUpScore()
    {
       
            //scoreText.rectTransform.DOShakeAnchorPos(duration, 25f, 50, 90, true, true);
            float elapsed = 0f;
            countup_score = 0;
            while (elapsed < duration)
            {
                if (IsCount)
                {
                    elapsed += Time.deltaTime;
                    float progress = Mathf.Clamp01(elapsed / duration);
                    countup_score = Mathf.RoundToInt(finalScore * progress);
                    scoreText.text =  countup_score.ToString()+ "kcal ";
                }
            yield return null;

            }

            scoreText.text = finalScore.ToString() + "kcal ";

       
    }

    IEnumerator StopCountUpScore(float StopTime)
    {

        IsCount = false;
        yield return new WaitForSeconds(StopTime);
        IsCount = true;
    }
}

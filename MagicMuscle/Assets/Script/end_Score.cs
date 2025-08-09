using UnityEngine;
using TMPro;
using System.Collections;

public class ScoreAnimator : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // スコア表示用
    public int finalScore;            // 最終スコア（GameManagerから受け取る）
    public float duration = 2f;       // カウントアップにかける秒数

    void Start()
    {
        // ① Scoreからスコアを受け取る
        finalScore = Score.score;

        // ② カウントアップ開始
        StartCoroutine(CountUpScore());
    }

    IEnumerator CountUpScore()
    {
        float elapsed = 0f;
        int currentScore = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / duration);
            currentScore = Mathf.RoundToInt(finalScore * progress);
            scoreText.text = "kcal: " + currentScore.ToString();
            yield return null;
        }

         scoreText.text = "kcal: " + finalScore.ToString();

    }
}

using TMPro;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    public string custonText = "Score: ";
    public TextMeshProUGUI scoreText; // TextMeshProの参照

    void Start()
    {
        UpdateScoreText(); // 初期スコア表示
    }
    void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text =  Score.score.ToString() +"マッスル";
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ranking : MonoBehaviour
{

    [SerializeField, Header("数値")]
    int point;

    string[] ranking = { "ランキング1位", "ランキング2位", "ランキング3位", "ランキング4位", "ランキング5位" };
    public int ranknumber;

    int[] rankingValue = new int[5];
    public static int[] rankingshare = new int[5];
    [SerializeField, Header("表示させるテキスト")]
    TextMeshProUGUI[] rankingText = new TextMeshProUGUI[5];

    private bool awake = false;
    // Use this for initialization
    void Start()
    {
        ResetRanking();
    }

    void Update()
    {
        //if (!awake)
        //{
        //    awake = true;
        //    ResetRanking();
        //}
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearRanking();
        }
    }

    public void ResetRanking()
    {
        ranknumber = -1;
        rankingshare = rankingValue;
        GetRanking();
        SetRanking(Score.score);
        for (int i = 0; i < rankingText.Length; i++)
        {
            int lank = i + 1;
            if (Score.score == rankingValue[i])
                rankingText[i].color = Color.yellow;
            rankingText[i].text = lank + "st " + rankingValue[i].ToString();
            if (rankingValue[i] == Score.score / 100 + 1)
                ranknumber = lank;
        }
    }


    /// <summary>
    /// ランキング呼び出し
    /// </summary>
    void GetRanking()
    {
        //ランキング呼び出し
        for (int i = 0; i < ranking.Length; i++)
        {
            rankingValue[i] = PlayerPrefs.GetInt(ranking[i]);
        }
    }
    /// <summary>
    /// ランキング書き込み
    /// </summary>

    void SetRanking(int _value)
    {
        //書き込み用
        for (int i = 0; i < ranking.Length; i++)
        {
            //取得した値とRankingの値を比較して入れ替え
            if (_value > rankingValue[i])
            {
                var change = rankingValue[i];
                rankingValue[i] = _value;
                _value = change;
                ranknumber = i;
            }
        }
        //入れ替えた値を保存
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.SetInt(ranking[i], rankingValue[i]);
        }
    }

    /// <summary>
    /// 保存されているランキングをクリア
    /// </summary>
    void ClearRanking()
    {
        for (int i = 0; i < ranking.Length; i++)
        {
            PlayerPrefs.DeleteKey(ranking[i]); // 保存データ削除
            rankingValue[i] = 0;               // 配列も初期化
        }
        PlayerPrefs.Save();
    }
}


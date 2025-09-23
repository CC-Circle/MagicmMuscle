using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    [SerializeField, Header("ランキング保存数")]
    public int rankingSize = 100;   // 保存は100件
    [SerializeField, Header("表示件数")]
    public int displaySize = 5;     // 表示は5件

    string[] rankingKeys;           // PlayerPrefsのキー（100件分）
    public int[] rankingValue;             // スコア保存用配列（100件分）
    public static int[] rankingshare;

    [SerializeField, Header("表示用テキスト (上位5件)")]
    TextMeshProUGUI[] rankingText;  // インスペクタで5件分だけ割り当て

    public int ranknumber = -1;

    void Awake()
    {
        // 配列をランキング数に合わせて初期化
        rankingKeys = new string[rankingSize];
        rankingValue = new int[rankingSize];
        rankingshare = new int[rankingSize];

        for (int i = 0; i < rankingSize; i++)
        {
            rankingKeys[i] = $"ランキング{i + 1}位"; // PlayerPrefsのキー
        }
        ResetRanking();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearRanking();
        }
    }

    public void ResetRanking()
    {
        ranknumber = -1;
        GetRanking();
        InsertScore(Score.score);
        SaveRanking();

        // 上位5件だけUIに反映
        for (int i = 0; i < displaySize && i < rankingText.Length; i++)
        {
            //Debug.Log("Value" + rankingValue[i]);
            int lank = i + 1;
            if (Score.score == rankingValue[i])
                rankingText[i].color = Color.yellow;
            else
                rankingText[i].color = Color.red;

            rankingText[i].text = $"{lank}位 : {rankingValue[i]}";
        }
    }

    /// <summary>
    /// 保存されているランキングを読み込む
    /// </summary>
    void GetRanking()
    {
        for (int i = 0; i < rankingSize; i++)
        {
           
            rankingValue[i] = PlayerPrefs.GetInt(rankingKeys[i], 0);
            
        }
    }

    /// <summary>
    /// 新しいスコアを挿入（常に大きい順に保つ）
    /// </summary>
    void InsertScore(int newScore)
    {
        for (int i = 0; i < rankingSize; i++)
        {
            if (newScore > rankingValue[i])
            {
                // iの位置から右にシフト
                for (int j = rankingSize - 1; j > i; j--)
                {
                    rankingValue[j] = rankingValue[j - 1];
                }
                rankingValue[i] = newScore;
                ranknumber = i;
                break;
            }
        }
    }

    /// <summary>
    /// ランキングを保存
    /// </summary>
    void SaveRanking()
    {
        for (int i = 0; i < rankingSize; i++)
        {
            PlayerPrefs.SetInt(rankingKeys[i], rankingValue[i]);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 保存されているランキングをクリア
    /// </summary>
    void ClearRanking()
    {
        for (int i = 0; i < rankingSize; i++)
        {
            PlayerPrefs.DeleteKey(rankingKeys[i]);
            rankingValue[i] = 0;
        }
        PlayerPrefs.Save();
        ResetRanking();
    }
}

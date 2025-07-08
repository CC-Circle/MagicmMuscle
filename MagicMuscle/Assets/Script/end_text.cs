using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ← 追加！

public class end_text : MonoBehaviour
{
   //public Image characterImage;      // 先輩魔法少女のイラスト表示
    public TextMeshProUGUI messageText;          // セリフ表示

    // テスト用のスコア（仮に750点に固定）
    public int score = 750;
    public Image characterImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowEnding(score); // テスト用に固定スコアを使用


    }

     // ここが ShowEnding の定義！
    void ShowEnding(int score)
    {
       string imageName = "";
        string message = "";

        if (score < 200)
        {
            imageName = "Endings/fail_01";
            message = "……これで終わり？ 期待外れね。";
        }
        else if (score < 400)
        {
            imageName = "Endings/fail_02";
            message = "まぁ、初めてならこんなもんかしら。";
        }
        else if (score < 600)
        {
            imageName = "Endings/neutral";
            message = "ふーん、悪くはないわよ。";
        }
        else if (score < 800)
        {
            imageName = "Endings/good";
            message = "やるじゃない！私も驚いたわ。";
        }
        else
        {
            imageName = "Endings/great";
            message = "最高よ！あなたなら世界も救える！";
        }

        // 画像を読み込み、表示
        Sprite loadedSprite = Resources.Load<Sprite>(imageName);
        if (loadedSprite != null)
        {
            characterImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogWarning("イラストが見つかりません: " + imageName);
        }

        // メッセージを表示
        messageText.text = message;
    }
}
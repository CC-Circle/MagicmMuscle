using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    private string[] dialogues = {
        "やぁ。わたしはアナタの先輩に当たる魔法少女よ！",
        "それでこの子は（マスコット名）よ",
        "今からこの子が君のサポートをしてくれるはずよ！",
        "ワイは（マスコット名）や。よろしゅな！",
        "まずは変身や。ステッキを握ってくれ！",
        "握ると球を放てるんや。強く握るとより強い球が撃てるぞ！",
        "めいっぱい強い球を撃とう！"
    };

    private int currentIndex = 0;

    void Start()
    {
        // 最初のセリフを表示
        dialogueText.text = dialogues[currentIndex];

        // ボタンのクリックイベントを登録
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];
        }
        else
        {
            // 全部表示したらチュートリアル終了（非表示）
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialDialogue_M : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    private string[] dialogues = {
        "初めまして、君が新しい魔法少女か！",
        "隣の子は”ラパン”よ",
        "今からこの子が君のサポートをしてくれるはずだ！",
        "よろしくね！　ステッキは持ってるな？",
        "ステッキはカチッと鳴るまで握るんだ！",
        "ステッキを振ると球を放てるよ！",
        "振る前に何回も握ると強い球が出せるぞ!",
        "まずは変身だ！　１回グッと握ってくれ！"
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
            // 次のセリフを表示
            dialogueText.text = dialogues[currentIndex];
        }
        else if (currentIndex == dialogues.Length)
        {
            // 🔽 8文目が表示されたあとに、さらに1回押したら非表示
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }
}

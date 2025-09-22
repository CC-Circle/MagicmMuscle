using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class name : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    private string[] dialogues = {
        "　　主人公　",
        "先輩魔法少女",
        "先輩魔法少女",
        "　　主人公　",
        "　　主人公　",
        "　　主人公　",
        "先輩魔法少女",
        "　　ラパン　"
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
            // まだ残りがある場合は次のセリフを表示
            dialogueText.text = dialogues[currentIndex];
        }
        else
        {
            // 🔽 修正ポイント：
            // 全部表示した後にボタンを押すと非表示になる
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }
}

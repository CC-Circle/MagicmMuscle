using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class name : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    private string[] dialogues = {
        "先輩魔法少女",
        "先輩魔法少女",
        "先輩魔法少女",
        "　　ラパン　",
        "　　ラパン　",
        "　　ラパン　",
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

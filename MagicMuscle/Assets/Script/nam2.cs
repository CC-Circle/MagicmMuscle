using UnityEngine;
using TMPro;
using UnityEngine.UI;

// 1つのセリフデータ（名前＋色付き表示用）
[System.Serializable]
public class DialogueName
{
    public string text;       // 表示するキャラ名
    public Color color = Color.white; // キャラ名の色
}

public class nam2 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    [Header("キャラクター名リスト")]
    public DialogueName[] dialogues;     // インスペクターで設定

    private int currentIndex = 0;

    void Start()
    {
        if (dialogues.Length > 0)
        {
            ShowDialogue(currentIndex);
        }

        // ボタンのクリックイベントを登録
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            // まだ残りがある場合は次のセリフを表示
            ShowDialogue(currentIndex);
        }
        else
        {
            // 全部表示した後にボタンを押すと非表示になる
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    void ShowDialogue(int index)
    {
        DialogueName line = dialogues[index];
        string hexColor = ColorUtility.ToHtmlStringRGBA(line.color);

        // 名前に色を適用して表示
        dialogueText.text = $"<color=#{hexColor}>{line.text}</color>";
    }
}

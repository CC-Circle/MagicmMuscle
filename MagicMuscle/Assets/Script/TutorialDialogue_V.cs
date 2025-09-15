using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialDialogue_V : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン

    // 🔽 追加した部分
    public AudioSource audioSource;      // ボイス再生用
    public AudioClip[] voiceClips;       // セリフに対応するボイス

    private string[] dialogues = {
        "初めまして、君が新しい魔法少女か！",
        "隣の子は”ラパン”よ",
        "今からこの子が君のサポートをしてくれるはずだ！",
        "よろしくね！　ステッキは持ってるな？",
        "ステッキは強く握り続けるんや！",
        "ステッキを振ると球を放てるよ！",
        "力が強いほど強い球が出せるぞ!",
        "まずは変身だ！　ステッキを強く握り続けてくれ！"
    };

    private int currentIndex = 0;

    void Start()
    {
        // 最初のセリフを表示 
        dialogueText.text = dialogues[currentIndex];

        // 🔽 追加した部分（最初のボイスを再生）
        PlayVoice(currentIndex);

        // ボタンのクリックイベントを登録 
        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];

            // 🔽 追加した部分（次のセリフに合わせてボイスを再生）
            PlayVoice(currentIndex);
        }
        else if (currentIndex == dialogues.Length)
        {
            // 全部表示したらチュートリアル終了（非表示）
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    // 🔽 追加したメソッド
    void PlayVoice(int index)
    {
        if (audioSource != null && voiceClips != null && index < voiceClips.Length)
        {
            audioSource.Stop();
            audioSource.clip = voiceClips[index];
            audioSource.Play();
        }
    }
}

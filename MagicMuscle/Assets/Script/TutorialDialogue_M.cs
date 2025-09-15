using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialDialogue_M : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // セリフ表示用
    public Button nextButton;            // 次へボタン
    public AudioSource audioSource;      // ボイス再生用
    public AudioClip[] voiceClips;       // セリフに対応するボイス
    public Serial serial;
    public string scenename;
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
        serial = GameObject.Find("Serial").GetComponent<Serial>();
        // 最初のセリフとボイスを表示・再生
        dialogueText.text = dialogues[currentIndex];
        PlayVoice(currentIndex);

        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    private void Update()
    {
        if (serial.ischargedown) {
            ShowNextDialogue();
        }

    }
    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex];
            PlayVoice(currentIndex); // セリフに対応したボイスを再生
        }
        else if (currentIndex == dialogues.Length)
        {
            SceneManager.LoadScene(scenename);
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

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

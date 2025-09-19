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

    [Header("イラスト表示用")]
    public Image illustrationImage;      // 表示させたいイラスト
    public Sprite finalIllustration;     // 最後に表示するイラスト

    private string[] dialogues = {
        "私を魔法少女にしてください！",
        "敵は人を太らせることを企む組織’ギトギター’だ！",
        "君に戦う覚悟はあるか？！",
        "私はこの体型で沢山、たいへんなことがありました",
        "そんな辛い思いを広めたくない！",
        "だから私、戦います！！",
        "良い気合だ。隣の’ラパン’が君を魔法少女にしてくれる",
        "よろしゅうな新人！　このステッキを握るんや"
    };

    private int currentIndex = 0;

    void Start()
    {
        serial = GameObject.Find("Serial").GetComponent<Serial>();

        // 最初のセリフとボイスを表示・再生
        dialogueText.text = dialogues[currentIndex];
        PlayVoice(currentIndex);

        // イラストは最初は非表示にしておく
        if (illustrationImage != null)
        {
            illustrationImage.gameObject.SetActive(false);
        }

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

        if (currentIndex < dialogues.Length - 1)
        {
            // 通常のセリフ
            dialogueText.text = dialogues[currentIndex];
            PlayVoice(currentIndex);
        }
        else if (currentIndex == dialogues.Length - 1)
        {
            // 🔽 最後のセリフ＋イラスト表示
            dialogueText.text = dialogues[currentIndex];
            PlayVoice(currentIndex);

            if (illustrationImage != null && finalIllustration != null)
            {
                illustrationImage.sprite = finalIllustration;
                illustrationImage.gameObject.SetActive(true);
            }
        }
        else if (currentIndex >= dialogues.Length)
        {
            // 全部終わったらシーン遷移
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

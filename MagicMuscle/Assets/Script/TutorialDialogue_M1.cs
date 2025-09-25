using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class DialogueLine
{
    [TextArea(2, 5)]
    public string text;   // セリフ本文
    public Color color = Color.white; // セリフごとの色
}

public class TutorialDialogue_M1 : MonoBehaviour
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

    [Header("セリフリスト")]
    public List<DialogueLine> dialogues = new List<DialogueLine>();

    private int currentIndex = 0;

    void Start()
    {
        serial = GameObject.Find("Serial").GetComponent<Serial>();

        if (dialogues.Count > 0)
        {
            ShowDialogue(currentIndex);
            PlayVoice(currentIndex);
        }

        if (illustrationImage != null)
        {
            illustrationImage.gameObject.SetActive(false);
        }

        nextButton.onClick.AddListener(ShowNextDialogue);
    }

    private void Update()
    {
        if (serial.ischargedown)
        {
            ShowNextDialogue();
        }
    }

    void ShowNextDialogue()
    {
        currentIndex++;

        if (currentIndex < dialogues.Count - 1)
        {
            ShowDialogue(currentIndex);
            PlayVoice(currentIndex);
        }
        else if (currentIndex == dialogues.Count - 1)
        {
            // 🔽 最後のセリフ＋イラスト表示
            ShowDialogue(currentIndex);
            PlayVoice(currentIndex);

            if (illustrationImage != null && finalIllustration != null)
            {
                illustrationImage.sprite = finalIllustration;
                illustrationImage.gameObject.SetActive(true);
            }
        }
        else if (currentIndex >= dialogues.Count)
        {
            // 全部終わったらシーン遷移
            SceneManager.LoadScene(scenename);
            dialogueText.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
        }
    }

    void ShowDialogue(int index)
    {
        DialogueLine line = dialogues[index];

        string hexColor = ColorUtility.ToHtmlStringRGBA(line.color);

        // 🔽 セリフ本文だけを色付きで表示
        dialogueText.text = $"<color=#{hexColor}>{line.text}</color>";
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

using UnityEngine;

public class Tutorial_GameManager : MonoBehaviour
{
<<<<<<< Updated upstream
    private bool IsSteckUp = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
=======
    
    // 複数のステップを管理できるようにリスト化
    public List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    public TextMeshProUGUI TM;
    private int currentStepIndex = 0;

    private Serial serial;
    public SliderCharge slider;
    private bool isMoveTextTimer = false;
    public string scenename;
    // クラスのメンバに追加
    public AudioSource audioSource;   // 音声再生用
    public AudioClip[] voiceClips;    // 各セリフに対応する音声

>>>>>>> Stashed changes
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSteckUp)
        {
            if (audioSource != null && voiceClips != null && currentStepIndex < voiceClips.Length)
        {
            audioSource.Stop();
            audioSource.clip = voiceClips[currentStepIndex];
            audioSource.Play();
        }

        }
    }
}

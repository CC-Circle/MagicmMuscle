using UnityEngine;

public class EnemyAnima : MonoBehaviour
{
    private Animator animator;
    public string[] animationNames = { "isDeathR", "isDeathL" };

    // サウンド
    public AudioClip rollingClip,popClip;   // 鳴らす音（Inspectorで設定）
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();

        // AudioSource を取得（なければ追加）
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayRandomAnimation()
    {
        int index = Random.Range(0, animationNames.Length);
        animator.SetBool(animationNames[index], true);
    }

    void Ondeath()
    {
        gameObject.SetActive(false);
    }
     

    void Roling()
    {
        audioSource.PlayOneShot(rollingClip);

    }

    public void Splash(){
        animator.SetBool("isAttack", true);
    }

}

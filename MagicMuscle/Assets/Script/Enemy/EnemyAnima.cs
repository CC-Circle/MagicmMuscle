using UnityEngine;

public class EnemyAnima : MonoBehaviour
{
    //アニメーションをさせる
    private Animator animator;
    public string[] animationNames = { "isDeathR", "isDeathL" };
    void Start()
    {
        animator = GetComponent<Animator>();
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
}

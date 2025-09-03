using UnityEngine;

public class ArmAnimationContrloler : MonoBehaviour
{
    Animator animator;
    bool isPlaying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        //if (state.IsName("ArmAtk") && state.normalizedTime >= 1.0f)
        //{
        //    animator.SetBool("ArmAtk", false);
        //}
    }

    public void Attack()
    {
        animator.SetTrigger("ArmAtk"); // Triggerを発火させるだけでOK
    }
    void AttackEnd()
    {
        
        //animator.SetBool("ArmAtk", false);

    }

}

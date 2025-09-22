
using UnityEngine;

public class UI_Arm: StateMachineBehaviour
{
    // アニメーションが終了してステートを抜けるときに呼ばれる
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Score.instance != null)
        {
            Score.instance.isAnimated = false;
        }
    }
}

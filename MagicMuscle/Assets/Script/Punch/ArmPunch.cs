using UnityEngine;

public class ArmPunch : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioclip;
    public ArmAnimationContrloler ArmAnime;

    public SerialPunch serialpunch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)||serialpunch.entercharge){
            PunchR();
        }
        
    }
    void PunchR()
    {
        ArmAnime.Attack();
    }
}

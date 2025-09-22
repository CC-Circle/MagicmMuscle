using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score instance;
    public TMP_Text scoreText;
    public static int score = 0;
    public Animator animator;
    private int attackHash;
    public bool isAnimated = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackHash = Animator.StringToHash("Base Layer.Rize");
        score = 0;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text =score+ "マッスル ";
        if(score < 0) {
            score = 0;
        }

    }

    public void ScoreAdd(int addScore)
    {
        if (!isAnimated&&animator!=null)
        {
            animator.SetTrigger("IsGetMuscle");
            isAnimated = true;
        }
            

        //animator.SetTrigger("IsGetMuscle");
        score += addScore;
    }
    public void ScoreRed(int redScore)
    {
        if (animator != null)
        {
            animator.SetTrigger("IsLostMuscle");
            score -= redScore;
        }
        
    }

    void Awake()
    {
        CheckInstance();
    }

    
    void CheckInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnAnimationEnd()
    {
        isAnimated = false;
    }
}

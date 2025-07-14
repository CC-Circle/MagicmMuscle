using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score instance;
    public TMP_Text scoreText;
    public static int score = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "kcal: " + score;
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
}

using UnityEngine;
using System.Collections;
public class EndingGameManager : MonoBehaviour
{
    public GameObject daietto;
    public end_Score end_score;
    public bool isCount= false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3.0f);
        end_score.StartCount();
        daietto.SetActive(false);
    }
}

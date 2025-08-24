using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float startTime = 0;
    public List<SpawnEvent> spawnEvents;

    private float elapsedTime = 0f;

    private int nextEventIndex = 0;
   
    private Dictionary<SpawnPatternType, Action<SpawnEvent>> patternFunctions;

    void Start()
    {
        elapsedTime = startTime;
        // パターン関数を登録
        patternFunctions = new Dictionary<SpawnPatternType, Action<SpawnEvent>> {
            { SpawnPatternType.Single, SpawnSingle },
            { SpawnPatternType.Parallel3, SpawnParallel3 },
            { SpawnPatternType.Circle5, SpawnCircle5 }
        };
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > 120f) return;

        while (nextEventIndex < spawnEvents.Count && elapsedTime >= spawnEvents[nextEventIndex].time)
        {
            SpawnEvent e = spawnEvents[nextEventIndex];
            if (patternFunctions.ContainsKey(e.pattern))
            {
                patternFunctions[e.pattern](e);  // 登録された関数を呼び出す
            }
            nextEventIndex++;
        }
    }

    // ==== 各出現パターン関数 ====

    void SpawnSingle(SpawnEvent e)
    {
        SpawnOne(e.enemyPrefab, e.basePosition, e.animationTrigger);
    }

    void SpawnParallel3(SpawnEvent e)
    {
        float spacing = 2.0f;
        for (int i = -1; i <= 1; i++)
        {
            Vector3 pos = e.basePosition + new Vector3(i * spacing, 0, 0);
            SpawnOne(e.enemyPrefab, pos, e.animationTrigger);
        }
    }

    void SpawnCircle5(SpawnEvent e)
    {
        float radius = 3.0f;
        for (int i = 0; i < 5; i++)
        {
            float angle = i * Mathf.PI * 2f / 5f;
            Vector3 pos = e.basePosition + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            SpawnOne(e.enemyPrefab, pos, e.animationTrigger);
        }
    }

    // ==== 汎用スポーン処理 ====

    void SpawnOne(GameObject prefab, Vector3 pos, string animTrigger)
    {
        GameObject enemy = Instantiate(prefab, pos, Quaternion.identity);
        if (!string.IsNullOrEmpty(animTrigger))
        {
            Animator animator = enemy.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(animTrigger);
            }
        }
    }
}
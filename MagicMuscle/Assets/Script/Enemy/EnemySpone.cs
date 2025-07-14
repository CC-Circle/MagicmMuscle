using UnityEngine;

public class EnemySpone : MonoBehaviour
{
    public GameObject[] spawnPrefabs; // 生成するPrefabを登録
    public float spawnInterval = 2f;  // 生成間隔（秒）
    public Transform spawnPoint;      // 生成位置（オプション）

    public float minX = -5f; // ランダム範囲の最小X
    public float maxX = 5f;  // ランダム範囲の最大X

    void Start()
    {
        InvokeRepeating("SpawnRandomObject", 0f, spawnInterval);
    }

    void SpawnRandomObject()
    {
        if (spawnPrefabs.Length == 0) return;

        int index = Random.Range(0, spawnPrefabs.Length);
        GameObject selectedPrefab = spawnPrefabs[index];

        Vector3 basePosition = spawnPoint != null ? spawnPoint.position : transform.position;

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, basePosition.y, basePosition.z);

        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}

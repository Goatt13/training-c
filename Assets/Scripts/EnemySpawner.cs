using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-5f, -5f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(5f, 5f);

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            Instantiate(enemyPrefab, randomPos, Quaternion.identity);
        }
    }
}

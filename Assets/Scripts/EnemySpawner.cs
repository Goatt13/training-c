using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int enemyCount = 5;

    // Map borders
    private float mapMinX = -11f;
    private float mapMaxX = 11f;
    private float mapMinY = -5f;
    private float mapMaxY = 5f;

    // Enemies will spawn outside this range
    private float spawnMargin = 2f;

    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPos = GetRandomOutsidePosition();
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomOutsidePosition()
    {
        int side = Random.Range(0, 4); // 0=left, 1=right, 2=top, 3=bottom
        float x = 0, y = 0;

        switch (side)
        {
            case 0: // Left side
                x = Random.Range(mapMinX - spawnMargin * 2, mapMinX - spawnMargin);
                y = Random.Range(mapMinY, mapMaxY);
                break;
            case 1: // Right side
                x = Random.Range(mapMaxX + spawnMargin, mapMaxX + spawnMargin * 2);
                y = Random.Range(mapMinY, mapMaxY);
                break;
            case 2: // Top side
                x = Random.Range(mapMinX, mapMaxX);
                y = Random.Range(mapMaxY + spawnMargin, mapMaxY + spawnMargin * 2);
                break;
            case 3: // Bottom side
                x = Random.Range(mapMinX, mapMaxX);
                y = Random.Range(mapMinY - spawnMargin * 2, mapMinY - spawnMargin);
                break;
        }
        return new Vector2(x, y);
    }
}

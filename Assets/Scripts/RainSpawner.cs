using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    public GameObject rainDropPrefab;
    public float spawnRate = 0.1f;
    public float spawnWidth = 10f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnRain), 0f, spawnRate);
    }

    void SpawnRain()
    {
        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);
        Instantiate(rainDropPrefab, spawnPosition, Quaternion.identity);
    }
}
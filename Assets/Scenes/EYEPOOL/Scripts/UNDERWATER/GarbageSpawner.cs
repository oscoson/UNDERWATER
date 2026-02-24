
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{

    public GameObject[] garbagePrefabs;
    public float currentPopulation;
    [SerializeField] private float maxSpawnPopulation;
    [SerializeField] private float spawnRange; // X and Z Range
    [SerializeField] private Vector2 spawnHeightRange = new Vector2(-0.0808f, -0.033f); // Y Position
    [SerializeField] private Vector2 SpawnRotX;
    [SerializeField] private float spawnRotY;
    [SerializeField] private float spawnRotZ;

    private float spawnInterval;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnInterval = Random.Range(3f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPopulation < maxSpawnPopulation)
        { 
            spawnInterval -= Time.deltaTime;
            if(spawnInterval <= 0f)
            {
                SpawnGarbage();
                spawnInterval = Random.Range(3f, 10f);
            }
        }
    }

    void SpawnGarbage()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(spawnHeightRange.x, spawnHeightRange.y), Random.Range(-spawnRange, spawnRange));
        Instantiate(garbagePrefabs[Random.Range(0, garbagePrefabs.Length)], spawnPos, Quaternion.Euler(Random.Range(SpawnRotX.x, SpawnRotX.y), Random.Range(-spawnRotY, spawnRotY), Random.Range(-spawnRotZ, spawnRotZ)));
        currentPopulation++;
    }
}

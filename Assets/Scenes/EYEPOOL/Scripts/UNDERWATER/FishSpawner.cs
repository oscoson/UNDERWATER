using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public float currentPopulation;
    [SerializeField] private float maxSpawnPopulation;
    [SerializeField] private float spawnRange; // X and Z Range
    
    private float spawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnInterval = Random.Range(3f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPopulation < maxSpawnPopulation)
        {
            spawnInterval -= Time.deltaTime;
            if(spawnInterval <= 0f)
            {
                SpawnFish();
                spawnInterval = Random.Range(3f, 20f);
            }
        }
    }

    void SpawnFish()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0f, Random.Range(-spawnRange, spawnRange));
        GameObject spawnedFish = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)], spawnPos, Quaternion.Euler(0f, 90f, 0f));
        spawnedFish.AddComponent<Fish>();
        spawnedFish.AddComponent<SphereCollider>().isTrigger = true;
        spawnedFish.transform.localScale = Vector3.one * Random.Range(7f, 15f);
        spawnedFish.layer = LayerMask.NameToLayer("GameLogicLayer");
        foreach(Transform child in spawnedFish.transform)
        {
            child.gameObject.layer = spawnedFish.layer;
        }
        SetRigidBody(spawnedFish);
        currentPopulation++;
    }

    void SetRigidBody(GameObject fish)
    {
        Rigidbody rb = fish.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}

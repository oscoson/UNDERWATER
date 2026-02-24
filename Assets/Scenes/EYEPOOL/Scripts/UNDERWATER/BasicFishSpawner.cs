using UnityEngine;

public class BasicFishSpawner : MonoBehaviour
{
    public enum GeneralDirection
    {
        Left,
        Right,
        Forward,
        Backward
    }
    public GameObject[] fishPrefabs;
    public GeneralDirection generalDirection;
    [SerializeField] private float floatYRot;
    private float spawnInterval;
    [SerializeField] float posXRange;
    [SerializeField] float posZRange;
    [SerializeField] float parentZPositionOffset;
    private float posYRange = 12f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnInterval = Random.Range(1f, 10f);
    }

    void Update()
    {
        spawnInterval -= Time.deltaTime;
        if(spawnInterval <= 0f)
        {
            SpawnFish();
            spawnInterval = Random.Range(1f, 10f);
        }
    }

    void SpawnFish()
    {
        GameObject spawnedFish = Instantiate(fishPrefabs[Random.Range(0, fishPrefabs.Length)], new Vector3(transform.position.x, Random.Range(0, posYRange), parentZPositionOffset), Quaternion.Euler(0f, floatYRot, 0f));
        spawnedFish.AddComponent<BasicFish>();
        spawnedFish.GetComponent<BasicFish>().fishDirection = SetDirection();
        spawnedFish.transform.localScale = Vector3.one * Random.Range(7f, 15f);
        // if(posXRange > 0f)
        // {
        //     spawnedFish.transform.position = new Vector3(Random.Range(-posXRange, posXRange), transform.position.y, parentZPositionOffset);
        // }
        // if(posZRange > 0f)
        // {
        //     spawnedFish.transform.position = new Vector3(transform.position.x, transform.position.y, Random.Range(-posZRange, posZRange));
        // }
    }

    Vector3 SetDirection()
    {
        switch(generalDirection)
        {
            case GeneralDirection.Left:
                return Vector3.left;
            case GeneralDirection.Right:
                return Vector3.right;
            case GeneralDirection.Forward:
                return Vector3.forward;
            case GeneralDirection.Backward:
                return Vector3.back;
            default:
                return Vector3.forward;
        }
    }


}

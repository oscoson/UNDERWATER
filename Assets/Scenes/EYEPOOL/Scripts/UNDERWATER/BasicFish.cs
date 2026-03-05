using UnityEngine;

public class BasicFish : MonoBehaviour // This fish does not serve as a parent class for other fish types, it is simply a fish that moves one way and destroys itself
{
    public Vector3 fishDirection;
    public float speed;
    public bool isCaught;
    private float destroyTime = 15f;
    void Start()
    {
        speed = Random.Range(5f, 10f);
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.2f, 0.2f, 0.2f);
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0f && !isCaught)        
        {
            Destroy(gameObject);
        }
        if(isCaught && speed > 0)
        {
            speed -= Time.deltaTime * 6.5f;
        }
        transform.position += fishDirection * speed * Time.deltaTime;
    }
}

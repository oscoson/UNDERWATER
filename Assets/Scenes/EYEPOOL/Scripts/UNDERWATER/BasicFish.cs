using UnityEngine;

public class BasicFish : MonoBehaviour // This fish does not serve as a parent class for other fish types, it is simply a fish that moves one way and destroys itself
{
    public Vector3 fishDirection;
    private float destroyTime = 15f;
    private int speed;
    void Start()
    {
        speed = Random.Range(5, 10);
    }

    void Update()
    {
        destroyTime -= Time.deltaTime;
        if(destroyTime <= 0f)        
        {
            Destroy(gameObject);
        }
        transform.position += fishDirection * speed * Time.deltaTime;
    }
}

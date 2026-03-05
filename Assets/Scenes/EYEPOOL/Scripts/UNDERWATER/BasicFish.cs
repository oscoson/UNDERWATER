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
        gameObject.AddComponent<SphereCollider>().radius = 0.1f;
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
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
            if(gameObject.transform.parent.GetComponent<FishNet>().netType == FishNet.FishnetType.Hook)
            {
                speed -= Time.deltaTime * 15f;
            }
            else
            {
                speed -= Time.deltaTime * 7f;   
            }
        }
        if(speed < 0)
        {
            speed = 0;;
        }
        transform.position += fishDirection * speed * Time.deltaTime;
    }
}

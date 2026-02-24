
using UnityEngine;

public class Fish : MonoBehaviour
{

    public enum FishState
    {
        Idle,
        Moving,
        Planning,
        Captured,
        Fleeing
    }

    public FishState state = FishState.Idle;
    private AugmentaPickup personAttached;
    private float hoverDelayValue;
    private float idleCountdown = 0.5f;
    private int spawnRange = 14;
    private float yPos = -0.25f;
    private float hoverDelay = 0.1f;
    [SerializeField] private Vector3 newPos;
    [SerializeField] private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hoverDelayValue = hoverDelay;
        speed = Random.Range(4, 8);
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case FishState.Idle:
                speed = Random.Range(4, 8);
                idleCountdown -= Time.deltaTime;
                if (idleCountdown <= 0f)
                {
                    state = FishState.Planning;
                }
                break;
            case FishState.Planning:
                newPos = new Vector3(Random.Range(-spawnRange, spawnRange), yPos, Random.Range(-spawnRange, spawnRange));
                state = FishState.Moving;
                break;
            case FishState.Moving:
                // Handled In FixedUpdate
                break;
            case FishState.Captured:
                break;
            case FishState.Fleeing:
                Detach();
                break;
        }
    }

    void FixedUpdate()
    {
        switch(state)
        {
            case FishState.Idle:
                break;
            case FishState.Planning:
                break;
            case FishState.Moving:
                if (newPos != transform.position)
                {
                    Vector3 pos = Vector3.MoveTowards(transform.position, newPos, speed * Time.fixedDeltaTime);
                    gameObject.GetComponent<Rigidbody>().MovePosition(pos);
                    transform.LookAt(newPos);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
                }
                else
                {
                    idleCountdown = Random.Range(0.1f, 0.3f);
                    state = FishState.Idle;
                }
                break;
            case FishState.Captured:
                if(personAttached == null)
                {
                    state = FishState.Idle;
                    break;
                }
                else if(transform.position != personAttached.transform.position)
                {
                    speed = 25;
                    if(hoverDelayValue > 0f)
                    {
                        hoverDelayValue -= Time.fixedDeltaTime;
                        break;
                    }
                    // Deactivate Hover Animation State Line
                    Vector3 targetPos = Vector3.MoveTowards(transform.position, personAttached.transform.position, speed * Time.fixedDeltaTime);
                    gameObject.GetComponent<Rigidbody>().MovePosition(targetPos);
                    transform.LookAt(personAttached.transform);
                    transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
                }
                else if(transform.position == personAttached.transform.position && hoverDelayValue <= 0f)
                {
                    hoverDelayValue = hoverDelay;
                    speed = 4;
                    // Activate Hover Animation State Line
                }
                break;
            case FishState.Fleeing:
                break;
        }
    }

    public void AttachTo(Transform parent)
    {
        if (state == FishState.Captured) return;
        personAttached = parent.GetComponent<AugmentaPickup>();
        state = FishState.Captured;

    }

    public void Detach()
    {
        // flees after 20 seconds of being captured
    }


}

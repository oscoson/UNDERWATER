using UnityEngine;

public class Garbage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<AugmentaPickup>() != null)
        {
            FindAnyObjectByType<GarbageSpawner>().currentPopulation--;
            FindAnyObjectByType<AudioManager>().GarbagePickupSound();
            // Play Destroy Animation Here
            Destroy(gameObject, 0.1f); // Once animation is done, destroy the garbage object (tweak once animation is done)
        }
    }
}

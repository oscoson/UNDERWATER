using UnityEngine;

public class DestroyIn : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}

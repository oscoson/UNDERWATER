using UnityEngine;
using Augmenta;
using System.Collections.Generic;
public class IlluminationTrigger : MonoBehaviour
{
    public HashSet<AugmentaObject> usersInZone = new();


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (usersInZone.Count > 0)
        {
            // Increase brightness of the underwater environment
            //Debug.Log("Illumination Trigger: " + usersInZone.Count);
        }
        else
        {
            // Decrease brightness of the underwater environment
        }
    }
    void OnTriggerEnter(Collider other)
    {
        AugmentaObject augmenta = other.GetComponent<AugmentaObject>();
        if (augmenta == null)
        {
            return;
        }
        else
        {
            
            usersInZone.Add(augmenta);
            if(usersInZone.Count == 1)
            {
                //Debug.Log("Illumination Trigger: " + usersInZone.Count);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        AugmentaObject augmenta = other.GetComponent<AugmentaObject>();
        if (augmenta == null)
        {
            return;
        }
        else
        {
            usersInZone.Remove(augmenta);
        }
    }
}

using UnityEngine;
using Augmenta;
using System.Collections.Generic;


public class FishNetTrigger : MonoBehaviour
{
    public HashSet<AugmentaObject> usersInZone = new();
    private FishNet fishNetScript;

    void Awake()
    {
        fishNetScript = FindAnyObjectByType<FishNet>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<AugmentaObject>() != null && usersInZone.Count == 0)
        {
            AugmentaObject augmenta = other.gameObject.GetComponent<AugmentaObject>();
            usersInZone.Add(augmenta);   
        }
        if(usersInZone.Count == 1 && fishNetScript.netState == FishNet.FishNetState.Undeployed)
        {
            fishNetScript.DeployNet();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<AugmentaObject>() != null)
        {
            AugmentaObject augmenta = other.gameObject.GetComponent<AugmentaObject>();
            usersInZone.Remove(augmenta);
        }
        if(usersInZone.Count == 0 && fishNetScript.netState == FishNet.FishNetState.Deployed)
        {
            fishNetScript.RetractNet();
        }
    }
}

using UnityEngine;
using Augmenta;
using System.Collections.Generic;


public class FishNetTrigger : MonoBehaviour
{
    public FishNet fishNetScript;
    private AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(fishNetScript.netState == FishNet.FishNetState.Undeployed && fishNetScript.destroyFishTimer <= 0)
        {
            if(fishNetScript.netType == FishNet.FishnetType.Net)
            {
                audioManager.Play("Net-Hook_CAST");
            }
            else if(fishNetScript.netType == FishNet.FishnetType.Hook)
            {
                audioManager.Play("Net-Hook_YANK");
            }
            fishNetScript.DeployNet();
        }
    }
}

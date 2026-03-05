
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FishNet : MonoBehaviour
{
    public enum FishNetState
    {
        Undeployed,
        Deployed,
    }
    public FishNetState netState;
    [SerializeField] private int fishCaught;
    [SerializeField] private int maxFishToCatch;
    private Animator animator;
    public bool playingAnimation;
    private List<GameObject> caughtFishObjects = new List<GameObject>();
    private float destroyFishDelay = 2f;
    private float destroyFishTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        destroyFishTimer = destroyFishDelay;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(caughtFishObjects != null && netState == FishNetState.Undeployed && destroyFishTimer <= 0)
        {
            DestroyFish();
            destroyFishTimer = destroyFishDelay;
        }
        if(destroyFishTimer > 0 && netState == FishNetState.Undeployed)
        {
            destroyFishTimer -= Time.deltaTime;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("FishNet Triggered");
        if(other.gameObject.GetComponent<BasicFish>() != null && fishCaught < maxFishToCatch && netState == FishNetState.Deployed)
        {
            Debug.Log("Fish Caught");
            caughtFishObjects.Add(other.gameObject);
            fishCaught++;
            BasicFish fish = other.gameObject.GetComponent<BasicFish>();
            fish.isCaught = true;
            other.gameObject.transform.SetParent(transform);
        }

    }

    public void DeployNet()
    {
        if(playingAnimation)
        {
            return;
        }
        else
        {
            animator.SetTrigger("Deploying");
            StartCoroutine(WaitForAnimationToEnd(false));
        }

    }

    public void RetractNet()
    {
        if(playingAnimation)
        {
            return;
        }
        else
        {
            animator.SetTrigger("Undeploying");
            StartCoroutine(WaitForAnimationToEnd(true));
        }
    }

    private void DestroyFish()
    {
        if(caughtFishObjects.Count() == 0)
        {
            return;
        }
        else
        {
            foreach (GameObject fish in caughtFishObjects)
            {
                if(fish != null)
                {
                    Destroy(fish);
                }
            }  
        }
    }

    IEnumerator WaitForAnimationToEnd(bool isEnding)
    {
        playingAnimation = true;
        yield return new WaitForSeconds(1f);
        if (isEnding)
        {
            netState = FishNetState.Undeployed;
            fishCaught = 0;
            playingAnimation = false;
        }
        else
        {
            netState = FishNetState.Deployed;
            playingAnimation = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FishNet : MonoBehaviour
{
    public enum FishnetType
    {
        Net,
        Hook
    }
    public enum FishNetState
    {
        Undeployed,
        Deployed,
    }
    public FishNetState netState;
    public FishnetType netType;
    private Animator animator;
    public bool playingAnimation;
    public float destroyFishTimer = 0;
    [SerializeField] private float retractTimer = 0;
    private List<GameObject> caughtFishObjects = new List<GameObject>();
    private float destroyFishDelay = 1.75f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(caughtFishObjects != null && netState == FishNetState.Undeployed && destroyFishTimer < 0)
        {
            destroyFishTimer = 0;
            DestroyFish();
        }
        else if(destroyFishTimer > 0 && netState == FishNetState.Undeployed)
        {
            destroyFishTimer -= Time.deltaTime;
        }
        
        if(netState == FishNetState.Deployed && retractTimer <= 0)
        {
            RetractNet();
        }
        if(netState == FishNetState.Deployed)
        {
            retractTimer -= Time.deltaTime;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<BasicFish>() != null && netState == FishNetState.Deployed)
        {
            caughtFishObjects.Add(other.gameObject);
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
            DecideAnimation();
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
            DecideAnimation();  //undeploying
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

    private void DecideAnimation()
    {
        if(netType == FishnetType.Net)
        {
            if(netState == FishNetState.Undeployed)
            {
                animator.SetTrigger("Deploying");
            }
            else
            {
                animator.SetTrigger("Undeploying");
            }
        }
        else if(netType == FishnetType.Hook)
        {
            if(netState == FishNetState.Undeployed)
            {
                animator.SetTrigger("HookDeploying");
            }
            else
            {
                animator.SetTrigger("HookUndeploying");
            }
        }
    }

    IEnumerator WaitForAnimationToEnd(bool isEnding)
    {
        playingAnimation = true;
        FindAnyObjectByType<AudioManager>().Play("Net-Hook_TRIGGER");
        yield return new WaitForSeconds(1f);
        if (isEnding)
        {
            netState = FishNetState.Undeployed;
            destroyFishTimer = destroyFishDelay;
            playingAnimation = false;
        }
        else
        {
            retractTimer = Random.Range(5f, 15f);
            netState = FishNetState.Deployed;
            playingAnimation = false;
        }
    }
}

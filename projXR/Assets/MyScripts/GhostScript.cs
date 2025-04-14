using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostScript : MonoBehaviour
{
    public float eatDistance = 0.2f;
    public Animator animator;
    public NavMeshAgent agent;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject getClosestOrb(){
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        List<GameObject> orbs = OrbSpawner.instance.spawnedOrbs;

        foreach(GameObject item in orbs){
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0;

            Vector3 orbPosition = item.transform.position;
            orbPosition.y = 0;

            float d = Vector3.Distance(transform.position, item.transform.position);
            if(d < minDistance){
                minDistance = d;
                closest = item;
            }
        }

        if(minDistance < eatDistance){
            OrbSpawner.instance.DestroyOrb(closest);
        }

        return closest;
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled) return;

        GameObject closest = getClosestOrb();

        if(closest){
            Vector3 targetPosition = closest.transform.position;
            agent.SetDestination(targetPosition);
            agent.speed = speed;
        }
    }

    public void Kill(){
        agent.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}

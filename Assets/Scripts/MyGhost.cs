using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyGhost : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    public GameObject GetClosestOrb()
    {
        GameObject closestOrb = null;
        float closestDistance = Mathf.Infinity;

        List<GameObject> spawnedOrbs = OrbSpawner.Instance.spawnedOrbs;

        foreach (var orb in spawnedOrbs)
        {
            float distance = Vector3.Distance(transform.position, orb.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestOrb = orb;
            }
        }
        return closestOrb;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled)
        {
            return;
        }

        GameObject closestOrb = GetClosestOrb();
        if (closestOrb)
        {
            Debug.Log("Closest Orb: " + closestOrb.name);
            Vector3 targetPosition = closestOrb.transform.position;

            agent.SetDestination(targetPosition);
            agent.speed = speed;
        }

    }

    public void Kill()
    {
        // Implement the logic to handle ghost death
        Debug.Log("Ghost killed: " + gameObject.name);

        agent.enabled = false; // Disable the NavMeshAgent
        animator.SetTrigger("Death"); // Trigger death animation if available

     }

    public void Destroy()
    {
        Debug.Log("Destroying ghost: " + gameObject.name);
        Destroy(gameObject);
    }
}

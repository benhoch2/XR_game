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

    // Update is called once per frame
    void Update()
    {
        if (!agent.enabled)
        {
            return;
        }

        Vector3 targetPosition = Camera.main.transform.position;

        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }

    public void Kill()
    {
        Debug.Log("Ghost killed: " + gameObject.name);

        agent.enabled = false;
        animator.SetTrigger("Death");

        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Assumes "Death" is the name of the death animation state
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Wait until the animator is in the "Death" state
        while (!stateInfo.IsName("Death"))
        {
            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
        // Wait for the animation to finish
        yield return new WaitForSeconds(stateInfo.length);
        Destroy(gameObject);
    }
}

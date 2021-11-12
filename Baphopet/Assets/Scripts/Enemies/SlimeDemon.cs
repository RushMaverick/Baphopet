using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDemon : Enemy
{
    public Transform target;
    public float escapeRadius;
    public float safeRadius; 
    public float moveSpeed;
    public Transform homePosition; 
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("Target is " + target.name);
    }

   
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= escapeRadius && Vector3.Distance(target.position, transform.position) > safeRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, - 1 * moveSpeed * Time.deltaTime);
        }


    }

       void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, escapeRadius);
        Gizmos.DrawWireSphere(transform.position, safeRadius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDemon : Enemy
{
    public Transform target;
    public float escapeRadius;
    public float safeRadius; 
    public float moveSpeed;
    private bool isMoving;
    public Transform homePosition; 
    Vector3 PreviousPosition;
    Vector3 CurrentMoveDirection;

     //Update is called once per frame
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log("Target is " + target.name);
    }

   
    private void Update()
    {
        //Checks position of enemy, currently unused. PreviousPosition can be used as transform.position, CurrentMoveDirection checks where enemy is moving, similiar to Player.input.
        if (PreviousPosition != transform.position)
            {   
                CurrentMoveDirection = (PreviousPosition - transform.position).normalized;
                PreviousPosition = transform.position;
                Debug.Log("Demon is" + CurrentMoveDirection);
            }

        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= escapeRadius && Vector3.Distance(target.position, PreviousPosition) > safeRadius)
        {
            if ((target.position - PreviousPosition).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(PreviousPosition, target.position, - 1 * moveSpeed * Time.deltaTime);
                Debug.Log("Grid Movement here");
            }
        }
    }

       void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, escapeRadius);
        Gizmos.DrawWireSphere(transform.position, safeRadius);
    }
}



/*       
        CheckDistance();*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDemon : Enemy
{
    public Transform target;
    public float escapeRadius;
    public float safeRadius;
    public float moveSpeed;
    public bool isMoving;
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
        //Checks position of enemy to be used by enemy, currently unused. PreviousPosition can be used as transform.position, CurrentMoveDirection checks where enemy is moving, similiar to Player.input.
        if (PreviousPosition != transform.position)
        {
            CurrentMoveDirection = (PreviousPosition - transform.position).normalized;
            PreviousPosition = transform.position;
            //TODO Debug.Log in update is too resource heavy
            //Debug.Log("Demon is" + CurrentMoveDirection);
        }

        CheckDistance();

        if (!isMoving)
        {
            // /*Moves the enemy in one direction*/StartCoroutine(Move(PreviousPosition));
        }

    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= escapeRadius && Vector3.Distance(target.position, PreviousPosition) > safeRadius)
        {
            if ((target.position - PreviousPosition).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(PreviousPosition, target.position, -1 * moveSpeed * Time.deltaTime);
            }
        }
    }

    public IEnumerator Move(Vector2 moveVec)
    {

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        isMoving = true;

        if (moveVec.x != 0) moveVec.y = 0;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, escapeRadius);
        Gizmos.DrawWireSphere(transform.position, safeRadius);
    }
}







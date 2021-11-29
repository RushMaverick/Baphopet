using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritEnemy : MonoBehaviour
{
    private Vector3 directionVector;
    private Transform myTransform;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    public Collider2D bounds;
    private bool isMoving;
    private bool playerInRange;
    public float minMoveTime;
    public float maxMoveTime;
    private float moveTimeSeconds;
    public float minWaitTime;
    public float maxWaitTime;
    private float waitTimeSeconds;
    public float escapeRadius;
    public float safeRadius; 
    public float moveSpeed;
    public Transform target;
    Vector3 PreviousPosition;
    Vector3 CurrentMoveDirection;

    void Start()
    {
        moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    public void Update()
    {
        //Checks position of enemy to be used by enemy. PreviousPosition can be used as transform.position, CurrentMoveDirection checks where enemy is moving, similiar to Player.input.
        if (PreviousPosition != transform.position)
        {   
            CurrentMoveDirection = (PreviousPosition - transform.position).normalized;
            PreviousPosition = transform.position;
            Debug.Log("Demon is" + CurrentMoveDirection);
        }
        //If Player is near, trigger Escape().
        if(Vector3.Distance(target.position, transform.position) <= escapeRadius && Vector3.Distance(target.position, PreviousPosition) > safeRadius)
        {
            playerInRange = true;
            StartCoroutine(Escape());
        }
        else
        {
            playerInRange = false;
        }

        if(isMoving)
        {
            moveTimeSeconds -= Time.deltaTime;
            if(moveTimeSeconds<= 0)
            {
                moveTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
                isMoving = false;
            }
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeSeconds -= Time.deltaTime;
            if(waitTimeSeconds <= 0)
            {
                ChooseDifferentDirection();
                isMoving = true;
                waitTimeSeconds = Random.Range(minMoveTime, maxMoveTime);
            }
        }
    }

    private void ChooseDifferentDirection()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * moveSpeed * Time.deltaTime;
        //Determine an object the enemy can move within, in this case: Bounds in Grid, selected by the field Bounds in the inspector.
        if (bounds.bounds.Contains(temp))
        {
            myRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
    }

        public IEnumerator Escape()
    {
        Vector3 runTemp = Vector3.MoveTowards(PreviousPosition, target.position, - 5 * moveSpeed * Time.deltaTime);

        if ((target.position - PreviousPosition).sqrMagnitude > Mathf.Epsilon)
        {
            //transform.position = Vector3.MoveTowards(PreviousPosition, target.position, - 1 * moveSpeed * Time.deltaTime);
            Debug.Log("RUN");
            myRigidbody.MovePosition(runTemp);
            yield return null; 
        }
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch(direction)
        {
            case 0:
                // Walking to the right
                directionVector = Vector3.right;
                break;
            case 1:
                // Walking up
                directionVector = Vector3.up;
                break;
            case 2:
                // Walking Left
                directionVector = Vector3.left;
                break;
            case 3:
                // Walking down
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("MoveX", directionVector.x);
        anim.SetFloat("MoveY", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }
}

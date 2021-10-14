using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }
}

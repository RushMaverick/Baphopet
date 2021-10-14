using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GridUpdate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        AstarPath.active.UpdateGraphs(PlayerObject.collider.bounds);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public bool isRunningBack = false;

    private int currentWaypointIndex = 0;
    private bool isMovingForward = true;

    private void Update()
    {
        if (waypoints.Length < 2) return;

        MovePlatform();
    }

    private void MovePlatform()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            UpdateNextWaypoint();
        }
    }

    private void UpdateNextWaypoint()
    {
        if (isRunningBack)
        {
            if (currentWaypointIndex == waypoints.Length - 1)
            {
                isMovingForward = false;
            }
            else if (currentWaypointIndex == 0)
            {
                isMovingForward = true;
            }

            currentWaypointIndex += isMovingForward ? 1 : -1;
        }
        else
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}

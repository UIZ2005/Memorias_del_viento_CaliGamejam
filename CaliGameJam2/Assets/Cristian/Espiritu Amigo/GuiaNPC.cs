using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiaNPC : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 3f; // Distancia para empezar a guiar
    public Transform[] waypoints;         // Puntos de la ruta
    public float speed = 2f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceToPlayer < activationDistance)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        if (currentWaypointIndex >= waypoints.Length)
            return; // Llegó al final

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}

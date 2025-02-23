using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviorPatrolWaypoints 
{
    Transform WaypointPatrolLeft { get; }
    Transform WaypointPatrolRight { get; }

    void Init(Rigidbody2D rd, Vector2 direction, float acceleration, float speedMax);

    void TickPhysics();
}

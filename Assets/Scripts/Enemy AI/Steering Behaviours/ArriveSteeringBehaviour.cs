using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveSteeringBehaviour : SeekSteeringBehaviour
{
	public float slowDownDistance = 5.0f;
	public float deceleration = 2.5f;
	public float stoppingDistance = 0.5f;

	public override Vector2 calculateForce()
	{
		checkMouseInput();

		Vector2 toTarget = target - new Vector2(transform.parent.position.x, transform.parent.position.y);
		float distanceToTarget = toTarget.magnitude;

		steeringAgent.reachedGoal = false;
		if (distanceToTarget > slowDownDistance)
		{
			return base.calculateForce();
		}
		else if (distanceToTarget > stoppingDistance)
		{
			float speed = distanceToTarget / deceleration;
			if (speed > steeringAgent.maxSpeed)
			{
				speed = steeringAgent.maxSpeed;
			}

			speed = speed / distanceToTarget;
			Vector2 desiredVelocity = toTarget.normalized * speed;
			return desiredVelocity - steeringAgent.velocity;
		}

		steeringAgent.reachedGoal = true;
		return Vector3.zero;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeSteeringBehaviour : SteeringBehaviourBase
{
	public float fleeDistance = 5.0f;
	public Transform enemyTarget;

	public override Vector2 calculateForce()
	{
		if (enemyTarget == null)
		{
			checkMouseInput();
		}
		else
		{
			target = enemyTarget.position;
		}

		// check the distance return zero if we are far away
		float distance = (target - new Vector2(transform.parent.position.x, transform.parent.position.y)).magnitude;
		if (distance > fleeDistance)
		{
			return Vector3.zero;
		}

		Vector2 desiredVelocity = (new Vector2(transform.parent.position.x,transform.parent.position.y) - target).normalized;
		desiredVelocity = desiredVelocity * steeringAgent.maxSpeed;
		return desiredVelocity - steeringAgent.velocity;
	}

	private void OnDrawGizmos()
	{
		DebugExtension.DrawCircle(transform.parent.position, fleeDistance);
		DebugExtension.DebugWireSphere(target, Color.red);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekSteeringBehaviour : SteeringBehaviourBase
{
	public Transform m_playerTarget;
	public override Vector2 calculateForce()
	{
		//checkMouseInput();
		if(m_playerTarget != null)
        {
			target = m_playerTarget.position;
        }

		Vector2 desiredVelocity = (target - new Vector2(transform.parent.position.x, transform.parent.position.y)).normalized;
		desiredVelocity = desiredVelocity * steeringAgent.maxSpeed;
		return desiredVelocity - steeringAgent.velocity;
	}
}

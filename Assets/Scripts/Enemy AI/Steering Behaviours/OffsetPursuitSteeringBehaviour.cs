using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursuitSteeringBehaviour : ArriveSteeringBehaviour
{
	public SteeringAgent pursuitAgent;
	public Vector3 offset;

	public override Vector2 calculateForce()
	{
		if (pursuitAgent != null)
		{
			Vector2 worldOffsetPos = (pursuitAgent.transform.rotation * offset) +
									  pursuitAgent.transform.position;

			Vector2 toOffset = worldOffsetPos - new Vector2(transform.position.x, transform.position.y);
			float lookAheadTime = toOffset.magnitude / (steeringAgent.maxSpeed + pursuitAgent.velocity.magnitude);

			target = (worldOffsetPos + pursuitAgent.velocity * lookAheadTime);

			return base.calculateForce();
		}

		return Vector3.zero;
	}
}

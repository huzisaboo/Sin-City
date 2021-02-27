using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollowSteeringBehaviour : ArriveSteeringBehaviour
{
	public Transform PathTarget;
	public Transform PathTarget2;
	public float WaypointSeekDist = 0.5f;
	public bool loop = false;

	public int currentWaypointIndex = 0;

	private NavMeshPath path;

    void Start()
    {
		path = new NavMeshPath();
		NavMesh.CalculatePath(transform.position, PathTarget.position, NavMesh.AllAreas, path);
		if (path != null && path.corners.Length > 0)
		{
			target = path.corners[0];
		}
    }

	 void Update()
	{
		if(steeringAgent.reachedGoal)
		{

			NavMesh.CalculatePath(transform.position, PathTarget2.position, NavMesh.AllAreas, path);
			Debug.Log("Changing paths");
			if(path !=null && path.corners.Length > 0)
			{
				currentWaypointIndex = 0;
				target = path.corners[currentWaypointIndex];
			}
				
		}
	}


	public override Vector2 calculateForce()
	{
		if (currentWaypointIndex >= path.corners.Length - 1)
		{
			return base.calculateForce();
		}
		else if ((path.corners[currentWaypointIndex] - transform.parent.position).magnitude < WaypointSeekDist)
		{
			currentWaypointIndex++;
			target = path.corners[currentWaypointIndex];
		}
		return base.calculateForce();
	}

	private void OnDrawGizmos()
	{
		if (path != null)
		{
			for(int i = 1; i < path.corners.Length; i++)
			{
				DebugExtension.DebugWireSphere(path.corners[i - 1], Color.blue, 0.5f);
				Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.blue);
			}
		}
	}

}

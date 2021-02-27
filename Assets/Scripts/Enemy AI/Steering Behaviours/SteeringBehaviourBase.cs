using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehaviourBase : MonoBehaviour
{
	public float weight = 1.0f;
	public Vector2 target = Vector2.zero;
	public bool useMouseInput = true;

	public abstract Vector2 calculateForce();

	[HideInInspector] public SteeringAgent steeringAgent;

	protected void checkMouseInput()
	{
		if (Input.GetMouseButton(0) && useMouseInput)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000))
			{
				target = hit.point;
			}
		}
	}

}

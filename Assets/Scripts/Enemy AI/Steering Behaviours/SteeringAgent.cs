using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
	public enum SummingMethod
	{
		WeightedAverage,
		Prioritized,
		Dithered
	};
	public SummingMethod summingMethod = SummingMethod.WeightedAverage;

	public float mass = 1.0f;
	public float maxSpeed = 1.0f;
	public float maxForce = 10.0f;

	public Vector2 velocity = Vector2.zero;
	private SpriteRenderer spriteRenderer;

	private List<SteeringBehaviourBase> steeringBehaviours = new List<SteeringBehaviourBase>();

	public float angularDampeningTime = 5.0f;
	public float deadZone = 10.0f;

	[HideInInspector] public bool reachedGoal = false;

	private Animator animator;
	private Transform m_target;
	private void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		steeringBehaviours.AddRange(GetComponentsInChildren<SteeringBehaviourBase>());
		foreach(SteeringBehaviourBase behaviour in steeringBehaviours)
		{
			behaviour.steeringAgent = this;
		}
	}

	//private void OnAnimatorMove()
	//{
	//	if (Time.deltaTime != 0.0f)
	//	{
	//		Vector3 animationVelocity = animator.deltaPosition / Time.deltaTime;
	//		transform.position += (transform.right * animationVelocity.magnitude) * Time.deltaTime;
			
	//	}
	//}

	private void Update()
	{
		Vector2 steeringForce = calculateSteeringForce();
		//steeringForce.y = 0.0f;

		if (reachedGoal == true)
		{
			velocity = Vector2.zero;
			animator.SetFloat("Speed", 0.0f);
		}
		else
		{
			//Vector3 acceleration = steeringForce * (1.0f / mass);
			velocity = velocity + (steeringForce * Time.deltaTime);
			velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

			float speed = velocity.magnitude;
			animator.SetFloat("Speed", speed);
			Vector2 tempVector = (velocity * Time.deltaTime);
			transform.position += new Vector3(tempVector.x, tempVector.y, 0);

			if(Mathf.Round(Vector2.Dot(transform.right, velocity)) < 0)
            {
				if(!spriteRenderer.flipX)
                {
					spriteRenderer.flipX = true;
                }
            }
			else if(Mathf.Round(Vector2.Dot(transform.right, velocity)) > 0)
            {
				if(spriteRenderer.flipX)
                {
					spriteRenderer.flipX = false;
                }
            }

		}

		//if (velocity.magnitude > 0.0f)
		//{
		//	float angle = Vector3.Angle(transform.forward, velocity);
		//	if (Mathf.Abs(angle) <= deadZone)
		//	{
		//		transform.LookAt(transform.position + velocity);
		//	}
		//	else
		//	{
		//		transform.rotation = Quaternion.Slerp(transform.rotation,
		//											  Quaternion.LookRotation(velocity),
		//											  Time.deltaTime * angularDampeningTime);
		//	}
		//}
	}

	private Vector3 calculateSteeringForce()
	{
		Vector2 totalForce = Vector2.zero;

		foreach(SteeringBehaviourBase behaviour in steeringBehaviours)
		{
			if (behaviour.enabled)
			{
				switch(summingMethod)
				{
					case SummingMethod.WeightedAverage:
						totalForce = totalForce + (behaviour.calculateForce() * behaviour.weight);
						//totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
						break;

					case SummingMethod.Prioritized:
						break;
				}

			}
		}

		return totalForce;
	}

	public void SetTarget(Transform p_target)
    {
		m_target = p_target;
    }

	public Transform GetTarget()
    {
		return m_target;
    }
}

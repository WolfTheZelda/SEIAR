using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorByGravity : MonoBehaviour {

	const float G = 667.4f;

	public static List<AttractorByGravity> Attractors;

	public Rigidbody RigidbodyComponent;

	void FixedUpdate ()
	{
		foreach (AttractorByGravity Attractor in Attractors)
		{
			if (Attractor != this)
				Attract(Attractor);
		}
	}

	void OnEnable ()
	{
		if (Attractors == null)
			Attractors = new List<AttractorByGravity>();

		Attractors.Add(this);
	}

	void OnDisable ()
	{
		Attractors.Remove(this);
	}

	void Attract (AttractorByGravity ObjectToAttract)
	{
		Rigidbody RigidbodyToAttract = ObjectToAttract.RigidbodyComponent;

		Vector3 Direction = RigidbodyComponent.position - RigidbodyToAttract.position;
		float Distance = Direction.magnitude;

		if (Distance == 0f)
			return;

		float ForceMagnitude = G * (RigidbodyComponent.mass * RigidbodyToAttract.mass) / Mathf.Pow(Distance, 2);
		Vector3 force = Direction.normalized * ForceMagnitude;

		RigidbodyToAttract.AddForce(force);
	}

}

using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float damage;
	[HideInInspector]
	public Collider sourceChar;
	public GameObject particles;            // Prefab of the particle system to play in the state.
	public AvatarIKGoal attackLimb;

	public float forceMultiplier = 10;
	public Vector3 HitAngle;

	void OnTriggerEnter(Collider other)
	{
		if (other != sourceChar) 
		{

			Vector3 HitAngleNew = HitAngle.normalized;
			if(other.transform.position.z < transform.position.z)
				HitAngleNew = new Vector3(0, HitAngleNew.y, HitAngleNew.z * -1);

			Character character = other.GetComponent<Character>();
            if (character != null)
                character.Attacked(this);
            else
                other.GetComponent<Thing>().Attacked(this);
			other.attachedRigidbody.AddForce(HitAngleNew * forceMultiplier, ForceMode.Impulse);
		}
	}
}



using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float damage;
	[HideInInspector]
	public Collider sourceChar;
	public GameObject particles;            // Prefab of the particle system to play in the state.
	public AvatarIKGoal attackLimb;

	void OnTriggerEnter(Collider other)
	{
		if (other != sourceChar) 
		{
			Debug.Log("Hit! " + other.name);

			Character character = other.GetComponent<Character>();
			if(character != null)
				character.Attacked(this);
			else
				other.attachedRigidbody.AddForce((other.transform.position - transform.position) * damage * 1000);
		}
	}
}



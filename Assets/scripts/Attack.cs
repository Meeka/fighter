using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float damage;
	public Collider sourceChar;
	

	void OnTriggerEnter(Collider other)
	{
		if (other != sourceChar) 
		{
			Debug.Log("Hit! " + other.name);
			other.attachedRigidbody.AddForce((other.transform.position - transform.position) * damage * 1000);
		}
	}
}



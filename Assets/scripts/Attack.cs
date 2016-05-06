using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public float damage;
	//public Anima charAnima
	public Collider sourceChar;

	public Attack ()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if(other != sourceChar)
			Debug.Log ("Hit! " + other.name);
	}
}



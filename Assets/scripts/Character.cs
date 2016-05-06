using UnityEngine;
using System.Collections;

public class testCharacter : MonoBehaviour {

	public Rigidbody body;

	public float HP;
	//controls controls
	Attack currentAttack;

	Animation idle;
	Animation dead;

	public Attack Normal1;
	public Attack Normal2;
	public Attack Special1;
	public Attack Special2;

	// Use this for initialization
	void Start () {
		/* controls.move += Move; */
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Random.value);
		if (Random.value < 0.01)
			body.AddForce (new Vector3 (Random.value - 0.5f, Random.value -0.5f, Random.value-0.5f) * 500);
	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;

	}


}

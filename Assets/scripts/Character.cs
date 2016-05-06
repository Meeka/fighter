using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Rigidbody body;

	public float HP;
	controls controls;
	Attack currentAttack;

	public Animator animator;

	Animation idle;
	Animation dead;

	public Attack Normal1;
	public Attack Normal2;
	public Attack Special1;
	public Attack Special2;

	// Use this for initialization
	void Start () {
		controls = FindObjectOfType<controls> ();
		controls.onHorizontalMov += HorizontalMove; 
		controls.onVerticalMov += VerticalMove; 
		controls.onHeavyAttack += HeavyAttack;
		controls.onLightAttack += LightAttack;
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.value < 0.01)
			body.AddForce (new Vector3 (Random.value - 0.5f, Random.value -0.5f, Random.value-0.5f) * 500);
	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;

	}
	
	void HorizontalMove(float amount){}
	void VerticalMove(float amount){}
	void HeavyAttack()
	{
		currentAttack = Normal1;
		animator.SetTrigger ("HeavyAttack");
		//currentAttack.charAnimation.Play ("play");
	}
	void LightAttack(){}

}

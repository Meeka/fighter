using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Rigidbody body;

	public float HP;
	public controls controls;
	Attack currentAttack;

	public Animator animator;

	float horizontalMoveAmount;

	public float moveSpeed;
	public float jumpHeight;

	public Attack LightAttack;
	public Attack HeavyAttack;

	public Transform rightHand;
	public Transform leftHand;

	bool airborne;
	bool block;
	bool blockDelay;

	behavior behavior;

	// Use this for initialization
	void Start () {
		behavior = animator.GetBehaviour<behavior> ();

		controls = FindObjectOfType<controls> ();
		controls.onHorizontalMov += HorizontalMove; 
		controls.onVerticalMov += VerticalMove; 
		controls.onHeavyAttack += DoHeavyAttack;
		controls.onLightAttack += DoLightAttack;
		controls.onJump += Jump;
		controls.onBlock += Block;
	}
	
	// Update is called once per frame
	void Update () {

		airborne = !Physics.Raycast (new Ray (transform.position, Vector3.down), 1);

		if (!blockDelay)
			block = false;

		blockDelay = false;

		animator.SetFloat ("forwardSpeed", horizontalMoveAmount);

		//body.isKinematic = !airborne;
	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;

	}
	
	void HorizontalMove(float amount)
	{
		horizontalMoveAmount = amount;

		if(!airborne)
			transform.position += transform.forward * amount * moveSpeed;
	}
	void VerticalMove(float amount)
	{
		if (!airborne) {
			transform.position += transform.right * amount * moveSpeed;

		}
	}
	void DoHeavyAttack()
	{
		animator.SetTrigger ("heavyAttack");
		behavior.SetAttack (HeavyAttack);

	}
	void DoLightAttack()
	{
		animator.SetTrigger ("lightAttack");
		behavior.SetAttack (LightAttack);
	}

	void HopForwards()
	{
	}

	void HopBackwards()
	{
	}

	void Jump()
	{
		if (!airborne) 
		{
			body.AddForce (Vector3.up * jumpHeight);
			animator.SetTrigger("jump");
		}
	}

	void Block()
	{
		block = true; 
		blockDelay = true;
	}
	
	public void createAttack()
	{
		behavior.createAttack (GetComponent<Collider>());
	}
	
	public void destroyAttack()
	{
		behavior.destroyAttack ();
	}
}

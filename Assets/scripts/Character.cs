using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Rigidbody body;

	public float HP;
	public controls controls;
	Attack currentAttack;

	public Animator animator;

	float horizontalMoveAmount;
	float targetHorizontalVelocity;
	float velocityhorizontalSmoothSpeed;

	public float moveSpeed;
	float walkAnimationFactor = 3;
	public float jumpHeight;

	public Attack LightAttack;
	public Attack HeavyAttack;

	public Transform target;

	public bool airborne;
	bool block;
	bool blockDelay;

	behavior behavior;

	// Use this for initialization
	void Start () {
		behavior = animator.GetBehaviour<behavior> ();

		controls.onHorizontalMov += HorizontalMove; 
		//controls.onVerticalMov += VerticalMove; 
		controls.onHeavyAttack += DoHeavyAttack;
		controls.onLightAttack += DoLightAttack;
		controls.onJump += DoJump;
		controls.onBlock += Block;
		controls.onDash += DoDash;
		
		animator.SetFloat ("walkForwardAnimSpeed", moveSpeed / walkAnimationFactor);
		animator.SetFloat ("walkBackwardsAnimSpeed", -moveSpeed / walkAnimationFactor);
	}
	
	// Update is called once per frame
	void Update () {

		airborne = !Physics.Raycast (new Ray (transform.position + Vector3.up * 0.1f, Vector3.down), 0.11f);

		if (!blockDelay)
			block = false;

		blockDelay = false;

		animator.SetFloat ("forwardSpeed", horizontalMoveAmount);
		animator.SetBool ("airborne", airborne);


		if (airborne)
			animator.ResetTrigger ("jump");

				
		horizontalMoveAmount = Mathf.SmoothDamp (horizontalMoveAmount, targetHorizontalVelocity, ref velocityhorizontalSmoothSpeed, 0.1f);
		body.velocity = transform.forward * horizontalMoveAmount * moveSpeed + body.velocity.y * Vector3.up;
		if(!airborne)
			targetHorizontalVelocity = 0;

		//face target
		Debug.Log (Vector3.Dot (target.position - transform.position, transform.forward));
	}

	public void Attacked(Attack attack)
	{
		Debug.Log ("Im hit!");
	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;
	}
	
	void HorizontalMove(float amount)
	{

		if (!airborne)
			targetHorizontalVelocity = -amount;

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

	void DoDash(float amount)
	{
		targetHorizontalVelocity = - amount * 5;
		horizontalMoveAmount = targetHorizontalVelocity;
		if (amount > 0)
			animator.SetTrigger ("dashBackward");
		else
			animator.SetTrigger ("dashForward");
	}


	void DoJump()
	{
		if (!airborne) 
		{
			animator.SetTrigger("jump");
		}
	}

	void Block()
	{
		block = true; 
		blockDelay = true;
	}

	public void jump()
	{
		body.velocity += transform.up * jumpHeight;
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

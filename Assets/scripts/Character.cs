using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Rigidbody body;
	Collider collider;

	public float HP;
	public float startHP;
	public controls controls;
	Attack currentAttack;

	public Animator animator;

	float horizontalMoveAmount;
	float targetHorizontalVelocity;
	float velocityhorizontalSmoothSpeed;

	public float moveSpeed;
	float walkAnimationFactor = 3;
	public float jumpHeight;
	public float knockbackFactor = 100;

	public Attack LightAttack;
	public Attack HeavyAttack;

	public Transform target;

	public bool airborne;
	bool block;
	bool blockDelay;
	float roty;
	bool leftSide;

	behavior behavior;

	bool IsWalking {
		get {
			return animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkBackwards") ||
				animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkForward");
		}
	}

	// Use this for initialization
	void Start () {
		behavior = animator.GetBehaviour<behavior> ();

		controls.onHorizontalMov += HorizontalMove; 
		controls.onVerticalMov += VerticalMove; 
		controls.onHeavyAttack += DoHeavyAttack;
		controls.onLightAttack += DoLightAttack;
		controls.onJump += DoJump;
		controls.onBlock += Block;
		controls.onDash += DoDash;
		
		animator.SetFloat ("walkForwardAnimSpeed", moveSpeed / walkAnimationFactor);
		animator.SetFloat ("walkBackwardsAnimSpeed", -moveSpeed / walkAnimationFactor);

		roty = transform.eulerAngles.y;

		startHP = HP;
		collider = GetComponent<Collider> (); 
	}


	// Update is called once per frame
	void Update () {
		float offset = 0.6f;
		float length = 0.5f;

		Vector3 origin1 = collider.bounds.center + Vector3.down * collider.bounds.extents.y * 0.8f;
		Vector3 direction1 = Vector3.down * length;

		Vector3 origin2 = collider.bounds.center + Vector3.down * 0.4f + Vector3.forward * collider.bounds.extents.y * 0.4f;
		Vector3 direction2 = (Vector3.down + Vector3.forward * collider.bounds.extents.y * 0.4f)  * length;
		Vector3 origin3 = collider.bounds.center + Vector3.down * 0.4f - Vector3.forward * collider.bounds.extents.y * 0.4f;
		Vector3 direction3 = (Vector3.down - Vector3.forward * collider.bounds.extents.y * 0.4f)  * length;
		
		Vector3 origin4 = collider.bounds.center + Vector3.forward * collider.bounds.extents.y * 0.8f;
		Vector3 direction4 = (Vector3.down + Vector3.forward * collider.bounds.extents.y * 0.2f)  * length;
		Vector3 origin5 = collider.bounds.center - Vector3.forward * collider.bounds.extents.y * 0.8f;
		Vector3 direction5 = (Vector3.down - Vector3.forward * collider.bounds.extents.y * 0.2f)  * length;


		airborne = !Physics.Raycast (new Ray (origin1, direction1), length)
			&& !Physics.Raycast (new Ray (origin2, direction2), length)
				&& !Physics.Raycast (new Ray (origin3, direction3), length)
				&& !Physics.Raycast (new Ray (origin4, direction4), length)
				&& !Physics.Raycast (new Ray (origin5, direction5), length);
		
		Debug.DrawLine(origin1, origin1 + direction1);
		Debug.DrawLine(origin2, origin2 + direction2);
		Debug.DrawLine(origin3, origin3 + direction3);
		Debug.DrawLine(origin4, origin4 + direction4);
		Debug.DrawLine(origin5, origin5 + direction5);

		if (!blockDelay)
			block = false;

		blockDelay = false;

		if(!leftSide)
			animator.SetFloat ("forwardSpeed", body.velocity.z);
			//animator.SetFloat ("forwardSpeed", horizontalMoveAmount);
		else
			animator.SetFloat ("forwardSpeed", -body.velocity.z);
			//animator.SetFloat ("forwardSpeed", -horizontalMoveAmount);

		animator.SetBool ("airborne", airborne);


		if (airborne)
			animator.ResetTrigger ("jump");



		//face target(other player)
		if (HP > 0) {
			leftSide = (target.position - transform.position).z < 0;
			if (leftSide)
				roty = Mathf.Clamp (roty + 5, 0, 180);
			else
				roty = Mathf.Clamp (roty - 5, 0, 180);

			transform.rotation = Quaternion.Euler (0, roty, 0);
		}
		if (HP <= 0)
			animator.SetBool ("dead", true);

	}

	//hit by a object
	void OnCollisionEnter(Collision collision) 
	{
		//other player
		if (collision.gameObject.GetComponent<Character> ())
			return;

		Rigidbody body = collision.rigidbody;

		//if(body != null)
		//	Debug.Log (body.velocity.magnitude);

		if(body == null || body.velocity.magnitude < 4)
		   return;

		float damage = Mathf.Clamp(body.mass * body.velocity.magnitude / 20, 0, 50);
		HP -= damage;

		if(damage <= 20)
			animator.SetTrigger ("lightHit");
		else
			animator.SetTrigger ("heavyHit");
	}
		
	public void Attacked(Attack attack)
	{
		HP = Mathf.Clamp(HP - attack.damage, 0, startHP);

		if(attack.damage <= 20)
			animator.SetTrigger ("lightHit");
		else
			animator.SetTrigger ("heavyHit");
	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;
	}
	
	void HorizontalMove(float amount)
	{
		float h = body.velocity.z;
		h += amount * moveSpeed;
		h = Mathf.Clamp (h, -Mathf.Abs (amount * moveSpeed), Mathf.Abs (amount * moveSpeed));
		if (!airborne && Mathf.Abs(body.velocity.z) < Mathf.Abs(amount * moveSpeed))
			body.velocity = (new Vector3 (0, body.velocity.y, h));
			//targetHorizontalVelocity = amount;

	}
	void VerticalMove(float amount)
	{
		if (!airborne && amount > 0) {
			animator.SetTrigger("jump");
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
		if (!airborne) {
			body.velocity = (new Vector3 (0, body.velocity.y + 3, Mathf.Sign(amount) * moveSpeed * 2));

			if(IsWalking)
			{
				if (amount > 0 != !leftSide )
					animator.SetTrigger ("dashBackward");
				else
					animator.SetTrigger ("dashForward");
			}
		}
	}

	void DoJump()
	{
		if (!airborne && IsWalking) 
		{
			animator.SetTrigger("jump");
		}
	}

	void Block()
	{
		block = true; 
		blockDelay = true;
	}


	//Events from animations
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
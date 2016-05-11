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
	float invincibilityFrames;
    bool airDashCoolDown;

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
		
		/*Debug.DrawLine(origin1, origin1 + direction1);
		Debug.DrawLine(origin2, origin2 + direction2);
		Debug.DrawLine(origin3, origin3 + direction3);
		Debug.DrawLine(origin4, origin4 + direction4);
		Debug.DrawLine(origin5, origin5 + direction5);*/
        
        

		if(!leftSide)
			animator.SetFloat ("forwardSpeed", body.velocity.z);
			//animator.SetFloat ("forwardSpeed", horizontalMoveAmount);
		else
			animator.SetFloat ("forwardSpeed", -body.velocity.z);
			//animator.SetFloat ("forwardSpeed", -horizontalMoveAmount);

		animator.SetBool ("airborne", airborne);

        if (!airborne)
            airDashCoolDown = false;

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

        invincibilityFrames--;
	}

	//indirect hit by an object
	void OnCollisionEnter(Collision collision) 
	{
		//other player
        Thing thing = collision.gameObject.GetComponent<Thing>();
        if (thing == null)
            return;


        //Debug.logger.Log("Indirect Hit");

        Hit(thing.oldVelocity.magnitude * thing.body.mass);
	}

    //direct hit by an object
    void OnTriggerEnter(Collider other)
    {
        Thing thing = other.GetComponent<Thing>();
        if (thing == null)
            return;

        //Debug.logger.Log("Direct Hit");

        if(invincibilityFrames <= 0)
            body.AddForce(Vector3.up * 500, ForceMode.Impulse);

        Hit(thing.oldVelocity.magnitude * thing.body.mass);
    }

    void Hit(float impactForce)
    {
       
        //Debug.logger.Log("Hit " + impactForce);
        if (impactForce < 500 || invincibilityFrames > 0)
            return;

        float damage = Mathf.Clamp(impactForce / 35, 0, 50);
        HP -= damage;

        if (damage <= 20)
            animator.SetTrigger("lightHit");
        else
            animator.SetTrigger("heavyHit");

        invincibilityFrames = 5;
        
    }

	//hit by an attack
	public void Attacked(Attack attack)
	{
		HP = Mathf.Clamp(HP - attack.damage, 0, startHP);

		if(attack.damage <= startHP / 5)
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
        if (airborne && airDashCoolDown)
            return;

        if (airborne)
            airDashCoolDown = true;

		body.velocity = (new Vector3 (0, body.velocity.y + 3, Mathf.Sign(amount) * moveSpeed * 2));

		if(IsWalking || airborne)
		{

			if (amount > 0 != !leftSide )
				animator.SetTrigger ("dashBackward");
			else
				animator.SetTrigger ("dashForward");
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
		if(!airborne)
			body.velocity += transform.up * jumpHeight;
	}

	public void createAttack()
	{
        Debug.logger.Log("Create Attack");
		behavior.createAttack (GetComponent<Collider>());
		invincibilityFrames = 10;
	}
	
	public void destroyAttack()
	{
		behavior.destroyAttack ();
	}
}
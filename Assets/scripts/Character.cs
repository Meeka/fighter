using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	public Rigidbody body;

	public float HP;
	public controls controls;
	Attack currentAttack;

	public Animator animator;

	Animation idle;
	Animation dead;

	public float moveSpeed;

	public Attack Normal1;
	public Attack Normal2;
	public Attack Special1;
	public Attack Special2;

	public Transform rightHand;
	public Transform leftHand;

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

		//if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle"))
		//	Destroy (currentAttack);

	}

	void DoAttack(Attack attack)
	{
		currentAttack = attack;

	}
	
	void HorizontalMove(float amount)
	{
		transform.position += transform.forward * amount * moveSpeed;
	}
	void VerticalMove(float amount)
	{
		transform.position += transform.right * amount * moveSpeed;
	}
	void HeavyAttack()
	{
		//currentAttack = Instantiate (Normal1);
		//currentAttack.transform.parent = rightHand;

		animator.SetTrigger ("HeavyAttack");

		//currentAttack.charAnimation.Play ("play");
	}
	void LightAttack(){}

}

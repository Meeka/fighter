using UnityEngine;
using System.Collections;

public class behavior : StateMachineBehaviour {

	public Attack attackPrefab;
	
	
	private Transform particlesTransform;       // Reference to the instantiated prefab's transform.
	private ParticleSystem particleSystem;      // Reference to the instantiated prefab's particle system.
	private Attack attack;
	
	
	// This will be called when the animator first transitions to this state.
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}
	
	
	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		destroyAttack ();
	}
	
	
	// This will be called every frame whilst in the state.
	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (attack == null)
			return;

		// Find the position and rotation of the limb the particles should follow.
		Vector3 limbPosition = animator.GetIKPosition(attack.attackLimb);
		Quaternion limbRotation = animator.GetIKRotation (attack.attackLimb);

		// OnStateExit may be called before the last OnStateIK so we need to check the particles haven't been destroyed.
		if (particleSystem != null && particlesTransform != null) {
			// Set the particle's position and rotation based on that limb.
			particlesTransform.position = limbPosition;
			particlesTransform.rotation = limbRotation;

			// If the particle system isn't playing, play it.
			if(!particleSystem.isPlaying)
				particleSystem.Play();
		}

		attack.transform.position = limbPosition;
	}

	public void SetAttack(Attack newAttackPrefab)
	{
		attackPrefab = newAttackPrefab;
	}

	public void createAttack(Collider collider){
		if (attackPrefab != null) {
			attack = Instantiate<Attack> (attackPrefab);
			attack.sourceChar = collider;
			attack.transform.position = collider.transform.position;
			Debug.Log("Attack" + attack.transform.position);
			if(attack.particles != null)
			{
				// Instantiate the particles and set up references to their components.
				GameObject particlesInstance = Instantiate(attack.particles);
				particlesTransform = particlesInstance.transform;
				particleSystem = particlesInstance.GetComponent <ParticleSystem> ();
			}
		}
	}
	public void destroyAttack()
	{
		if (attack != null) {
			Destroy (attack.gameObject);
		}
		if(particleSystem != null)
		{
			// When leaving the special move state, stop the particles.
			particleSystem.Stop ();
			Destroy (particleSystem.gameObject);
		}
	}
}
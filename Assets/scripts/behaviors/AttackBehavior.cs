using UnityEngine;
using System.Collections;

public class AttackBehavior : StateMachineBehaviour {

    Character sourceObject;

	public Attack attackPrefab;
	
	
	private Transform particlesTransform;       // Reference to the instantiated prefab's transform.
	private GameObject particleSystem;      // Reference to the instantiated prefab's particle system.
	private Attack attack;
	
	
	// This will be called when the animator first transitions to this state.
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        sourceObject = animator.GetComponent<Character>();
        sourceObject.OnStartAttack += createAttack;
        sourceObject.OnEndAttack += endAttack;
	}
	
	
	// This will be called once the animator has transitioned out of the state.
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		endAttack ();
	}
	
	
	// This will be called every frame whilst in the state.
	override public void OnStateIK (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (attack == null)
			return;

		// OnStateExit may be called before the last OnStateIK so we need to check the particles haven't been destroyed.
		if (particleSystem != null && particlesTransform != null) {
            
            // Find the position and rotation of the limb the particles should follow.
			// Set the particle's position and rotation based on that limb.
            switch (attack.particalAttachAnchor)
            {
                case Attack.ParticalAttachAnchor.LeftArm:
                    particlesTransform.position = animator.GetIKPosition(AvatarIKGoal.LeftHand);
                    particlesTransform.rotation = animator.GetIKRotation(AvatarIKGoal.LeftHand);
                    break;

                case Attack.ParticalAttachAnchor.RightArm:
                    particlesTransform.position = animator.GetIKPosition(AvatarIKGoal.RightHand);
                    particlesTransform.rotation = animator.GetIKRotation(AvatarIKGoal.RightHand);
                    break;

                case Attack.ParticalAttachAnchor.Body:
                default:
                    break; //do nothing.. should be parented already
                    
            }

			// If the particle system isn't playing, play it.
            foreach (ParticleSystem particals in particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                if (!particals.isPlaying)
                    particals.Play();
            }
		}

        attack.transform.position = animator.GetIKPosition(attack.attackLimb); ;
	}

    //public void SetAttack(Attack newAttackPrefab)
    //{
    //    attackPrefab = newAttackPrefab;
    //}

	public void createAttack(){
		if (attackPrefab != null) {
			attack = Instantiate<Attack> (attackPrefab);
            attack.sourceChar = sourceObject.gameObject;
            attack.transform.position = sourceObject.transform.position;
            attack.transform.rotation = sourceObject.transform.rotation;
            

			if(attack.particles != null)
			{
				// Instantiate the particles and set up references to their components.
                particleSystem = Instantiate(attack.particles);
                particlesTransform = particleSystem.transform;
                if (attack.particalAttachAnchor == Attack.ParticalAttachAnchor.Body)
                    particlesTransform.SetParent(attack.sourceChar.gameObject.transform, false);
			}
		}
	}
	public void endAttack()
	{
        //Debug.logger.Log("endAttack");
        sourceObject.OnStartAttack -= createAttack;
        sourceObject.OnEndAttack -= endAttack;

		if (attack != null) {
			attack.End();
		}
		if(particleSystem != null)
		{
			// When leaving the special move state, stop the particles.
            foreach (ParticleSystem particals in particleSystem.GetComponentsInChildren<ParticleSystem>())
            {
                particals.Stop();
            }
            Destroy(particleSystem);
		}
	}
}
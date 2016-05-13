using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public enum ParticalAttachAnchor
    {
        Body,
        LeftArm,
        RightArm
    }
    public ParticalAttachAnchor particalAttachAnchor;

	public float damage;
	[HideInInspector]
    public GameObject sourceChar;
	public GameObject particles;            // Prefab of the particle system to play in the state.
    


	public AvatarIKGoal attackLimb;

	public float forceMultiplier = 10;
	public Vector3 HitAngle;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject != sourceChar) 
		{

			Vector3 HitAngleNew = HitAngle.normalized;
			if(other.transform.position.z < transform.position.z)
				HitAngleNew = new Vector3(0, HitAngleNew.y, HitAngleNew.z * -1);

			Character character = other.GetComponent<Character>();
            if (character != null)
                character.Attacked(this);
            else
                other.GetComponent<Thing>().Attacked(this);
			other.attachedRigidbody.AddForce(HitAngleNew * forceMultiplier, ForceMode.Impulse);
		}
	}

    internal void End()
    {
        AudioSource soundClip = GetComponent<AudioSource>();
        if (soundClip == null || !soundClip.isPlaying)
            DestroyThis();
        else
        {
            GetComponent<Collider>().enabled = false;
            Invoke("DestroyThis", soundClip.clip.length - soundClip.time);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}



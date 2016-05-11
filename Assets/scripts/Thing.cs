using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

    public Rigidbody body;
    Collider collider;
    bool attackEndable;
    AudioSource audio;

    public Vector3 oldVelocity;

    //ParticalSystem particals;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        oldVelocity = body.velocity;
    }

    internal void Attacked(Attack attack)
    {
        StartAttack();
    }

    void StartAttack()
    {
        collider.isTrigger = true;
        attackEndable = false;
        Invoke("SetEndable", 0.2f);
    }

    void SetEndable()
    {
        attackEndable = true;
    }

    void EndAttack()
    {
        collider.isTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {

        if (attackEndable && other.name == "Ground")
        {
            EndAttack();
            return;
        }

        

        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (otherBody != null && collider.isTrigger)
        {
            audio.Play();

            if(other.GetComponent<Character>() == null)
                otherBody.AddForce(Vector3.up * 500, ForceMode.Impulse);
        }
    }
}

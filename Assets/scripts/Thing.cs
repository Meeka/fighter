using UnityEngine;
using System.Collections;

public class Thing : MonoBehaviour {

    Rigidbody body;
    float startMass;

    //ParticalSystem particals;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        startMass = body.mass;
    }

    void Update()
    {
        if (body.velocity.magnitude < 1)
            EndAttack();
    }

    internal void Attacked(Attack attack)
    {
        Invoke("StartAttack", 0.1f);
    }

    void StartAttack()
    {
        body.mass = startMass * 10;
    }

    void EndAttack()
    {
        body.mass = startMass;
    }
}

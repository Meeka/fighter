using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public controls testControl;

	// Use this for initialization
	void Start () {
        testControl.onHorizontalMov += horzMovEventReceived;
        testControl.onVerticalMov += vertMovEventReceived;
		testControl.onHeavyAttack += heavyAttackEventReceived;
		testControl.onLightAttack += lightAttackEventReceived;
		testControl.onJump += jumpEventReceived;
		testControl.onBlock += blockEventReceived;
	}

    private void vertMovEventReceived(float direction)
    {
        Debug.Log("We moved vertically by this much: " + direction.ToString());
    }

    private void horzMovEventReceived(float direction)
    {
        Debug.Log("We moved horizontally by this much: " + direction.ToString());
    }

	private void heavyAttackEventReceived()
	{
		Debug.Log ("Heavy Attack!!");
	}

	private void lightAttackEventReceived()
	{
		Debug.Log ("Light Attack!!");
	}

	private void jumpEventReceived()
	{
		Debug.Log ("Jump!!");
	}

	private void blockEventReceived()
	{
		Debug.Log ("Block!!");
	}
	// Update is called once per frame
	void Update () {
	
	}
}

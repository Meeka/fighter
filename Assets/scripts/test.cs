using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour {

	/*enum input
	{
		heavy,
		light
	}

	Dictionary<input, string> plr1Map = new Dictionary<input, string>
	{
		{light, "Player1Light"},
		{heavy, "Player1Heavy"}
	};

	Dictionary<input, string> plr2Map = new Dictionary<input, string>
	{
		{light, "Player2Light"},
		{heavy, "Player2Heavy"}
	};*/




	
	//Dictionary dict;





    public controls testControl;

	// Use this for initialization
	void Start () {

		/*if (player1)
			dict = plr1Map;
		else
			dict = plr2Map;

		Input.GetButtonDown (dict [light]);*/

        testControl.onHorizontalMov += horzMovEventReceived;
        testControl.onVerticalMov += vertMovEventReceived;
		testControl.onHeavyAttack += heavyAttackEventReceived;
		testControl.onLightAttack += lightAttackEventReceived;
		testControl.onJump += jumpEventReceived;
		testControl.onBlock += blockEventReceived;
		testControl.onDash += dashEventReceived;
	}

    private void vertMovEventReceived(float direction)
    {
        Debug.Log("Player" + testControl.playerNum + " moved vertically by this much: " + direction.ToString());
    }

    private void horzMovEventReceived(float direction)
    {
		Debug.Log("Player" + testControl.playerNum + " moved horizontally by this much: " + direction.ToString());
    }

	private void heavyAttackEventReceived()
	{
		Debug.Log ("Player" + testControl.playerNum + " Heavy Attack!!");
	}

	private void lightAttackEventReceived()
	{
		Debug.Log ("Player" + testControl.playerNum + " Light Attack!!");
	}

	private void jumpEventReceived()
	{
		Debug.Log ("Player" + testControl.playerNum + " Jump!!");
	}

	private void blockEventReceived()
	{
		Debug.Log ("Player" + testControl.playerNum + " Block!!");
	}

	private void dashEventReceived(float direction)
	{
		Debug.Log ("Player" + testControl.playerNum + " Dash!! in this direction:" + direction.ToString());
	}
	// Update is called once per frame
	void Update () {
	
	}
}

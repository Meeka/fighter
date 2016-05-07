using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class controls : MonoBehaviour {


    public delegate void horizontalMov(float direction);
    public horizontalMov onHorizontalMov;

	public delegate void dash(float direction);
	public dash onDash;

    public delegate void verticalMov(float direction);
    public verticalMov onVerticalMov;

	public delegate void heavyAttack();
	public heavyAttack onHeavyAttack;

	public delegate void lightAttack();
	public lightAttack onLightAttack;

	public delegate void jump();
	public jump onJump;

	public delegate void block();
	public block onBlock;

	public float ButtonCooler = 0 ; // Half a second before reset
	public int ButtonCount = 0;

	private float tempHAxis = 0;

	public int playerNum = 0;

	enum input
	{
		heavyAtk,
		lightAtk,
		horizontal,
		vertical
	}

	Dictionary<input, string> dict;

	Dictionary<input, string> plr1Map = new Dictionary<input, string>{
		{input.lightAtk, "Player1Light"},
		{input.heavyAtk, "Player1Heavy"},
		{input.horizontal, "Player1Horizontal"},
		{input.vertical, "Player1Vertical"}
	};

	Dictionary<input, string> plr2Map = new Dictionary<input, string>{
		{input.lightAtk, "Player2Light"},
		{input.heavyAtk, "Player2Heavy"},
		{input.horizontal, "Player2Horizontal"},
		{input.vertical, "Player2Vertical"}
	};

    // Use this for initialization
	void Start () {
		if (playerNum == 1) {
			dict = plr1Map;
		}
		else if (playerNum == 2){
			dict = plr2Map;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool buttonPressed = tempHAxis < Input.GetAxis (dict [input.horizontal]);
		bool buttonReleased = tempHAxis > Input.GetAxis (dict [input.horizontal]);

		//first press
		if (buttonPressed && ButtonCount == 0)
		{
			ButtonCooler = 0.5f;
			ButtonCount += 1;
		}
		//first release
		if (buttonReleased && ButtonCount == 1) {
			ButtonCount += 1;
		}
		//second press - DASH!!
		if (buttonPressed && ButtonCount == 2) {
			ButtonCooler = 0;
			onDash.Invoke(Input.GetAxis (dict [input.horizontal]));
		}
		if (ButtonCooler > 0) {
			ButtonCooler -= 1 * Time.deltaTime;
		} else {
			ButtonCount = 0;
		}

		tempHAxis = Input.GetAxis (dict [input.horizontal]);

		if (Input.GetAxis(dict [input.horizontal]) != 0 && onHorizontalMov != null)
		{
			onHorizontalMov.Invoke(Input.GetAxis(dict [input.horizontal]));
		}

		if (Input.GetAxis(dict [input.vertical]) != 0 && onVerticalMov != null)
        {
			onVerticalMov.Invoke(Input.GetAxis(dict [input.vertical]));
        }
		if (Input.GetButtonDown(dict [input.heavyAtk]) && onHeavyAttack != null)
		{
			onHeavyAttack.Invoke();
		}
		if (Input.GetButtonDown(dict [input.lightAtk]) && onLightAttack != null)
		{
			onLightAttack.Invoke();
		}
	}
}

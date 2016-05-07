using UnityEngine;
using System.Collections;

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


    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		bool buttonPressed = tempHAxis < Input.GetAxis ("Horizontal");
		bool buttonReleased = tempHAxis > Input.GetAxis ("Horizontal");

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
			onDash.Invoke(Input.GetAxis ("Horizontal"));
		}
		if (ButtonCooler > 0) {
			ButtonCooler -= 1 * Time.deltaTime;
		} else {
			ButtonCount = 0;
		}

		tempHAxis = Input.GetAxis ("Horizontal");

		if (Input.GetAxis("Horizontal") != 0 && onHorizontalMov != null)
		{
			onHorizontalMov.Invoke(Input.GetAxis("Horizontal"));
		}

        if (Input.GetAxis("Vertical") != 0 && onVerticalMov != null)
        {
            onVerticalMov.Invoke(Input.GetAxis("Vertical"));
        }
		if (Input.GetKeyDown("j") && onHeavyAttack != null)
		{
			onHeavyAttack.Invoke();
		}
		if (Input.GetKeyDown("k") && onLightAttack != null)
		{
			onLightAttack.Invoke();
		}
		if (Input.GetKeyDown("space") && onJump != null) 
		{
			onJump.Invoke();
		}
		if (Input.GetKeyDown("l") && onBlock != null) 
		{
			onBlock.Invoke();
		}
	}
}

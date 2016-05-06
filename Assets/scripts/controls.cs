using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {


    public delegate void horizontalMov(float direction);
    public horizontalMov onHorizontalMov;

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

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") != 0 && onHorizontalMov != null)
        {
            onHorizontalMov.Invoke(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0 && onVerticalMov != null)
        {
            onVerticalMov.Invoke(Input.GetAxis("Vertical"));
        }
		if (Input.GetKey("j") && onHeavyAttack != null)
		{
			onHeavyAttack.Invoke();
		}
		if (Input.GetKey("k") && onLightAttack != null)
		{
			onLightAttack.Invoke();
		}
		if (Input.GetKey ("space") && onJump != null) 
		{
			onJump.Invoke();
		}
		if (Input.GetKey ("l") && onBlock != null) 
		{
			onBlock.Invoke();
		}
	}
}

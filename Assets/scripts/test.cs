using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

    public controls testControl;

	// Use this for initialization
	void Start () {
        testControl.onHorizontalMov += horzMovEventReceived;
        testControl.onVerticalMov += vertMovEventReceived;
	}

    private void vertMovEventReceived(float direction)
    {
        Debug.logger.Log("We moved vertically by this much: " + direction.ToString());
    }

    private void horzMovEventReceived(float direction)
    {
        Debug.logger.Log("We moved horizontally by this much: " + direction.ToString());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class controls : MonoBehaviour {


    public delegate void horizontalMov(float direction);
    public horizontalMov onHorizontalMov;

    public delegate void verticalMov(float direction);
    public verticalMov onVerticalMov;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizonal") != 0 && onHorizontalMov != null)
        {
            onHorizontalMov.Invoke(Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Vertical") != 0 && onVerticalMov != null)
        {
            onVerticalMov.Invoke(Input.GetAxis("Vertical"));
        }
	}
}

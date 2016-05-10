using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public Transform player1;
	public Transform player2;

    public float minDist = 8;
    public float maxDist = 35;
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (player1.position, player2.position);
        transform.position = (player1.position + player2.position) / 2 + new Vector3(Mathf.Min(-minDist, Mathf.Max(-maxDist, dist * -1)), 3, 0);

	}
}

using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public Transform player1;
	public Transform player2;

	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (player1.position, player2.position);
		transform.position = (player1.position + player2.position) / 2 + new Vector3 (Mathf.Min(-10, Mathf.Max (-35, dist * -1)), 3, 0);

	}
}

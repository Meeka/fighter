using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropRandomThings : MonoBehaviour {


	 //y 20 z -35 35

	public List<GameObject> ThingsPrefabs;

	float timer = 5;

	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if(timer <0)
		{
			GameObject o = Instantiate<GameObject>(ThingsPrefabs[Random.Range(0, ThingsPrefabs.Count)]);
            o.transform.parent = transform.parent;
			o.transform.position = new Vector3(0, 20, Random.Range(-35,35));
			timer = 5;
		}
	}
}

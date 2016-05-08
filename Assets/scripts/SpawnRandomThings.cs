using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnRandomThings : MonoBehaviour {

	public List<GameObject> ThingsPrefabs;

	// Use this for initialization
	void Start () {
		float offset = 0;
		for (int i = 0; i < Random.Range(0, 8); i ++) {
			GameObject go = Instantiate(ThingsPrefabs[Random.Range(0, ThingsPrefabs.Count)]);
			offset += go.GetComponent<Collider>().bounds.extents.y;
			go.transform.position = transform.position + Vector3.up * offset;
			offset += go.GetComponent<Collider>().bounds.extents.y;

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
	public float time;
	public float speed;
	public bool translate;
	public float max;
	private float StartTime = 0;
	private float StartPos = 0;
	public float speedt = 0.001f;

	void Start () {
		StartPos = transform.position.x;
	}

	void Update () {
		if (Time.timeSinceLevelLoad - StartTime > time) {
			speed *= -1;
			StartTime = Time.timeSinceLevelLoad;
		}
		transform.Rotate(Vector3.forward * speed * Time.deltaTime);
		if (translate) {
			if (StartPos - transform.position.x > max || StartPos - transform.position.x < max * -1)
				speedt *= -1;
			transform.Translate(new Vector3(speedt, 0, 0));
		}
	}
}

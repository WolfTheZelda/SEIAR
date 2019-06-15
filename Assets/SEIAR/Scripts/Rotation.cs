using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	public float RotationSpeedX;
	public float RotationSpeedY;
	public float RotationSpeedZ;

	void Update () {

		transform.Rotate (RotationSpeedX * Time.deltaTime, RotationSpeedY * Time.deltaTime, RotationSpeedZ * Time.deltaTime);
		
	}
}

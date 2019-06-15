using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float MoveSpeed = 0.004f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), MoveSpeed);

        Vector3 Difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        Difference.Normalize();
        float RotationZ = Mathf.Atan2(Difference.y, Difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, RotationZ);

    }
}

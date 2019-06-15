using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeObject : MonoBehaviour {

    private bool Shake;

    public float ValueOne = -0.00025f;
    public float ValueTwo = 0.00025f;

    void Update () {

        if (!Shake)
        {
            transform.Translate(ValueOne, ValueTwo, 0);
            Shake = true;
        }
        else
        {
            transform.Translate(ValueTwo, ValueOne, 0);
            Shake = false;
        }
		
	}
}

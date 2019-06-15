using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomHead : MonoBehaviour {

    public float Y = 50f;
    public float Z = -25f;
    public float TimeFloat = 6.5f;

    void Update () {

        gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition, Vector3.zero.SetY(Y), Time.deltaTime / TimeFloat);
        gameObject.transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Z);

    }
}

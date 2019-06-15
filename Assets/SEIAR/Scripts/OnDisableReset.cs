using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableReset : MonoBehaviour
{

    public Vector3 ResetPosition;

    private void OnDisable()
    {

        transform.localPosition = ResetPosition;

    }

}
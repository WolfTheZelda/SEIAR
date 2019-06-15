using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

	public GameObject CameraStrange;

	private Camera CameraStrangeComponent;
	private float CameraStrangeFieldOfView;
	private Camera CameraLocalComponent;

	void Awake ()
	{

		CameraLocalComponent = GetComponent<Camera> ();
		CameraStrangeComponent = CameraStrange.GetComponent <Camera> ();

        CameraStrangeFieldOfView = CameraStrangeComponent.fieldOfView;
        CameraLocalComponent.fieldOfView = CameraStrangeFieldOfView;

    }

	void FixedUpdate ()
	{

		CameraStrangeFieldOfView = CameraStrangeComponent.fieldOfView;
		CameraLocalComponent.fieldOfView = CameraStrangeFieldOfView;
		
	}
}

using UnityEngine;

public class Gyroscope : MonoBehaviour
{

	private Quaternion RotationFix;
	public GameObject Target;
	private float StartY;
	
	public Transform ZoomObject;

	void Start ()
	{

		GameObject Parent = new GameObject ("GPS Camera");
		Parent.transform.position = transform.position;
		transform.parent = Parent.transform;
        Input.gyro.enabled = true;
		Parent.transform.rotation = Quaternion.Euler (90f, 100f, 0);
		RotationFix = new Quaternion (0, 0, 1, 0);
		
	}

	void Update ()
	{

		if (StartY == 0) {

			ResetGyroPosition ();
		
		}
		transform.localRotation = Input.gyro.attitude * RotationFix;
		Debug.DrawRay (transform.position, transform.forward, Color.green);

	}

	public void ResetGyroPosition ()
	{

        RaycastHit Hit;

		if (Physics.Raycast (transform.position, transform.forward, out Hit, 7500)) {

			Vector3 HitPoint = Hit.point;
			HitPoint.y = 0;
            float Z = Vector3.Distance (Vector3.zero, HitPoint);
			ZoomObject.localPosition = new Vector3 (0f, ZoomObject.localPosition.y, Z);
            Geolocation.OriginalPosition = ZoomObject.localPosition;

        }

		StartY = transform.eulerAngles.y;
		Target.transform.rotation = Quaternion.Euler (0, StartY, 0);

	}
}

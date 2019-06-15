using System.Collections;
using UnityEngine;

public class Geolocation : MonoBehaviour
{

    // private string LatitudeFix;
    // private string LongitudeFix;
    private float OriginalLatitude;
    private float OriginalLongitude;
    private float CurrentLongitude;
    private float CurrentLatitude;
    private GameObject DistanceTextObject;
    private double Distance;
    private bool SetOriginalValues = true;
    private Vector3 TargetPosition;
    static public Vector3 OriginalPosition;
    private float Speed = 0.15f;

    IEnumerator GetCoordinates()
    {
        while (true)
        {

            if (!Input.location.isEnabledByUser) { yield break; }

            Input.location.Start(1.0f, 0.1f);

            int MaxWait = 20;

            while (Input.location.status == LocationServiceStatus.Initializing && MaxWait > 0)
            {
                yield return new WaitForSeconds(1);
                MaxWait--;
            }

            if (MaxWait < 1)
            {
                print("E morreu!!!");
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Seu gps é quase tão ruim quanto o de Laerte!");
                yield break;
            }
            else
            {

                print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

                if (SetOriginalValues)
                {
                    OriginalLatitude = Input.location.lastData.latitude;
                    OriginalLongitude = Input.location.lastData.longitude;
                    SetOriginalValues = false;
                }

                CurrentLatitude = Input.location.lastData.latitude;
                CurrentLongitude = Input.location.lastData.longitude;

                // LatitudeFix = Input.location.lastData.latitude.ToString("R");
                // LongitudeFix = Input.location.lastData.longitude.ToString("R");

                Calc(OriginalLatitude, OriginalLongitude, CurrentLatitude, CurrentLongitude);

            }
            Input.location.Stop();
        }
    }

    public void Calc(float LatOne, float LonOne, float LatTwo, float LonTwo)
    {
        var R = 6378.137;
        var Lat = LatTwo * Mathf.PI / 180 - LatOne * Mathf.PI / 180;
        var Lon = LonTwo * Mathf.PI / 180 - LonOne * Mathf.PI / 180;
        float a = Mathf.Sin(Lat / 2) * Mathf.Sin(Lat / 2) + Mathf.Cos(LatOne * Mathf.PI / 180) * Mathf.Cos(LatTwo * Mathf.PI / 180) * Mathf.Sin(Lon / 2) * Mathf.Sin(Lon / 2);
        var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        Distance = R * c;
        Distance = Distance * 1000f;
        float DistanceFloat = (float)Distance;
        TargetPosition = OriginalPosition - new Vector3(0, 0, DistanceFloat * 12.5f);
    }

    void Start()
    {

        StartCoroutine("GetCoordinates");

        TargetPosition = transform.position;
        OriginalPosition = transform.position;

    }

    void Update()
    {

        transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, Speed * Time.deltaTime);

        // transform.eulerAngles += new Vector3 (0, 1f, 0);
    }
}


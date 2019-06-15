using UnityEngine;
using System.Collections;

public class AtomicBomb : MonoBehaviour
{

    static public bool Detonated;

    public LensFlare LightFlare;
    public GameObject Crater, Head;
    public ParticleSystem Body;
    
    IEnumerator CraterSpawn()
    {

        yield return new WaitForSeconds(2.5f);

        Detonated = false;

        Crater.SetActive(true);

        StopCoroutine("CraterSpawn");

    }

    IEnumerator Restart()
    {

        yield return new WaitForSecondsRealtime(45.0f);

        Head.transform.localPosition = new Vector3(0, 0, 0);

        Head.SetActive(false);
        Body.transform.gameObject.SetActive(false);

        StopCoroutine("Restart");

    }

    IEnumerator FlareBrightness()
    {

        for (float i = 0; i < 1; i += Time.deltaTime * 5)
        {

            LightFlare.brightness = Mathf.Lerp(LightFlare.brightness, 5, i);

            yield return 0;

        }

        for (float i = 0; i < 1; i += Time.deltaTime / 10)
        {

            LightFlare.brightness = Mathf.Lerp(LightFlare.brightness, 0, i);

            yield return 0;

        }

        StopCoroutine("FlareBrightness");

    }

    void Update()
    {

        if (Detonated)
        {

            StartCoroutine("FlareBrightness");
            StartCoroutine("CraterSpawn");
            StartCoroutine("Restart");

            Body.transform.gameObject.SetActive(true);
            Head.SetActive(true);

        }
    }

}

public static class Extensions
{

    public static Vector3 SetX(this Vector3 Vector, float x)
    {

        Vector.x = x;
        return Vector;

    }

    public static Vector3 SetY(this Vector3 Vector, float y)
    {

        Vector.y = y;
        return Vector;

    }

    public static Vector3 SetZ(this Vector3 Vector, float z)
    {

        Vector.z = z;
        return Vector;

    }

    public static void CopyFrom(this Transform t, Transform Other)
    {

        t.position = Other.position;
        t.rotation = Other.rotation;
        t.localScale = Other.localScale;

    }
}
using UnityEngine;

public class ScreenScale : MonoBehaviour
{    
	void Start ()
	{
        transform.localScale = new Vector3 ((1.70f * Screen.width / Screen.height), transform.localScale.y, transform.localScale.z);
	}
}
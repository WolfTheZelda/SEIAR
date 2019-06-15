using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public void Update()
    {
        if (transform.position.x <= -20.0f)
        {
            transform.position = new Vector2(transform.position.x + 54.61f, transform.position.y);
        }
    }
}
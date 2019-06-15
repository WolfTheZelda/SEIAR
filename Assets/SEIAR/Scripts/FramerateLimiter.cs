using UnityEngine;

public class FramerateLimiter : MonoBehaviour
{
    void Awake()
    {

        Application.targetFrameRate = 30;

    }
}
using UnityEngine;

public class IfUnityEditor : MonoBehaviour
{

    static public bool UnityEditor;

    public bool Enable;

    void Awake()
    {

#if UNITY_EDITOR
        if (Enable)
        {
            UnityEditor = true;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("Editor", "true");
            PlayerPrefs.SetString("Torch", "true");
            PlayerPrefs.SetInt("LanguageNumber", 0);
        }
#endif

    }
}
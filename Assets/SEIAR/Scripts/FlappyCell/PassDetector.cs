using UnityEngine;

public class PassDetector : MonoBehaviour 
{
	void OnTriggerEnter(Collider Other)
	{        
        if (Other.CompareTag("Player"))
		{
            FlappyCellButton.Static.BirdScored();            
		}
	}
}

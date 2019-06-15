using UnityEngine;

public class ScrollingObject : MonoBehaviour 
{
    public bool Sky;
    public bool Ground;
    
	void Update()
	{		
		if(FlappyCellButton.Static.GameOver == false)
		{
            if (Sky)
            {
                transform.Translate(Vector3.right * FlappyCellButton.Static.ScrollSpeed);
            }

            if (Ground)
            {
                transform.Translate(Vector3.left * FlappyCellButton.Static.ScrollSpeed);
            }
        } else {
            transform.Translate(Vector3.zero);
        }
	}
}

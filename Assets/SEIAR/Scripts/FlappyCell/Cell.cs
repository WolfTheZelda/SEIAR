using UnityEngine;

public class Cell : MonoBehaviour 
{
	public float Force;			
	private bool Dead;
    private bool Collided;
    private Renderer[] CellRenderer;
    private Rigidbody CellRigidbody;
	public GameObject AudioSystem;

	void Start()
	{
        CellRenderer = GetComponentsInChildren<Renderer>();
        CellRigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{		
		if (Dead == false) 
		{			
			if (Input.GetMouseButtonDown(0) && TouchDetector.Touched)
            {
                CellRigidbody.velocity = Vector2.zero;
                CellRigidbody.AddForce(new Vector2(0, Force));
			}
		}              
    }

	void OnCollisionEnter (Collision Other)
	{
        Collided = true;

        if (Collided == true)
        {
			AudioSystem.SetActive (false);
            Dead = true;
            FlappyCellButton.Static.GameOver = true;
            FlappyCellButton.Static.BirdDied();
            CellRigidbody.velocity = Vector2.zero;
            foreach (Renderer Go in CellRenderer) { Go.material.SetColor("_Color", Color.gray); }
            Collided = false;
        }
    }
}

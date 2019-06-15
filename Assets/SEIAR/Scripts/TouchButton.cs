using UnityEngine;

public class TouchButton : MonoBehaviour
{
    public float DeltaMagnitudeDifference;

	private Vector3 ScreenPoint;
    private Vector3 Offset;

    void Update()
    {            
        if (Input.touchCount == 2)
        {

            Touch TouchZeroPosition = Input.GetTouch(0);
            Touch TouchOnePosition = Input.GetTouch(1);

            Vector2 TouchZeroPreviousPosition = TouchZeroPosition.position - TouchZeroPosition.deltaPosition;
            Vector2 TouchOnePreviousPosition = TouchOnePosition.position - TouchOnePosition.deltaPosition;

            float PreviousTouchDeltaMagnitude = (TouchZeroPreviousPosition - TouchOnePreviousPosition).magnitude;
            float TouchDeltaMagnitude = (TouchZeroPosition.position - TouchOnePosition.position).magnitude;

            DeltaMagnitudeDifference = PreviousTouchDeltaMagnitude - TouchDeltaMagnitude;
                        
            gameObject.transform.localScale = new Vector3(DeltaMagnitudeDifference / 1000, DeltaMagnitudeDifference / 1000, DeltaMagnitudeDifference / 1000);
            
        }
    }

    void OnMouseDown()
    {
        if (Input.touchCount < 2)
        {
            ScreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            Offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (Input.touchCount < 2) { 
            Vector3 CursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);
        Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(CursorPoint) + Offset;
        transform.position = CursorPosition;
    }
    }
}
using UnityEngine;

public class MeteorDestroy : MonoBehaviour {

    private void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Meteor"))
        {
            Destroy(Other.gameObject);
        }
    }    
}

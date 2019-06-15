using UnityEngine;

public class MeteorInstantiate : MonoBehaviour {

    private float LastTime = 0;
    public GameObject MeteorPrefab;
    public float SpawnTime = 15;
    public int SpawnForTime = 5;
    public float MaxX, MaxY;
    private Vector3 MeteorPosition;
    	
	void Update () {

        LastTime += Time.deltaTime;
        float X = Random.Range(0, MaxX);
        float Y = Random.Range(0, MaxY);

        if (LastTime >= SpawnTime)
        {
            LastTime = 0;            
            MeteorPosition = new Vector3(X, Y, -75);

            // GameObject Meteor = Instantiate(MeteorPrefab, MeteorPosition, Quaternion.identity);  
            Instantiate(MeteorPrefab, MeteorPosition, Quaternion.identity);                     
        }		
	}
}

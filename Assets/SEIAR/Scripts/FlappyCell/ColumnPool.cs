using UnityEngine;

public class ColumnPool : MonoBehaviour 
{
	public GameObject VirusPrefab;										
	public float SpawnTime = 7.5f;									
	public float MinY = -2.5f;									
	public float MaxY = 2.5f;

    private bool Spawned;
    private GameObject Virus;
    private Vector2 VirusPosition;
    private float SpawnPositionY;
    private float LastSpawnTime = 0;
    private float SpawnPositionX = 7.5f;   
    	
	void Update()
	{
        LastSpawnTime += Time.deltaTime;
        
        if (FlappyCellButton.Static.GameOver == false && LastSpawnTime >= SpawnTime) 
		{         
            LastSpawnTime = 0f;            
            SpawnPositionY = Random.Range(MinY, MaxY);
            VirusPosition = new Vector2 (SpawnPositionX, SpawnPositionY);
            if (!Spawned) { Virus = (GameObject)Instantiate(VirusPrefab, VirusPosition, Quaternion.identity); Spawned = true; } else { Virus.transform.position = new Vector2(SpawnPositionX, SpawnPositionY); }             
        } 
    }
}
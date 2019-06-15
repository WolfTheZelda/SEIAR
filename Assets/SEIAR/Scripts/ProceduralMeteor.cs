using UnityEngine;
using System.Collections.Generic;

public class ProceduralMeteor : MonoBehaviour
{
    public int Seed = 37823787;
    private Material MeteorMaterial;
    private List<Vector3> Vertices = new List<Vector3>();
    private List<Vector3> DoneVertices = new List<Vector3>();
    private Vector3 Center;
    
    void Start()
    {        
        Random.InitState(Seed);
        float Offset = Random.Range(0, 20);

        Mesh MeteorMesh = GetComponent<MeshFilter>().mesh;

        for (int s = 0; s < MeteorMesh.vertices.Length; s++)
        {
            Vertices.Add(MeteorMesh.vertices[s]);
        }

        Center = GetComponent<Renderer>().bounds.center;

        for (int v = 0; v < Vertices.Count; v++)
        {
            bool Used = false;
            for (int k = 0; k < DoneVertices.Count; k++)
            {
                if (DoneVertices[k] == Vertices[v])
                {
                    Used = true;
                }
            }
            if (!Used)
            {
                Vector3 CurrentVector = Vertices[v];
                DoneVertices.Add(CurrentVector);
                int Smoothing = Random.Range(4, 6);
                Vector3 ChangedVector = (CurrentVector + ((CurrentVector - Center) * (Mathf.PerlinNoise((float)v / Offset, (float)v / Offset) / Smoothing)));
                for (int s = 0; s < Vertices.Count; s++)
                {
                    if (Vertices[s] == CurrentVector)
                    {
                        Vertices[s] = ChangedVector;
                    }
                }
            }
        }

        MeteorMesh.SetVertices(Vertices);
        MeteorMesh.RecalculateBounds();
        MeteorMesh.RecalculateNormals();

    }
}


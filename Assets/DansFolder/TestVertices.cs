using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVertices : MonoBehaviour
{

    Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void OnDrawGizmos()
    {
        if(mesh != null)
            Gizmos.DrawSphere(GetTrueHeight(), 0.1f);
    }
 

    public Vector3 GetTrueHeight()
    {
        
        Vector3 highestPoint = Vector3.negativeInfinity;
        foreach(Vector3 vertex in mesh.vertices)
        {
            if(WorldVertex(vertex).y > highestPoint.y)
            {
                highestPoint = WorldVertex(vertex);
            } 
        }

        return highestPoint;
    }

    public float GetFurthestPoint(Vector3 position)
    {
        float furthestPoint = 0;
        foreach (Vector3 vertex in mesh.vertices)
        {
            float distance = Vector3.Distance(position, vertex);
            if (distance > furthestPoint)
            {
                furthestPoint = distance;
            }
        }
        return furthestPoint;
    }

    public Vector3 WorldVertex(Vector3 vertex)
    {
        var localToWorld = transform.localToWorldMatrix;
        
        return localToWorld.MultiplyPoint3x4(vertex);
    }


}

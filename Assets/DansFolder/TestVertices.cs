using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVertices : MonoBehaviour
{

    Mesh mesh;

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }
    
    void Update()
    {
        Debug.DrawLine(transform.position, GetTrueHeight(), Color.magenta, 0.5f);
    }

    public Vector3 GetTrueHeight()
    {
        
        Vector3 highestPoint = GetCenterToVertex(mesh.vertices[0]);
        foreach(Vector3 vertex in mesh.vertices)
        {
            if(GetCenterToVertex(vertex).y > highestPoint.y)
            {
                highestPoint = GetCenterToVertex(vertex);
            }
            
        }

        return highestPoint;
    }

    public Vector3 GetCenterToVertex(Vector3 vertex)
    {
        Vector3 p = transform.position;
        Vector3 v = new Vector3(vertex.x * transform.localScale.x, vertex.y * transform.localScale.y, vertex.z * transform.localScale.z);
        Vector3 r = transform.rotation.eulerAngles;
        Quaternion q = Quaternion.Euler(r);
        return p - (q * v);
    }
}

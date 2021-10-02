using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool Locked { get; set; }

    [SerializeField] Mesh[] meshPool;

    MeshFilter mesh;
    MeshCollider meshCollider;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>();
        Debug.Log(mesh.mesh);
    }

    void Start()
    {
        int meshIndex = Random.Range(0, meshPool.Length);

        mesh.mesh = meshPool[meshIndex];
        meshCollider.sharedMesh = mesh.mesh;

    }



}

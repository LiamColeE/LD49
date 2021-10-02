using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool locked;
    public bool settled;

    [SerializeField] Mesh[] meshPool;
    [SerializeField] float settledThreshold;

    MeshFilter mesh;
    MeshCollider meshCollider;
    Rigidbody rb;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        int meshIndex = Random.Range(0, meshPool.Length);

        mesh.mesh = meshPool[meshIndex];
        meshCollider.sharedMesh = mesh.mesh;

    }

    void Update()
    {
        settled = Mathf.Abs(rb.velocity.magnitude) < settledThreshold;
    }



}

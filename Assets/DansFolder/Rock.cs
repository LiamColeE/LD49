using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool locked;
    public bool settled;
    public bool rooted;
    public List<Rock> connectedRocks = new List<Rock>();

    [SerializeField] Mesh[] meshPool;
    [SerializeField] float settledThreshold;

    MeshFilter mesh;
    MeshCollider meshCollider;
    Rigidbody rb;
    Material material;
    Color startColor;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
        startColor = material.color;
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

        if(rooted)
        {
            material.color = Color.green;
        }
        else
        {
            material.color = startColor;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            Rock collidedRock = collision.gameObject.GetComponent<Rock>();
            if(collidedRock.rooted)
            {
                connectedRocks.Add(collidedRock);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Root"))
        {
            rooted = true;
        }
        else if(collision.gameObject.CompareTag("Rock"))
        {
            rooted = collision.gameObject.GetComponent<Rock>().rooted == true;
        }
        else
        {
            rooted = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {
            Rock collidedRock = collision.gameObject.GetComponent<Rock>();
            if(connectedRocks.Contains(collidedRock))
            {
                connectedRocks.Remove(collidedRock);
            }
        }
    }





}

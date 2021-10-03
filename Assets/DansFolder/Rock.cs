using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public bool locked;
    public bool settled;

    public bool rooted;
    public bool root;
    public List<Rock> connectedRocks = new List<Rock>();

    

    [SerializeField] Mesh[] meshPool;
    [SerializeField] float settledThreshold;

    MeshFilter mesh;
    MeshCollider meshCollider;
    Rigidbody rb;
    Material material;
    Color startColor;
    ParticleSystem ps;
    ParticleSystemRenderer psr;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        mesh = GetComponent<MeshFilter>();
        rb = GetComponent<Rigidbody>();
        material = GetComponent<MeshRenderer>().material;
        startColor = material.color;
        ps = GetComponent<ParticleSystem>();
        psr = GetComponent<ParticleSystemRenderer>();
    }

    void Start()
    {
        int meshIndex = Random.Range(0, meshPool.Length);

        mesh.mesh = meshPool[meshIndex];
        meshCollider.sharedMesh = mesh.mesh;
        psr.mesh = mesh.mesh;
    }

    void Update()
    {
        settled = Mathf.Abs(rb.velocity.magnitude) < settledThreshold;

        if(root || rooted)
        {
            ps.Play();
            //material.color = Color.green;
        }
        else
        {
            ps.Stop();
            //material.color = startColor;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Rock"))
        {

            Rock collidedRock = collision.gameObject.GetComponent<Rock>();
            connectedRocks.Add(collidedRock);
            
            SetConnectedRocksAsRooted(new List<Rock>(), LookForRoot(new List<Rock>()));
        }

        if(collision.gameObject.CompareTag("Root"))
        {
            root = true;
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
                SetConnectedRocksAsRooted(new List<Rock>(), LookForRoot(new List<Rock>()));
            }
        }

        if(collision.gameObject.CompareTag("Root"))
        {
            root = false;
        }
    }

    public bool LookForRoot(List<Rock> visited)
    {
        foreach(Rock rock in connectedRocks)
        {

            if(!visited.Contains(rock))
            {
                visited.Add(rock);
                if(!rock.root)
                {
                    if(rock.LookForRoot(visited))
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SetConnectedRocksAsRooted(List<Rock> visited, bool rooted)
    {
        foreach(Rock rock in connectedRocks)
        {
            if(!visited.Contains(rock))
            {
                visited.Add(rock);
                rock.rooted = rooted;
                SetConnectedRocksAsRooted(visited, rooted);
            }
        }

        if(connectedRocks.Count == 0)
        {
           this.rooted = rooted;
        }
    }

}

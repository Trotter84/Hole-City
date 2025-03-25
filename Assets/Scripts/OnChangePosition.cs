using System;
using System.Collections;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] allObjects;
    public PolygonCollider2D ground2DCollider;
    public PolygonCollider2D hole2DCollider;
    public MeshCollider generateMeshCollider;
    public Collider groundCollider;
    Mesh generatedMesh;
    public PlayerController playerControllerScript;
    public CapsuleCollider playerCollider;

    [Header("Attributes")]
    public float initialScale = 0.29f;
    public Vector3 currentScale;
    public float speedIncrease = 0.09f;

    [Header("Sizes")]
    string size1 = "Obstacle Size 1";
    string size3 = "Obstacle Size 3";
    string size5 = "Obstacle Size 5";
    string size7 = "Obstacle Size 7";
    string size9 = "Obstacle Size 9";
    string size11 = "Obstacle Size 11";


    void Start()
    {
        // allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        CalculateObjectSize();

        // foreach (var go in allObjects)
        // {
        //     if (go.layer == LayerMask.NameToLayer("Obstacles"))
        //     {
        //         Physics.IgnoreCollision(go.GetComponent<Collider>(), generateMeshCollider, true);
        //     }
        // }
    }

    void Update()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;

            currentScale = transform.localScale * initialScale;

            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);

        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    void Make3DMeshCollider()
    {
        if (generatedMesh != null) Destroy(generatedMesh);

        generatedMesh = ground2DCollider.CreateMesh(true, true);
        generateMeshCollider.sharedMesh = generatedMesh;
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody objectRb = other.GetComponent<Rigidbody>();
        objectRb.isKinematic = false;

        Physics.IgnoreCollision(other, groundCollider, true);
        Physics.IgnoreCollision(other, generateMeshCollider, false);
    }

    void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, groundCollider, false);
        Physics.IgnoreCollision(other, generateMeshCollider, true);
    }

    void OnCollisionEnter(Collision collision)
    {

    }

    void CalculateObjectSize()
    {
        switch (playerControllerScript.currentSize)
        {
            case 1:
                playerCollider.excludeLayers = LayerMask.GetMask(size1);
                break;
            case 3:
                playerCollider.excludeLayers = LayerMask.GetMask(size1, size3);
                break;
            case 5:
                playerCollider.excludeLayers = LayerMask.GetMask(size1, size3, size5);
                break;
            case 7:
                playerCollider.excludeLayers = LayerMask.GetMask(size1, size3, size5, size7);
                break;
            case 9:
                playerCollider.excludeLayers = LayerMask.GetMask(size1, size3, size5, size7, size9);
                break;
            case 11:
                playerCollider.excludeLayers = LayerMask.GetMask(size1, size3, size5, size7, size9, size11);
                break;
        }
    }

    // Hole grow animation.
    public IEnumerator ScaleHole()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2;

        playerControllerScript.currentSize++;

        CalculateObjectSize();

        playerControllerScript.speed += playerControllerScript.speed * speedIncrease;

        float time = 0;

        while (time <= 0.5f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }
    }
}

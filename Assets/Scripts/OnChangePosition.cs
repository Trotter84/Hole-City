using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [Header("Components")]
    public GameObject[] objectSize1;
    public GameObject[] objectSize2;
    public GameObject[] objectSize3;
    public GameObject[] objectSize4;
    public GameObject[] objectSize5;
    public GameObject[] objectSize6;
    public PolygonCollider2D ground2DCollider;
    public PolygonCollider2D hole2DCollider;
    public MeshCollider generateMeshCollider;
    public Collider groundCollider;
    Mesh generatedMesh;
    public PlayerController playerControllerScript;

    [Header("Attributes")]
    public float initialScale = 0.29f;
    public Vector3 currentScale;
    public float speedIncrease = 0.06f;


    void Start()
    {
        // allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        // for (int i = 1; i < 7; i++)
        // {
        //     string objectString = $"size{i}Objects";
        //     (GameObject[])Convert.ChangeType(, typeof(GameObject[]);
        // }

        objectSize1 = GameObject.FindGameObjectsWithTag("Object_Size_1");
        objectSize2 = GameObject.FindGameObjectsWithTag("Object_Size_2");
        objectSize3 = GameObject.FindGameObjectsWithTag("Object_Size_3");
        objectSize4 = GameObject.FindGameObjectsWithTag("Object_Size_4");
        objectSize5 = GameObject.FindGameObjectsWithTag("Object_Size_5");
        objectSize6 = GameObject.FindGameObjectsWithTag("Object_Size_6");


        // foreach (var go in allObjects)
        // {
        //     for (int i = 0; i < 7; i++)
        //     {
        //         gameObjectTags.Add(i, go.tag);

        //     }
        //     if (go.layer == LayerMask.NameToLayer("Obstacles"))
        //     {
        //         go.CompareTag("");
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
        //TODO: Set up Layer changer.

    }

    // Hole grow animation.
    public IEnumerator ScaleHole()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2;

        playerControllerScript.currentSize++;

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

using System.Collections;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    [Header("Components")]
    public PolygonCollider2D ground2DCollider;
    public PolygonCollider2D hole2DCollider;
    public MeshCollider GenerateMeshCollider;
    public Collider GroundCollider;
    Mesh GeneratedMesh;

    [Header("Attributes")]
    public float initialScale = 0.6f;


    void Start()
    {
        GameObject[] AllGOs = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (var go in AllGOs)
        {
            if (go.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(go.GetComponent<Collider>(), GenerateMeshCollider, true);
            }
        }
    }

    void Update()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2DCollider.transform.localScale = transform.localScale * initialScale;

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
        if (GeneratedMesh != null) Destroy(GeneratedMesh);

        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GenerateMeshCollider.sharedMesh = GeneratedMesh;
    }

    void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other, GroundCollider, true);
        Physics.IgnoreCollision(other, GenerateMeshCollider, false);
    }

    void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, GroundCollider, false);
        Physics.IgnoreCollision(other, GenerateMeshCollider, true);
    }

    // Hole grow animation.
    public IEnumerator ScaleHole()
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = startScale * 2;

        float time = 0;

        while (time <= 0.4f)
        {
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }
    }
}

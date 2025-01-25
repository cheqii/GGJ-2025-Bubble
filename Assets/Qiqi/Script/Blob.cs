﻿using UnityEngine;

public class Blob : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public int referencePointsCount = 12;
    public float referencePointRadius = 0.25f;
    public float mappingDetail = 10;
    public float springDampingRatio = 0;
    public float springFrequency = 2;
    public PhysicsMaterial2D surfaceMaterial;
    public Rigidbody2D[] allReferencePoints;
    GameObject[] referencePoints;
    int vertexCount;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uv;
    Vector3[,] offsets;
    float[,] weights;

    void Start()
    {
        // CreateReferencePoints();
        // CreateMesh();
        // MapVerticesToReferencePoints();

    }



    public void CreateReferencePoints()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        referencePoints = new GameObject[referencePointsCount];
        Vector3 offsetFromCenter = ((0.5f - referencePointRadius) * Vector3.up);
        float angle = 360.0f / referencePointsCount;

        for (int i = 0; i < referencePointsCount; i++)
        {
            referencePoints[i] = new GameObject();
            referencePoints[i].tag = gameObject.tag;
            referencePoints[i].layer = LayerMask.NameToLayer("Attractable");
            referencePoints[i].transform.parent = transform;

            Quaternion rotation =
                Quaternion.AngleAxis(angle * (i - 1), Vector3.back);
            referencePoints[i].transform.localPosition =
                rotation * offsetFromCenter;

            Rigidbody2D body = referencePoints[i].AddComponent<Rigidbody2D>();
            body.constraints = RigidbodyConstraints2D.None;
            body.mass = 0.5f;
            body.interpolation = rigidbody.interpolation;
            body.collisionDetectionMode = rigidbody.collisionDetectionMode;
            allReferencePoints[i] = body;


            CircleCollider2D collider =
                referencePoints[i].AddComponent<CircleCollider2D>();
            collider.radius = referencePointRadius * (transform.localScale.x / 2);
            if (surfaceMaterial != null)
            {
                collider.sharedMaterial = surfaceMaterial;
            }

            AttachWithSpringJoint(referencePoints[i], gameObject);
            if (i > 0)
            {
                AttachWithSpringJoint(referencePoints[i],
                        referencePoints[i - 1]);
            }
        }
        AttachWithSpringJoint(referencePoints[0],
                referencePoints[referencePointsCount - 1]);

        IgnoreCollisionsBetweenReferencePoints();
    }

    public void AttachWithSpringJoint(GameObject referencePoint,
            GameObject connected)
    {
        SpringJoint2D springJoint =
            referencePoint.AddComponent<SpringJoint2D>();
        springJoint.connectedBody = connected.GetComponent<Rigidbody2D>();
        springJoint.connectedAnchor = LocalPosition(referencePoint) -
            LocalPosition(connected);
        springJoint.distance = 0;
        springJoint.dampingRatio = springDampingRatio;
        springJoint.frequency = springFrequency;
    }

    public void IgnoreCollisionsBetweenReferencePoints()
    {
        int i;
        int j;
        CircleCollider2D a;
        CircleCollider2D b;

        for (int _i = 0; _i < referencePointsCount; _i++)
        {
            for (int _j = _i; _j < referencePointsCount; _j++)
            {
                a = referencePoints[_i].GetComponent<CircleCollider2D>();
                b = referencePoints[_j].GetComponent<CircleCollider2D>();
                Physics2D.IgnoreCollision(a, b, true);
            }
        }
    }

    public void CreateMesh()
    {
        vertexCount = (width + 1) * (height + 1);

        int trianglesCount = width * height * 6;
        vertices = new Vector3[vertexCount];
        triangles = new int[trianglesCount];
        uv = new Vector2[vertexCount];
        int t;

        for (int y = 0; y <= height; y++)
        {
            for (int x = 0; x <= width; x++)
            {
                int v = (width + 1) * y + x;
                vertices[v] = new Vector3(x / (float)width - 0.5f,
                        y / (float)height - 0.5f, 0);
                uv[v] = new Vector2(x / (float)width, y / (float)height);

                if (x < width && y < height)
                {
                    t = 3 * (2 * width * y + 2 * x);

                    triangles[t] = v;
                    triangles[++t] = v + width + 1;
                    triangles[++t] = v + width + 2;
                    triangles[++t] = v;
                    triangles[++t] = v + width + 2;
                    triangles[++t] = v + 1;
                }
            }
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void MapVerticesToReferencePoints()
    {
        offsets = new Vector3[vertexCount, referencePointsCount];
        weights = new float[vertexCount, referencePointsCount];

        for (int i = 0; i < vertexCount; i++)
        {
            float totalWeight = 0;

            for (int j = 0; j < referencePointsCount; j++)
            {
                offsets[i, j] = vertices[i] - LocalPosition(referencePoints[j]);
                weights[i, j] =
                    1 / Mathf.Pow(offsets[i, j].magnitude, mappingDetail);
                totalWeight += weights[i, j];
            }

            for (int j = 0; j < referencePointsCount; j++)
            {
                weights[i, j] /= totalWeight;
            }
        }
    }

    void Update()
    {
        UpdateVertexPositions();
    }

    private void UpdateVertexPositions()
    {
        if(vertexCount <= 0) return;
        Vector3[] vertices = new Vector3[vertexCount];

        for (int i = 0; i < vertexCount; i++)
        {
            vertices[i] = Vector3.zero;

            for (int j = 0; j < referencePointsCount; j++)
            {
                vertices[i] += weights[i, j] *
                    (LocalPosition(referencePoints[j]) + offsets[i, j]);
            }
        }

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
    }

    private Vector3 LocalPosition(GameObject obj)
    {
        return transform.InverseTransformPoint(obj.transform.position);
    }



    public void TrigThis()
    {
        int z = 0;
        foreach (Rigidbody2D child in allReferencePoints)
        {
            allReferencePoints[z].constraints = RigidbodyConstraints2D.None;
            z++;
        }
    }


}


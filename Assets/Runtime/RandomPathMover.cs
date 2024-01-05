using System.Collections.Generic;
using UnityEngine;

public class CurvedPathMover : MonoBehaviour
{
    public float speed = 5.0f;
    private List<Vector3> pathPoints;
    private int currentPointIndex = 0;

    void Start()
    {
        GenerateBezierPath();
    }

    void Update()
    {
        if (pathPoints.Count > 0)
        {
            MoveAlongPath();
        }
    }

    void GenerateBezierPath()
    {
        // Generate 4 random points for a simple Bezier curve
        Vector3 startPoint = transform.position;
        Vector3 controlPoint1 = RandomPoint();
        Vector3 controlPoint2 = RandomPoint();
        Vector3 endPoint = RandomPoint();

        pathPoints = new List<Vector3>();
        for (float t = 0; t <= 1; t += 0.05f) // Adjust the step for smoother/rougher paths
        {
            Vector3 pathPoint = CalculateBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
            pathPoints.Add(pathPoint);
        }
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0;
        point += 3 * uu * t * p1;
        point += 3 * u * tt * p2;
        point += ttt * p3;

        return point;
    }

    void MoveAlongPath()
    {
        if (currentPointIndex >= pathPoints.Count) currentPointIndex = 0;

        Vector3 targetPoint = pathPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint) < 0.001f)
        {
            currentPointIndex++;
            if (currentPointIndex < pathPoints.Count)
            {
                LookAtPathDirection(pathPoints[currentPointIndex]);
            }
        }
    }

    void LookAtPathDirection(Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    Vector3 RandomPoint()
    {
        return new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
    }
}

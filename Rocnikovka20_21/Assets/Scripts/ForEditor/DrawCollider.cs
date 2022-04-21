using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
public class DrawCollider : MonoBehaviour
{
    public PolygonCollider2D polygonCollider2D { get { return m_PolygonCollider2D; } }
    PolygonCollider2D m_PolygonCollider2D;

    [SerializeField] bool alwaysShowCollider;

    public Color color = Color.blue;

    void Awake()
    {
        m_PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void OnDrawGizmos()
    {
        if (alwaysShowCollider)
        {
            Vector2[] points = m_PolygonCollider2D.points;
            Gizmos.color = color;

            for (int i = 0; i < points.Length - 1; i++)
            {
                GizmosUtil.DrawLocalLine(transform, (Vector3)points[i], (Vector3)points[i + 1]);
            }
            
            GizmosUtil.DrawLocalLine(transform, (Vector3)points[points.Length - 1], (Vector3)points[0]);
        }
    }
}

public static class GizmosUtil
{
    public static void DrawLocalLine(Transform tr, Vector3 p1, Vector3 p2)
    {
        Gizmos.DrawLine(tr.TransformPoint(p1), tr.TransformPoint(p2));
    }

}

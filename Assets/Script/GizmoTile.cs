using System;
using UnityEditor;
using UnityEngine;

namespace Script
{
    public class GizmoTile : MonoBehaviour
    {
        public float gizmoLength = 1.0f;
        public float gizmoRadius = 0.5f;
        public Color gizmoColor = Color.green;

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            
            DrawArrowGizmo();
        }


        private void DrawArrowGizmo()
        {
            var transform1 = transform;
            var position = transform1.position;
            var UP = transform1.up;
            Gizmos.DrawLine(position, position + UP * gizmoLength);
            Vector3 size = new Vector3(0.1f, 0.1f, 0);
            Gizmos.DrawCube(position, size);
            Vector3 right = Quaternion.LookRotation(UP) * Quaternion.Euler(0, 45, 0) * Vector3.forward * gizmoRadius;
            Vector3 left = Quaternion.LookRotation(UP) * Quaternion.Euler(0, -45, 0) * Vector3.forward * gizmoRadius;
            
            Gizmos.DrawSphere(position + UP * gizmoLength, 0.1f);

            // Gizmos.DrawLine(position + UP * gizmoLength, position + UP * gizmoLength - right);
            // Gizmos.DrawLine(position + UP * gizmoLength, position + UP * gizmoLength - left);
        }
    }
}
using UnityEngine;

namespace Script
{
    public class GridPos : MonoBehaviour
    {
    }

    public enum GridPosition : byte
    {
        none = 10,
        ButtomLeft = 0,
        ButtomCenter = 1,
        ButtomRight = 2,
        MiddelLeft = 3,
        MiddelCenter = 4,
        MiddelRight = 5,
        TopLeft = 6,
        TopCenter = 7,
        TopRight = 8
    }
}
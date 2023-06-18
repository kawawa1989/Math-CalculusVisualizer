using System.Collections.Generic;
using UnityEngine;


public class DifferentialGraph : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    private List<Vector3> _positions = new List<Vector3>();

    public void Initialize(Dictionary<int, float> pointToTangent)
    {
        for (float x = -10.00f; x <= 10; x += 0.01f)
        {
            int xPos = (int)(x * 100.0f);
            float tan = pointToTangent[xPos];
            Vector3 pos = new Vector3(x, tan, 0);
            _positions.Add(pos);
        }

        _lineRenderer.positionCount = _positions.Count;
        _lineRenderer.SetPositions(_positions.ToArray());
    }
}
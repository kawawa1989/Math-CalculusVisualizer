using System;
using System.Collections.Generic;
using UnityEngine;

public class TangentInfo
{
    private Dictionary<int, float> _pointToTangent = new();
}

public class FunctionGraph : MonoBehaviour
{
    [SerializeField] private DifferentialGraph _differentialGraph;
    private LineRenderer _lineRenderer;
    // 座標を四元数で演算するために Quaternion 型でベクトル部分のみを持ったオブジェクトを作る
    private List<Vector3> _positions = new List<Vector3>();
    public Dictionary<int, float> PointToTangent = new();

    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    //public Func<float, float> Func = x => (float)Math.Exp(x);    // e^x
    public Func<float, float> Func = x => x * x * x;             // x^3 
    //public Func<float, float> Func = x => x == 0 ? 0 : 1.0f / x; // x^-1
    //public Func<float, float> Func = x => x * x;                 // x^2
    //public Func<float, float> Func = x => Mathf.Cos(x);
    //public Func<float, float> Func = x => x;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PointToTangent = new Dictionary<int, float>();
        for (float x = -10.00f; x <= 10; x += 0.01f)
        {
            float dx = 0.001f;
            float y1 = Func(x);
            float y2 = Func(x + dx);

            Vector3 p1 = new Vector3(x, y1, 0);
            Vector3 p2 = new Vector3(x + dx, y2, 0);
            _positions.Add(p1);

            Vector3 v = p2 - p1;
            int xPos = (int)(x * 100.0f);
            float tan = v.y / v.x;
            PointToTangent[xPos] = tan;
        }

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _positions.Count;
        _lineRenderer.SetPositions(_positions.ToArray());
        _differentialGraph.Initialize(PointToTangent);
    }
}

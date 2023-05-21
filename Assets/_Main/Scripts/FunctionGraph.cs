using System;
using System.Collections;
using System.Collections.Generic;
using MathematicsExpression;
using UnityEngine;

public class FunctionGraph : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    // 座標を四元数で演算するために Quaternion 型でベクトル部分のみを持ったオブジェクトを作る
    private List<Vector3> _positions = new List<Vector3>();
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    //public Func<float, float> Func = x => (float)Math.Exp(x);    // e^x
    //public Func<float, float> Func = x => x * x * x;             // x^3 
    public Func<float, float> Func = x => x == 0 ? 0 : 1.0f / x; // x^-1
    //public Func<float, float> Func = x => x * x;                 // x^2
    //public Func<float, float> Func = x => Mathf.Cos(x);
    //public Func<float, float> Func = x => x;

    // Start is called before the first frame update
    void Start()
    {
        int precision = 1000;
        for (int t = 0; t <= precision; ++t)
        {
            float x = Mathf.Lerp(-10, 10, t / (float) precision);
            Vector3 pos = new Vector3(x, Func(x), 0);
            _positions.Add(pos);
        }

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _positions.Count;
        _lineRenderer.SetPositions(_positions.ToArray());

        for (int i = 0; i < _positions.Count - 1; ++i)
        {
            float dx = _positions[i + 1].x - _positions[i].x;
            float dy = _positions[i + 1].y - _positions[i].y;
            float t = dy / dx;
            //Debug.Log($"[x: {_positions[i].x}] 傾き: {t}");
        }
    }
}

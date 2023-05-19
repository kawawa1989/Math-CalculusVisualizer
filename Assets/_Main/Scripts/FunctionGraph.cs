using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionGraph : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    // 座標を四元数で演算するために Quaternion 型でベクトル部分のみを持ったオブジェクトを作る
    private List<Vector3> _positions = new List<Vector3>();
    // ReSharper disable once FieldCanBeMadeReadOnly.Global
    //public Func<float, float> Func = x => (float)Math.Exp(x);    // e^x
    //public Func<float, float> Func = x => x * x * x;             // x^3 
    //public Func<float, float> Func = x => x == 0 ? 0 : 1.0f / x; // x^-1
    public Func<float, float> Func = x => x * x; // 


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
            Debug.Log($"[x: {_positions[i].x}] 傾き: {t}");
        }
    }

    private const float rad2deg = (Mathf.PI / 180f);
    private readonly Vector3 _yAxis = Vector3.up;
    private float _rotY = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _rotY += 1;
            Debug.Log($"Rot!! rowY:{_rotY}");
            float theta = rad2deg * _rotY;
            float sin = Mathf.Sin(theta * 0.5f);
            float cos = Mathf.Cos(theta * 0.5f);
            Vector3 n = _yAxis * sin;
            // w(cos θ/2) + (i + j + k)(sin θ/2)
            var q = new Quaternion(n.x, n.y, n.z, cos);
            // 共役クォータニオン
            var qConjugate = new Quaternion(-q.x, -q.y, -q.z, q.w);
            
            // qpq* で回転後の座標を求めることができる
            transform.rotation = q;
            for (int i = 0; i < _positions.Count; ++i)
            {
                //_lineRenderer.SetPosition(i, new Vector3(r.x, r.y, r.z));
            }
        }
    }
}

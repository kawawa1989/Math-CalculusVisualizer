using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class IntegralModel
{
    private List<Vector3> _verticies = new List<Vector3>();
    private List<int> _triangles = new List<int>();
    public List<Vector3> Verticies => _verticies;
    public List<int> Triangles => _triangles;

    public int Precision { get; set; } = 10000;
    public float Start { get; set; } = 0;
    public float End { get; set; } = 0;
    public float SumArea { get; set; } = 0;


    /// <summary>
    /// 関数 f(x) を積分する。
    /// </summary>
    /// <param name="f"></param>
    public void Integrate(Func<float, float> f)
    {
        float x1 = Start;
        Vector3 v0 = new Vector3(x1, 0, 0);
        _verticies.Add(v0);
        float sumArea = 0;
        int b = 0;
        int t = 1;
        
        // 一回のループでx ~ x+dx を範囲とした四角形を一つ作る
        while (t <= Precision)
        {
            float x2 = Mathf.Lerp(Start, End, t++ / (float) Precision);
            float y = f(x1);
            Vector3 v1 = new Vector3(x1, y, 0);
            Vector3 v2 = new Vector3(x2, y, 0);
            Vector3 v3 = new Vector3(x2, 0, 0);

            _triangles.Add(0 + b);
            _triangles.Add(1 + b);
            _triangles.Add(3 + b);
            _triangles.Add(2 + b);
            _triangles.Add(3 + b);
            _triangles.Add(1 + b);
            _verticies.Add(v1);
            _verticies.Add(v2);
            _verticies.Add(v3);

            b += 3;
            float dx = x2 - x1;
            sumArea += dx * y;
            x1 = x2;
        }

        SumArea = sumArea;
    }
    
    /// <summary>
    /// 台形法を用いて関数 f(x) を積分する。
    /// </summary>
    /// <param name="f"></param>
    public void TrapezoidalIntegrate(Func<float, float> f)
    {
        float x1 = Start;
        Vector3 v0 = new Vector3(x1, 0, 0);
        _verticies.Add(v0);
        float sumArea = 0;
        int b = 0;
        int t = 1;
        
        // 一回のループでx ~ x+dx を範囲とした四角形を一つ作る
        while (t <= Precision)
        {
            /*                  y2 (botom)
             *               ----|
             *           ----    |
             *   y1(top)-        |
             *   |---            |
             *   |               | 
             *   |               |
             *   |               |
             *   |---------------|
             *  x1               x2
             *         height    
             */
            float x2 = Mathf.Lerp(Start, End, t++ / (float) Precision);
            float y = f(x1);
            float y2 = f(x2);
            Vector3 v1 = new Vector3(x1, y, 0);
            Vector3 v2 = new Vector3(x2, y2, 0);
            Vector3 v3 = new Vector3(x2, 0, 0);

            float height = x2 - x1;
            float area = (y2 + y) * height / 2;

            _triangles.Add(0 + b);
            _triangles.Add(1 + b);
            _triangles.Add(3 + b);
            _triangles.Add(2 + b);
            _triangles.Add(3 + b);
            _triangles.Add(1 + b);
            _verticies.Add(v1);
            _verticies.Add(v2);
            _verticies.Add(v3);

            b += 3;
            sumArea += area;
            x1 = x2;
        }

        SumArea = sumArea;
    }
}
    

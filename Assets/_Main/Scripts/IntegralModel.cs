using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class IntegralModel
{
    /// <summary>
    /// 積分区間
    /// </summary>
    public class Section
    {
        private int _start;
        private int _end;
        private int Center { get; }
        private int VertexBufferLength { get; }
        private int AllSqrCount => VertexBufferLength / 3;

        public int End
        {
            set => _end = value * 3;
            get => _end;
        }

        public int Start
        {
            set => _start = value * 3;
            get => _start;
        }

        public Section(int center, int vertexLength)
        {
            Center = center;
            End = 0;
            VertexBufferLength = vertexLength;
            Debug.Log($"Center: {Center}, \n" + $"VertexBufferLength:{VertexBufferLength}(mod {VertexBufferLength % 3}), \n" + $"VertexLength: {VertexLength}, \n" + $"SqrCount: {SqrCount}, AllSqrCount: {AllSqrCount}");
        }

        public float CalcSumArea(Vector3[] verticies)
        {
            float sum = 0;
            for (int i = VertexStart; i < VertexStart + VertexLength; i += 3)
            {
                Vector3 v1 = verticies[i + 1];
                Vector3 v2 = verticies[i + 2];
                float dx = v2.x - v1.x;
                float y = v1.y;
                sum += dx * y;
            }

            return sum;
        }

        public int VertexStart => Start + Center;
        public int VertexLength => (End - Start) + 1;
        public int SqrCount => VertexLength / 3;
    }

    private Vector3[] _verticies;
    private List<int> _triangles = new List<int>();
    public Vector3[] Verticies => _verticies;
    public List<int> Triangles => _triangles;


    /// <summary>
    /// 関数 ∫[start ... end] f(x) を積分する。
    /// </summary>
    /// <param name="f"></param>
    /// <param name="range"></param>
    public Section Integrate(Func<float, float> f, int range)
    {
        int sqrCount = (range * 2);
        int precision = sqrCount * 100;
        int vertexBufferLength = (precision * 3) + 1;
        int start = -range;
        int end = range;
        int vi = 0;

        float x1 = start;
        Vector3 v0 = new Vector3(x1, 0, 0);
        _verticies = new Vector3[vertexBufferLength];
        _verticies[vi++] = v0;
        float sumArea = 0;
        int b = 0;
        int t = 1;

        // 一回のループでx ~ x+dx を範囲とした四角形を一つ作る
        while (t <= precision)
        {
            float x2 = Mathf.Lerp(start, end, t++ / (float)precision);
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
            _verticies[vi++] = v1;
            _verticies[vi++] = v2;
            _verticies[vi++] = v3;

            b += 3;
            float dx = x2 - x1;
            sumArea += dx * y;
            x1 = x2;
        }

        return new Section(precision / 2 * 3, _verticies.Length);
    }

    /*
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
            //                  y2 (botom)
            //               ----|
            //           ----    |
            //   y1(top)-        |
            //   |---            |
            //   |               | 
            //   |               |
            //   |               |
            //   |---------------|
            //  x1               x2
            //         height    
            //
            float x2 = Mathf.Lerp(Start, End, t++ / (float)Precision);
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
    */
}
    

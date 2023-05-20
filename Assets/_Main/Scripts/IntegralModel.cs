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
        private IntegralModel _this;
        private int _start;
        private int _vertexStart;
        private int _end;
        private int _vertexEnd;
        private int _center;
        private int _vertexCenter;
        private int VertexBufferLength { get; }
        private int AllSqrCount => VertexBufferLength / 3;

        public int End
        {
            set
            {
                _end = value;
                _vertexEnd = _end * 3;
            }
            get => _end;
        }

        public int Start
        {
            set
            {
                _start = value;
                _vertexStart = _start * 3;
            }
            get => _start;
        }

        public int Center
        {
            set
            {
                _center = value;
                _vertexCenter = _center * 3;
            }
            get => _center;
        }


        public Section(int center, int vertexLength, IntegralModel @this)
        {
            _this = @this;
            Center = center;

            End = 0;
            VertexBufferLength = vertexLength;
            Debug.Log($"Center: {Center}(vertex: {_vertexCenter}), \n" + $"VertexBufferLength:{VertexBufferLength}(mod {VertexBufferLength % 3}), \n" + $"VertexLength: {VertexLength}, \n" + $"SqrCount: {SqrCount}, AllSqrCount: {AllSqrCount}");
        }

        public float CalcSumArea()
        {
            int start = Center + Start;
            int len = End - Start;
            Debug.Log($"start:{start}, end:{End}, len:{len}, Areas[{_this.Areas.Count}]");

            float sum = 0;
            for (int i = start; i < start + len; i++)
            {
                sum += _this.Areas[i];
            }

            return sum;
        }

        public int VertexStart => _vertexStart + _vertexCenter;
        public int VertexLength => (_vertexEnd - _vertexStart) + 1;
        public int SqrCount => VertexLength / 3;
    }

    private Vector3[] _verticies;
    private List<int> _triangles = new List<int>();
    public Vector3[] Verticies => _verticies;
    public List<int> Triangles => _triangles;
    public List<float> Areas { get; } = new List<float>();


    /// <summary>
    /// 関数 ∫[start ... end] f(x) を積分する。
    /// </summary>
    /// <param name="f"></param>
    /// <param name="range"></param>
    public Section Integrate(Func<float, float> f, int range)
    {
        Areas.Clear();
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

            float dx = v2.x - v1.x;
            Areas.Add(dx * y);
            b += 3;
            x1 = x2;
        }

        return new Section(precision / 2, _verticies.Length, this);
    }


    /// <summary>
    /// 台形法を用いて関数 f(x) を積分する。
    /// </summary>
    /// <param name="f"></param>
    public Section TrapezoidalIntegrate(Func<float, float> f, int range)
    {
        Areas.Clear();
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
        int b = 0;
        int t = 1;

        // 一回のループでx ~ x+dx を範囲とした四角形を一つ作る
        while (t <= precision)
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
            float x2 = Mathf.Lerp(start, end, t++ / (float)precision);
            float y = f(x1);
            float y2 = f(x2);
            Vector3 v1 = new Vector3(x1, y, 0);
            Vector3 v2 = new Vector3(x2, y2, 0);
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

            float height = x2 - x1;
            float area = (y2 + y) * height / 2;
            Areas.Add(area);
            b += 3;
            x1 = x2;
        }

        return new Section(precision / 2, _verticies.Length, this);
    }
}
    

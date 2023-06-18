using System;
using System.Collections.Generic;
using UnityEngine;

public class FunctionModel
{
    //public Func<float, float> Func = x => (float)Math.Exp(x);    // e^x
    //public Func<float, float> Func = x => x == 0 ? 0 : 1.0f / x; // x^-1
    //public Func<float, float> Func = x => x * x;                 // x^2
    //public Func<float, float> Func = x => Mathf.Cos(x);
    //public Func<float, float> Func = x => x;
    public Func<float, float> F = x => x * x * x; // x^3 
    private readonly Dictionary<int, float> _tangents = new();
    private readonly List<Vector3> _positions = new List<Vector3>();

    public List<Vector3> Positions => _positions;
    public Dictionary<int, float> Tangents => _tangents;

    public void Initialize(float start, float end, float unit)
    {
        _tangents.Clear();
        for (float x = start; x <= end; x += unit)
        {
            float dx = 0.001f;
            float y1 = F(x);
            float y2 = F(x + dx);

            Vector3 p1 = new Vector3(x, y1, 0);
            Vector3 p2 = new Vector3(x + dx, y2, 0);
            _positions.Add(p1);

            Vector3 v = p2 - p1;
            int xPos = (int)(x * 100.0f);
            float tan = v.y / v.x;
            _tangents[xPos] = tan;
        }        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class IntegralGraph : MonoBehaviour
{
    [SerializeField] private FunctionGraph _functionGraph;
    [SerializeField] private TextMeshProUGUI _sumAreaText;
    [SerializeField] private Slider _slider;
    
    private Mesh _mesh;
    private readonly IntegralModel _integral = new IntegralModel();
    
    // Start is called before the first frame update
    void Start()
    {
        _integral.Start = 0;
        _integral.End = 5;
        _integral.Precision = 10;
        _integral.Integrate(_functionGraph.Func);
        
        
        _sumAreaText.text = $"Area: {_integral.SumArea}";
        _mesh = new Mesh();
        _mesh.SetVertices(_integral.Verticies, 9, _integral.Verticies.Count - 9);
        _mesh.SetTriangles(_integral.Triangles, 0, _integral.Triangles.Count - 18, 0);
        _mesh.RecalculateNormals();
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _mesh;
    }

    private void Update()
    {
        /*
        int valueRate = (int) ((_slider.value / _slider.maxValue) * 10000);
        int vertLength = (int) Math.Clamp((4 * valueRate), 0, _integral.Verticies.Count / 2);
        int triangleLength = (int) Math.Clamp((3 * valueRate), 0, _integral.Triangles.Count / 2);
        _mesh.SetVertices(_integral.Verticies, 0, vertLength);
        _mesh.SetTriangles(_integral.Triangles, 0, triangleLength, 0);
        */
    }

    public float SnapToTarget(float value, float targetValue, float epsilon)
    {
        if (Math.Abs(value - targetValue) < epsilon)
        {
            return targetValue;
        }
        return value;
    }
}

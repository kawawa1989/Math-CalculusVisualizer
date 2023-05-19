using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class IntegralGraph : MonoBehaviour
{
    [SerializeField] private FunctionGraph _functionGraph;
    [SerializeField] private TextMeshProUGUI _sumAreaText;
    
    private Mesh _mesh;
    private IntegralModel _integral = new IntegralModel();
    
    // Start is called before the first frame update
    void Start()
    {
        _integral.Start = 1;
        _integral.End = 3;
        _integral.Precision = 20000;
        _integral.Integrate(_functionGraph.Func);
        
        _sumAreaText.text = $"Area: {_integral.SumArea}";
        _mesh = new Mesh();
        _mesh.SetVertices(_integral.Verticies);
        _mesh.SetTriangles(_integral.Triangles, 0);
        _mesh.RecalculateNormals();
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _mesh;
    }
}

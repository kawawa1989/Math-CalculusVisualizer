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
    private readonly FunctionModel _funcModel = new FunctionModel();
    public FunctionModel FuncModel => _funcModel;

    // Start is called before the first frame update
    void Start()
    {
        _funcModel.Initialize(-10, 10, 0.01f);
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _funcModel.Positions.Count;
        _lineRenderer.SetPositions(_funcModel.Positions.ToArray());
        _differentialGraph.Initialize(_funcModel.Tangents);
    }
}

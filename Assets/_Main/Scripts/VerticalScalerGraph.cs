using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class VerticalScalerGraph : MonoBehaviour
{
    private VerticalScalerModel _scaler;
    private Mesh _mesh;

    private void Start()
    {
        _scaler = new VerticalScalerModel();
        _scaler.Initialize();
        
        _mesh = new Mesh();
        
        _mesh.SetVertices(_scaler.Verticies);
        _mesh.SetTriangles(_scaler.Triangles, 0);
        _mesh.RecalculateNormals();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _mesh;
    }

    private void OnDestroy()
    {
        _scaler.Dispose();
    }
}
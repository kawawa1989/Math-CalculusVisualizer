using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class HorizontalScalerGraph : MonoBehaviour
{
    private HorizontalScalerModel _horizontalScaler;
    [SerializeField]
    private Mesh _mesh;

    private void Start()
    {
        _horizontalScaler = new HorizontalScalerModel();
        _horizontalScaler.Initialize();
        
        _mesh = new Mesh();
        _mesh.SetVertices(_horizontalScaler.Verticies);
        _mesh.SetTriangles(_horizontalScaler.Triangles, 0);
        _mesh.RecalculateNormals();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _mesh;
        
//#if UNITY_EDITOR
//        AssetDatabase.CreateAsset(meshFilter.mesh, "Assets/scaler_mesh.asset");
//        AssetDatabase.SaveAssets();
//#endif
    }

    private void OnDestroy()
    {
        _horizontalScaler.Dispose();
    }
}
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class IntegralGraph : MonoBehaviour
{
    [SerializeField] private FunctionGraph _functionGraph;
    [SerializeField] private TextMeshProUGUI _sumAreaText;
    [SerializeField] private TMP_InputField _sectionEndInput;

    private Mesh _mesh;
    private readonly IntegralModel _integral = new IntegralModel();
    private IntegralModel.Section _section;

    
    // Start is called before the first frame update
    void Start()
    {
        _section = _integral.Integrate(_functionGraph.Func, 10);
        _mesh = new Mesh();
        _mesh.SetVertices(_integral.Verticies, _section.VertexStart, _section.VertexLength);
        _mesh.SetTriangles(_integral.Triangles, 0, _section.SqrCount * 6, 0);
        _mesh.RecalculateNormals();
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = _mesh;
    }

    private int _end;

    private void Update()
    {
        if (double.TryParse(_sectionEndInput.text, out var sectionEnd))
        {
            int toIntSectionEnd = (int)(sectionEnd * 100);
            if (toIntSectionEnd != _end)
            {
                _section.End = toIntSectionEnd;
                _mesh.Clear();
                _mesh.SetVertices(_integral.Verticies, _section.VertexStart, _section.VertexLength);
                _mesh.SetTriangles(_integral.Triangles, 0, _section.SqrCount * 6, 0);
                _sumAreaText.text = $"Area: {_section.CalcSumArea(_integral.Verticies)}";
            }

            _end = toIntSectionEnd;
        }
    }
}

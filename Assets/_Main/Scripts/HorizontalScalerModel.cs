using System;
using Unity.Collections;
using UnityEngine;

public class HorizontalScalerModel : IDisposable
{
    private const int vertexPerUnit = 4;

    private const int trianglePerUnit = 6;
    // 1単位10目盛り
    private const int scaleCountPerUnit = 10;
    // 最大 ±10目盛りまで
    private const int scalerBufferRange = 10;
    
    // 1目盛り4頂点、1単位10目盛り、最大 ±50まで
    private const int vertexBufferSize = vertexPerUnit * scaleCountPerUnit * scalerBufferRange * 2;
    // 1目盛り4頂点、1単位10目盛り、最大 ±50まで
    private const int triangleBufferSize = trianglePerUnit * scaleCountPerUnit * scalerBufferRange * 2;
    
    public NativeArray<Vector3> Verticies;
    public readonly int[] Triangles;
    private float _scaleRate;
    private float _width = 0.01f;
    private float _height = 0.2f;

    public HorizontalScalerModel()
    {
        Verticies = new NativeArray<Vector3>(vertexBufferSize, Allocator.Persistent);
        Triangles = new int[triangleBufferSize];
    }


    // 配列の中心位置
    private int _centerPoint;

    public void Initialize()
    {
        // 目盛りの単位
        // 1目盛り を A としたとき
        // A = { ±(|n| + 0.1), ±(|n| + 0.2), ±(|n| + 0.3), ±(|n| + 0.4), ±(|n| + 0.5), ±(|n| + 0.6), ±(|n| + 0.7), ±(|n| + 0.8), ±(|n| + 0.9), ±(|n| + 1.0) }
        // の20個の要素を持った集合として表すことが出来る。
        //
        // マイナス値も要素として格納したいので配列長 arraySize は以下の式で表せる
        // arraySize = orderScaleRange * len(A)
        //
        // 目盛りの中心位置は以下の式で表すことができる。
        // ある目盛り index を参照する場合、以下のようになる。
        // index = _centerPoint + (orderValue * len(A) * 0.5)
        // 目盛りの範囲(start, end) を取得する場合は以下のようになる。
        // (start, end) = _centerPoint + ( ± orderValue * len(A) )

        float posX = 0.1f;
        for (int i = 0; i < 100; ++i)
        {
            int vi = 8 * i;
            int ti = 12 * i;
            float scaleHeight = _height;
            
            if ((i + 1) % 10 == 0)
            {
                scaleHeight += 0.2f;
            }
            else if ((i + 1) % 5 == 0)
            {
                scaleHeight += 0.1f;
            }

            SetVertex(vi, ti, posX, scaleHeight);
            SetVertex(vi + 4, ti + 6, -posX, scaleHeight);
            posX += 0.1f;
        }
    }

    private void SetVertex(int vi, int ti, float x, float height)
    {
        Verticies[vi] = new Vector3(x, 0, 0);
        Verticies[vi + 1] = new Vector3(x + _width, 0, 0);
        Verticies[vi + 2] = new Vector3(x, height, 0);
        Verticies[vi + 3] = new Vector3(x + _width, height, 0);

        Triangles[ti] = vi + 1;
        Triangles[ti + 1] = vi + 0;
        Triangles[ti + 2] = vi + 2;
        Triangles[ti + 3] = vi + 2;
        Triangles[ti + 4] = vi + 3;
        Triangles[ti + 5] = vi + 1;
    }


    public void Dispose()
    {
        Verticies.Dispose();
    }
}




public class VerticalScalerModel : IDisposable
{
    private const int vertexPerUnit = 4;

    private const int trianglePerUnit = 6;
    // 1単位10目盛り
    private const int scaleCountPerUnit = 10;
    // 最大 ±10目盛りまで
    private const int scalerBufferRange = 10;
    
    // 1目盛り4頂点、1単位10目盛り、最大 ±50まで
    private const int vertexBufferSize = vertexPerUnit * scaleCountPerUnit * scalerBufferRange * 2;
    // 1目盛り4頂点、1単位10目盛り、最大 ±50まで
    private const int triangleBufferSize = trianglePerUnit * scaleCountPerUnit * scalerBufferRange * 2;
    
    public NativeArray<Vector3> Verticies;
    public readonly int[] Triangles;
    private float _scaleRate;
    private float _width = 0.01f;
    private float _height = 0.2f;

    public VerticalScalerModel()
    {
        Verticies = new NativeArray<Vector3>(vertexBufferSize, Allocator.Persistent);
        Triangles = new int[triangleBufferSize];
    }


    // 配列の中心位置
    private int _centerPoint;

    public void Initialize()
    {
        // 目盛りの単位
        // 1目盛り を A としたとき
        // A = { ±(|n| + 0.1), ±(|n| + 0.2), ±(|n| + 0.3), ±(|n| + 0.4), ±(|n| + 0.5), ±(|n| + 0.6), ±(|n| + 0.7), ±(|n| + 0.8), ±(|n| + 0.9), ±(|n| + 1.0) }
        // の20個の要素を持った集合として表すことが出来る。
        //
        // マイナス値も要素として格納したいので配列長 arraySize は以下の式で表せる
        // arraySize = orderScaleRange * len(A)
        //
        // 目盛りの中心位置は以下の式で表すことができる。
        // ある目盛り index を参照する場合、以下のようになる。
        // index = _centerPoint + (orderValue * len(A) * 0.5)
        // 目盛りの範囲(start, end) を取得する場合は以下のようになる。
        // (start, end) = _centerPoint + ( ± orderValue * len(A) )

        float posX = 0.1f;
        for (int i = 0; i < 100; ++i)
        {
            int vi = 8 * i;
            int ti = 12 * i;
            float scaleHeight = _height;
            
            if ((i + 1) % 10 == 0)
            {
                scaleHeight += 0.2f;
            }
            else if ((i + 1) % 5 == 0)
            {
                scaleHeight += 0.1f;
            }

            SetVertex(vi, ti, posX, scaleHeight);
            SetVertex(vi + 4, ti + 6, -posX, scaleHeight);
            posX += 0.1f;
        }
    }

    private void SetVertex(int vi, int ti, float y, float height)
    {
        Verticies[vi] = new Vector3(0, y, 0);
        Verticies[vi + 1] = new Vector3(0, y + _width, 0);
        Verticies[vi + 2] = new Vector3(height, y, 0);
        Verticies[vi + 3] = new Vector3(height, y + _width, 0);

        Triangles[ti] = vi + 1;
        Triangles[ti + 1] = vi + 0;
        Triangles[ti + 2] = vi + 2;
        Triangles[ti + 3] = vi + 2;
        Triangles[ti + 4] = vi + 3;
        Triangles[ti + 5] = vi + 1;
    }


    public void Dispose()
    {
        Verticies.Dispose();
    }
}
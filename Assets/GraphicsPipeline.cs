using System;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Model mymodel = new Model();
        mymodel.CreateUnityGameObject();

        List<Vector4> verts = Homog(mymodel.vertices);

        Display(verts);

        // First Transformation

        // Rotation by 38 degrees about (21,-3,-3).normalized

        Vector3 axis = (new Vector3(21,-3,-3)).normalized;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.AngleAxis(38, axis),
            Vector3.one);

        Display(rotationMatrix);

        List<Vector4> imageAfterRotation = MatrixTransform(rotationMatrix, verts);

        Display(imageAfterRotation);

        // 2nd Transformation Scale be (1,1,3)
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(
           Vector3.zero, Quaternion.identity,
           new Vector3(1, 1, 3));

        Display(scaleMatrix);

        List<Vector4> imageAftertheScale =
            MatrixTransform(scaleMatrix, imageAfterRotation);

        Display(imageAftertheScale);


        Matrix4x4 traslationMatrix = Matrix4x4.TRS(
            new Vector3(-3, 0, -2),
            Quaternion.identity,
            Vector3.one);

        List<Vector4> imageAfterTranslation = MatrixTransform(traslationMatrix, imageAftertheScale);

        Display(imageAfterTranslation);

    }

    private List<Vector4> Homog(List<Vector3> vertices)
    {
        List<Vector4> result = new List<Vector4>();

        foreach (Vector3 v in vertices)
        { result.Add(new Vector4(v.x, v.y, v.z, 1f)); }

        return result;
    }

    private List<Vector4> MatrixTransform(Matrix4x4 matrix, List<Vector4> verts)
    {
        List<Vector4> hold = new List<Vector4>();
        foreach (Vector4 v in verts)
            hold.Add(matrix * v);

        return hold;
    }

    private void Display(Matrix4x4 matrix)
    {
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i));
    }

    void Display(List<Vector4> verts)
    {
        foreach (Vector4 v in verts) { print(v); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

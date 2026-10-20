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

        Display("Verticies", verts);

        // First Transformation

        // Rotation by 38 degrees about (21,-3,-3).normalized

        Vector3 axis = (new Vector3(21,-3,-3)).normalized;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.AngleAxis(38, axis),
            Vector3.one);

        Display("RotationMatrix", rotationMatrix);

        List<Vector4> imageAfterRotation = MatrixTransform(rotationMatrix, verts);

        Display("ImageAfterRotation", imageAfterRotation);

        // 2nd Transformation Scale be (3,2,3)
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(
           Vector3.zero, Quaternion.identity,
           new Vector3(3, 2, 3));

        Display("ScaleMatrix", scaleMatrix);

        List<Vector4> imageAftertheScale =
            MatrixTransform(scaleMatrix, imageAfterRotation);

        Display("imageAftertheScale", imageAftertheScale);


        Matrix4x4 translationMatrix = Matrix4x4.TRS(
            new Vector3(3, 3, -2),
            Quaternion.identity,
            Vector3.one);

        Display("Translation matrix", translationMatrix);

        List<Vector4> imageAfterTranslation = MatrixTransform(translationMatrix, imageAftertheScale);

        Display("imageAfterTranslation", imageAfterTranslation);

    }

    private void Display(string name, Matrix4x4 matrix)
    {
        print($"===== {name} =====");
        for (int i = 0; i < 4; i++)
            print(matrix.GetRow(i));
    }

    private void Display(string name, List<Vector4> verts)
    {
        print($"===== {name} =====");
        foreach (Vector4 v in verts)
            print(v);
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

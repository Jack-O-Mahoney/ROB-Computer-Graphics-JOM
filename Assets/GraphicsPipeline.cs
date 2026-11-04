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


        //Vertices
        List<Vector4> verts = Homog(mymodel.vertices);
        Display("Verticies", verts);


        // First Transformation
        // Rotation by 38 degrees about (21,-3,-3).normalized
        Vector3 axis = (new Vector3(21,-3,-3)).normalized;


        //RotationMatrix
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(Vector3.zero,Quaternion.AngleAxis(38, axis),Vector3.one);
        Display("RotationMatrix", rotationMatrix);


        //ImageAfterRotation
        List<Vector4> imageAfterRotation = MatrixTransform(rotationMatrix, verts);
        Display("ImageAfterRotation", imageAfterRotation);


        // 2nd Transformation Scale be (3,2,3)
        //ScaleMatrix
        Matrix4x4 scaleMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity,new Vector3(3, 2, 3));
        Display("ScaleMatrix", scaleMatrix);


        //ImageAfterScale
        List<Vector4> imageAftertheScale = MatrixTransform(scaleMatrix, imageAfterRotation);
        Display("imageAftertheScale", imageAftertheScale);


        //TranslationMatrix
        Matrix4x4 translationMatrix = Matrix4x4.TRS(new Vector3(3, 3, -2),Quaternion.identity,Vector3.one);
        Display("Translation matrix", translationMatrix);


        //ImageAfterTranslation
        List<Vector4> imageAfterTranslation = MatrixTransform(translationMatrix, imageAftertheScale);
        Display("imageAfterTranslation", imageAfterTranslation);


        //SingleMatrixofTransformations
        Matrix4x4 singleMoT = translationMatrix * scaleMatrix * rotationMatrix;
        Display("SingleMoT", singleMoT);


        //ImageafterTransformations
        List<Vector4> imageafterSingleMoT = MatrixTransform(singleMoT, verts);
        Display("ImageAfterMoT", imageafterSingleMoT);


        //ViewMatrix
        Vector3 eye = new Vector3(23, 0, 47);
        Vector3 target = new Vector3(-3, 3, 3);
        Vector3 up = new Vector3(-2, -3, 21).normalized;

        Matrix4x4 cameraTransform = Matrix4x4.LookAt(eye, target, up);
        Matrix4x4 viewMatrix = cameraTransform.inverse;
        Display("ViewMatrix", viewMatrix);

        List<Vector4> imageAfterView = MatrixTransform(viewMatrix, imageAfterTranslation);
        Display("ImageAfterView", imageAfterView);


        //ProjectionMatrix
        Matrix4x4 projMatrix = Matrix4x4.Perspective(60f, 16f / 9f, 0.3f, 1000f);
        Display("ProjectionMatrix", projMatrix);


        //EverythingMatrix
        Matrix4x4 EverythingMatrix = projMatrix * viewMatrix * singleMoT;
        Display("SingleMatrixEverything", EverythingMatrix);


        //FinalImage
        List<Vector4> FinalImage = MatrixTransform(projMatrix, imageAfterView);
        Display("FinalImage", FinalImage);


        //FinalImage2
        List<Vector4> FinalImage2 = MatrixTransform(EverythingMatrix, verts);
        Display("FinalImage2", FinalImage2);


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

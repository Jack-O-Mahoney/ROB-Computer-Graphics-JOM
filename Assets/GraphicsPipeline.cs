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


        Vector4[] pts = new Vector4[]
        {
            new Vector4(-4.65f, -7.57f, -53.62f, -52.99f), new Vector4(-3.12f, -20.15f, -48.60f, -47.97f),
            new Vector4(-3.43f, -4.24f, -56.85f, -56.22f), new Vector4(-2.41f, -12.63f, -53.51f, -52.88f),
            new Vector4(-1.39f, -21.02f, -50.16f, -49.53f), new Vector4(-0.88f, -25.22f, -48.49f, -47.86f),
            new Vector4(-1.90f, -16.83f, -51.84f, -51.21f), new Vector4(2.80f, -15.25f, -58.19f, -57.56f),
            new Vector4(3.31f, -19.44f, -56.52f, -55.89f), new Vector4(4.53f, -16.12f, -59.76f, -59.12f),
            new Vector4(4.02f, -11.93f, -61.43f, -60.79f), new Vector4(1.78f, -6.86f, -61.54f, -60.90f),
            new Vector4(-3.03f, -1.82f, -49.74f, -49.11f), new Vector4(-1.50f, -14.40f, -44.73f, -44.10f),
            new Vector4(-1.80f, 1.51f, -52.98f, -52.35f), new Vector4(-1.80f, 1.51f, -52.98f, -52.35f),
            new Vector4(-1.39f, -21.02f, -50.16f, -49.53f), new Vector4(0.75f, -19.47f, -44.61f, -43.99f),
            new Vector4(-0.27f, -11.08f, -47.96f, -47.33f), new Vector4(4.42f, -9.50f, -54.32f, -53.69f),
            new Vector4(4.93f, -13.69f, -52.65f, -52.01f), new Vector4(6.16f, -10.37f, -55.88f, -55.25f),
            new Vector4(5.65f, -6.18f, -57.55f, -56.92f), new Vector4(3.40f, -1.11f, -57.66f, -57.03f)
        };

        foreach (Vector4 p in pts)
        {
            float xp = p.x / -p.z;   // x divided by z (minus sign because the plane is z = -1)
            float yp = p.y / -p.z;   // y divided by z
            print($"{xp:F4}, {yp:F4}");
        }

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



    bool LineClip(ref Vector2 S, ref Vector2 E)
    {
        Outcode StartO = new Outcode(S);
        Outcode EndE = new Outcode(E);
        Outcode Inviewport = new Outcode();

        if ((StartO + EndE) == Inviewport)
            return true;

        if ((StartO * EndE) != Inviewport)
            return false;
        
        float x0 = S.x, y0 = S.y, x1 = E.x, y1 = E.y;
        bool vertical = (x0 == x1);
        bool horizontal = (y0 == y1);
        float m = vertical ? 0 : (y1 - y0) / (x1 - x0);

        Vector2 left = new Vector2(-1, horizontal ? y0 : y0 + m * (-1 - x0));
        Vector2 right = new Vector2(1, horizontal ? y0 : y0 + m * (1 - x0));
        Vector2 top = new Vector2(vertical ? x0 : x0 + (1 / m) * (1 - y0), 1);
        Vector2 bottom = new Vector2(vertical ? x0 : x0 + (1 / m) * (-1 - y0), -1);

        if (StartO.up) S = top;
        else if (StartO.down) S = bottom;
        else if (StartO.left) S = left;
        else if (StartO.right) S = right;

        if (EndE.up) E = top;
        else if (EndE.down) E = bottom;
        else if (EndE.left) E = left;
        else if (EndE.right) E = right;

        return true;
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

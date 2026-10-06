using UnityEngine;

public class GraphicsPipeline : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Model mymodel = new Model();
        mymodel.CreateUnityGameObject();

        /*List<Vector4> verts = Homog(myModel.vertices);

        Display(verts);

        // First Transformation

        // Rotation by 38 degrees about (21,-3,-3).normalized

        Vector3 axis = (new Vector3(21,-3,-3)).normalized;

        Matrix4x4 rotationMatrix = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.AngleAxis(-25, axis),
            Vecotr3.one);

        Display(rotationMatrix);

        List<Vector4> imageAfterRotation = MatrixTransform(rotationMatrix, verts);

        Display(imageAfterRotation);*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

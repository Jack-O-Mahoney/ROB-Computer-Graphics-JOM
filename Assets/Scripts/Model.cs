using UnityEngine;
using System.Collections.Generic;

public class Model
{

    List<Vector3> vertices = new List<Vector3>();
    List<Vector3Int> faces = new List<Vector3Int>();


    public Model()
    {
        addVertices();
        addFaces();
    }

    private void addFaces()
    {
        //Front Faces
        faces.Add(new Vector3Int(0, 2, 5)); //0
        faces.Add(new Vector3Int(0, 5, 1)); //1
        faces.Add(new Vector3Int(6, 3, 8)); //2
        faces.Add(new Vector3Int(3, 9, 8)); //3
        faces.Add(new Vector3Int(7, 11, 9)); //4
        faces.Add(new Vector3Int(11, 10, 9)); //5

        //Back Faces
        faces.Add(new Vector3Int(12, 13, 14)); //0
       faces.Add(new Vector3Int(13, 17, 14)); //1
       // faces.Add(new Vector3Int(8, 5, 3)); //2
        //faces.Add(new Vector3Int(8, 3, 2)); //3
        //faces.Add(new Vector3Int(4, 2, 1)); //4
        //faces.Add(new Vector3Int(4, 1, 0)); //5
    }

    private void addVertices()
    {
        //Front Vertices
        vertices.Add(new Vector3(-1.5f, 2.5f, -1f));  //0
        vertices.Add(new Vector3(1.5f, 2.5f, -1f));   //1
        vertices.Add(new Vector3(-2.5f, 1.5f, -1f));  //2
        vertices.Add(new Vector3(-0.5f, 1.5f, -1f));  //3
        vertices.Add(new Vector3(1.5f, 1.5f, -1f));   //4
        vertices.Add(new Vector3(2.5f, 1.5f, -1f));   //5
        vertices.Add(new Vector3(0.5f, 1.5f, -1f));   //6
        vertices.Add(new Vector3(-0.5f, -1.5f, -1f)); //7
        vertices.Add(new Vector3(0.5f, -1.5f, -1f));  //8
        vertices.Add(new Vector3(-0.5f, -2.5f, -1f)); //9
        vertices.Add(new Vector3(-1.5f, -2.5f, -1f)); //10
        vertices.Add(new Vector3(-2.5f, -1.5f, -1f)); //11

        //Back Vertices
        vertices.Add(new Vector3(-1.5f, 2.5f, 1f)); //12
        vertices.Add(new Vector3(1.5f, 2.5f, 1f)); //13
        vertices.Add(new Vector3(-2.5f, 1.5f, 1f)); //14
        vertices.Add(new Vector3(-0.5f, 1.5f, 1f)); //15
        vertices.Add(new Vector3(1.5f, 1.5f, 1f)); //16
        vertices.Add(new Vector3(2.5f, 1.5f, 1f)); //17
        vertices.Add(new Vector3(0.5f, 1.5f, 1f)); //18
        vertices.Add(new Vector3(-0.5f, -1.5f, 1f)); //19
        vertices.Add(new Vector3(0.5f, -1.5f, 1f)); //20
        vertices.Add(new Vector3(-0.5f, -2.5f, 1f)); //21
        vertices.Add(new Vector3(-1.5f, -2.5f, 1f)); //22
        vertices.Add(new Vector3(-2.5f, -1.5f, 1f)); //23
    }  



        public GameObject CreateUnityGameObject()
        {

       
        Mesh mesh = new Mesh();
        GameObject newGO = new GameObject();
     
        MeshFilter mesh_filter = newGO.AddComponent<MeshFilter>();
        MeshRenderer mesh_renderer = newGO.AddComponent<MeshRenderer>();

        List<Vector3> coords = new List<Vector3>();
        List<int> dummy_indices = new List<int>();
        /*List<Vector2> text_coords = new List<Vector2>();
        List<Vector3> normalz = new List<Vector3>();*/
//fghfg
        for (int i = 0; i < faces.Count; i++)
        {
            //Vector3 normal_for_face = normals[i];

            //normal_for_face = new Vector3(normal_for_face.x, normal_for_face.y, -normal_for_face.z);

            coords.Add(vertices[faces[i].x]); dummy_indices.Add(i * 3); //text_coords.Add(texture_coordinates[texture_index_list[i].x]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].y]); dummy_indices.Add(i * 3 + 2); //text_coords.Add(texture_coordinates[texture_index_list[i].y]); normalz.Add(normal_for_face);

            coords.Add(vertices[faces[i].z]); dummy_indices.Add(i * 3 + 1); //text_coords.Add(texture_coordinates[texture_index_list[i].z]); normalz.Add(normal_for_face);
        }

        mesh.vertices = coords.ToArray();
        mesh.triangles = dummy_indices.ToArray();
        /*mesh.uv = text_coords.ToArray();
        mesh.normals = normalz.ToArray();*/
        mesh_filter.mesh = mesh;

        return newGO;
    }
}

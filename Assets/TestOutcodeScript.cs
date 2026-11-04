using UnityEngine;

public class TestOutcodeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Outcode o = new Outcode(new Vector2 (2,2));

        print(o.ToString());

        Outcode o2 = new Outcode();
        
        print(o2.ToString());

        Outcode o3 = new Outcode(OutCode());

        print(o3.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

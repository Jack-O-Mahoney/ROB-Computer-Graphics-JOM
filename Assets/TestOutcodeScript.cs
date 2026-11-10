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

        Outcode o3 = new Outcode(o);
        print(o3.ToString());

        Outcode o4 = new Outcode(true, false, true, false);
        print(o4.ToString());

        //Logical OR
        print((o + o4).ToString());

        //Logical AND
        print((o * o4).ToString());

        //operator ==
        print(o == o3);
        print(o == o2);

        //operator !=
        print(o != o2);
        print(o != o3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

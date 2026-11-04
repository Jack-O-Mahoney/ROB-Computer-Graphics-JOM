using UnityEngine;

public class Outcode
{
    internal bool up, down, left, right;

    public Outcode(Vector2 v) 
    {
        Debug.Log (v);
        up = v.y > 1;
        down = v.y < -1;
        left = v.x < -1;
        right = v.x > 1;
    }

    public Outcode()
    {
        up = false;
        down = false;
        left = false; 
        right = false;
    }

    public Outcode(Outcode oc)
    {
        up = oc.up;
        down = oc.down;
        left = oc.left;
        right = oc.right;
    }

    public Outcode(bool up , bool down, bool left, bool right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }

    //Logical OR
    public static Outcode operator + (Outcode a,Outcode b)
    {
        return new Outcode(a.up || b.up, b.down || a.down, a.left || b.left, b.right || a.right);
    }

    //Logical AND
    public static Outcode operator * (Outcode a, Outcode b)
    {
        return new Outcode(a.up && b.up, b.down && a.down, a.left && b.left, b.right && a.right);
    }

    public static bool operator == (Outcode a, Outcode b)
    {
        return(a.up == b.up) && (a.down == b.down) && (a.left == b.left) && (a.right == a.left);
    }

    public static bool operator != (Outcode a, Outcode b)
    {
        return !(a == b);
    }

    public string ToString()
    {
        return (up ? "1" : "0") + (down ? "1" : "0") + (left ? "1" : "0") + (right ? "1" : "0");
    }
}

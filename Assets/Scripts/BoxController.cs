using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BoxController : MonoBehaviour
{
    private Rigidbody2D rb;
    private DistanceJoint2D joint;
    public bool isPushing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushing)
        {
            rb.mass = 25;
            joint.enabled = true;
            joint.connectedBody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        }
        else
        {
            rb.mass = 100;
            joint.enabled = false;
        }
    }
}
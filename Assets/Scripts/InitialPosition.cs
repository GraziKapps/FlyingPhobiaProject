using UnityEngine;

public class InitialPosition : MonoBehaviour
{
    private Vector3 position;

    public Vector3 Position
    {
        get
        {
            return position;
        }
    }

    void Start()
    {
        position = gameObject.transform.position;
    }
}

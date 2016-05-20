using UnityEngine;
using System.Collections.Generic;

public class PlayerPosition : MonoBehaviour
{
    public Transform player;
    public List<Vector3> positions = new List<Vector3>();

    private List<KeyCode> keyCodes = new List<KeyCode>()
    {
        KeyCode.Keypad1,
        KeyCode.Keypad2,
        KeyCode.Keypad3,
        KeyCode.Keypad4,
        KeyCode.Keypad5,
        KeyCode.Keypad6,
        KeyCode.Keypad7,
        KeyCode.Keypad8,
        KeyCode.Keypad9
    };

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keyCodes.Count; i++)
        {
            if (Input.GetKey(keyCodes[i]))
            {
                if (i < positions.Count)
                {
                    player.position = positions[i];
                }
            }
        }        
    }
}

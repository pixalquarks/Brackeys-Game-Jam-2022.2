using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    public bool ShowMouse;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = ShowMouse;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

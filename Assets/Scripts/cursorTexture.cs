using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorTexture : MonoBehaviour
{
    [SerializeField] Texture2D atkCursor;
    [SerializeField] Texture2D defaultCursor;

    static public cursorTexture cursor;

    private void Awake()
    {
        cursor = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        setDefault();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDefault()
    {
        Cursor.SetCursor(defaultCursor, Vector3.zero, CursorMode.ForceSoftware);
    }

    public void setAttack()
    {
        Cursor.SetCursor(atkCursor, Vector3.zero, CursorMode.ForceSoftware);
    }
}

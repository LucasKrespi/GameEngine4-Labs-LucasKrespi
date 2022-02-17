using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppEvents
{
    public delegate void OnMouseCursorEnableEvent(bool enable);

    public static event OnMouseCursorEnableEvent MouseCursorEnable;

    public static void InvokOnMouseCursorEnableEvent(bool enable)
    {
        MouseCursorEnable?.Invoke(enable);
    }
}

using UnityEditor;
using UnityEngine;

public class TransformCopyPasteTool0 : EditorWindow
{
    private static Vector3 copiedPosition0;
    private static Quaternion copiedRotation0;

    // 复制位置
    [MenuItem("Tools/Copy Position #p")]  // Ctrl + P 或 Cmd + P
    public static void CopyPosition()
    {
        if (Selection.activeTransform != null)
        {
            copiedPosition0 = Selection.activeTransform.position;
            Debug.Log($"Copied Position: {copiedPosition0}");
        }
        else
        {
            Debug.LogWarning("No object selected to copy position from.");
        }
    }

    // 粘贴位置
    [MenuItem("Tools/Paste Position #&p")]  // Ctrl + Shift + P 或 Cmd + Shift + P
    public static void PastePosition()
    {
        if (Selection.activeTransform != null)
        {
            Selection.activeTransform.position = copiedPosition0;
            Debug.Log($"Pasted Position: {copiedPosition0}");
        }
        else
        {
            Debug.LogWarning("No object selected to paste position to.");
        }
    }

    // 复制旋转
    [MenuItem("Tools/Copy Rotation #r")]  // Ctrl + R 或 Cmd + R
    public static void CopyRotation()
    {
        if (Selection.activeTransform != null)
        {
            copiedRotation0 = Selection.activeTransform.rotation;
            Debug.Log($"Copied Rotation: {copiedRotation0.eulerAngles}");
        }
        else
        {
            Debug.LogWarning("No object selected to copy rotation from.");
        }
    }

    // 粘贴旋转
    [MenuItem("Tools/Paste Rotation #&r")]  // Ctrl + Shift + R 或 Cmd + Shift + R
    public static void PasteRotation()
    {
        if (Selection.activeTransform != null)
        {
            Selection.activeTransform.rotation = copiedRotation0;
            Debug.Log($"Pasted Rotation: {copiedRotation0.eulerAngles}");
        }
        else
        {
            Debug.LogWarning("No object selected to paste rotation to.");
        }
    }
}

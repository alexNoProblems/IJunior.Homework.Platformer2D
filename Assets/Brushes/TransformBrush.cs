
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomGridBrush(true, false, false, "Transform Brush")]
public class TransformBrush : UnityEditor.Tilemaps.GridBrush
{
    public Vector3Int m_Anchor = Vector3Int.zero;
    public Quaternion m_Rotation = Quaternion.identity;
    public Vector3 m_Scale = Vector3.one;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget == null)
            return;

        Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
        if (tilemap == null)
            return;

        base.Paint(grid, brushTarget, position);
        tilemap.SetTransformMatrix(position, Matrix4x4.TRS(Vector3.zero, m_Rotation, m_Scale));
    }

    public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget == null)
            return;

        Tilemap tilemap = brushTarget.GetComponent<Tilemap>();
        if (tilemap == null)
            return;

        base.Erase(grid, brushTarget, position);
        tilemap.SetTransformMatrix(position, Matrix4x4.identity);
    }
}

[CustomEditor(typeof(TransformBrush))]
public class TransformBrushEditor : UnityEditor.Tilemaps.GridBrushEditor
{
    private TransformBrush transformBrush => target as TransformBrush;

    public override void OnPaintInspectorGUI()
    {
        transformBrush.m_Anchor = EditorGUILayout.Vector3IntField("Anchor", transformBrush.m_Anchor);

        Vector3 rotationEuler = transformBrush.m_Rotation.eulerAngles;
        rotationEuler = EditorGUILayout.Vector3Field("Rotation", rotationEuler);
        transformBrush.m_Rotation = Quaternion.Euler(rotationEuler);

        transformBrush.m_Scale = EditorGUILayout.Vector3Field("Scale", transformBrush.m_Scale);

        base.OnPaintInspectorGUI();
    }
}

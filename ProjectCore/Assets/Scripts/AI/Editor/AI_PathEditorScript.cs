using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(AI_Path))]
sealed public class AI_PathEditorScript : Editor
{
    [SerializeField]
    public List<Vector2> positions;
    AI_Path Path;


    void OnEnable()
    {
        Path = target as AI_Path;

        positions = new List<Vector2>();

        if (Path.PositionList != null)
        {
            positions = Path.PositionList;
        }

    }
    private void OnSceneGUI()
    {
        Transform HandleTransform = Path.transform;
        Quaternion HandleRotation = Tools.pivotRotation == PivotRotation.Local ? HandleTransform.rotation : Quaternion.identity;
        Handles.color = Color.cyan;


        if (positions.Count != 0)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i] != positions.Last())
                {
                    Handles.DrawLine(positions[i], positions[i + 1]);
                    Handles.DoPositionHandle(positions[i], HandleRotation);
                }
            }
        }


        EditorGUI.BeginChangeCheck();
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = Handles.DoPositionHandle(positions[i], HandleRotation);
        }
        if (EditorGUI.EndChangeCheck())
        {
            for (int i = 0; i < positions.Count; i++)
            {
                //Path.PositionList[i] = HandleTransform.InverseTransformPoint(Path.PositionList[i]);
            }
        }
        Path.PositionList = positions;
        EditorUtility.SetDirty(Path);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AI_Path Path = target as AI_Path;

        Path.AmountOfPositions = EditorGUILayout.IntSlider("Size", Path.AmountOfPositions, 0, 50);

        if (GUI.changed)
        {
            Path.NewAmountOfPositions = Path.AmountOfPositions - Path.NewAmountOfPositions;

            SetNewListAmount(Path, Path.NewAmountOfPositions);

        }
        EditorUtility.SetDirty(Path);
        Path.NewAmountOfPositions = Path.AmountOfPositions;
        Path.PositionList = positions;
    }

    private void SetNewListAmount(AI_Path Path, int changedAmount)
    {
        Transform HandleTransform = Path.transform;

        if (changedAmount < 0 && positions.Count >= 1)
        {
            for (int i = 0; i > changedAmount; i--)
            {
                positions.Remove(positions.Last());
            }
        }
        else if (changedAmount > 0)
        {
            for (int i = 0; i < changedAmount; i++)
            {
                positions.Add(HandleTransform.TransformPoint(new Vector2(0 + i, 0 + i)));
            }
        }
        EditorUtility.SetDirty(Path);
        Path.PositionList = positions;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;


//���ʹ� ���⿡ ���� �� ��ġ���� ���� ���ؼ� Ŀ���� �ν�����â ����
[CustomEditor(typeof(MoveTile), true)]
public class CustomInspector_MoveTile : Editor
{
    MoveTile moveTile;

    void OnEnable()
    {
        moveTile = target as MoveTile;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        int size = 1;
        switch (moveTile.externDirection)
        {
            case DirectionAxis.EAST :

                foreach (var child in moveTile.GetList())
                {
                    child.transform.localPosition = new Vector2(size, 0);
                    size += 1;
                }
                break;
            case DirectionAxis.WEST:

                foreach (var child in moveTile.GetList())
                {
                    child.transform.localPosition = new Vector2(-size, 0);
                    size += 1;
                }
                break;
            case DirectionAxis.NORTH:

                foreach (var child in moveTile.GetList())
                {
                    child.transform.localPosition = new Vector2(0, size);
                    size += 1;
                }
                break;
            case DirectionAxis.SOUTH:

                foreach (var child in moveTile.GetList())
                {
                    child.transform.localPosition = new Vector2(0, -size);
                    size += 1;
                }
                break;
            default:
                break;
        }

        if (GUILayout.Button("Create"))
        {
            GameObject child = Instantiate(moveTile.body, moveTile.transform.position, Quaternion.identity, moveTile.transform);            
            moveTile.GetList().Add(child);
            moveTile.childCount +=1;

            SceneView.RepaintAll();
            EditorApplication.RepaintHierarchyWindow();
        }

        if (GUILayout.Button("Remove"))
        {
            moveTile.RemoveLastIndex();
            SceneView.RepaintAll();
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}
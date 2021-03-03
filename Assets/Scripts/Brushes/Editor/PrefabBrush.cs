using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Tilemaps;

namespace UnityEditor
{
    [CustomEditor(typeof(PrefabBrush))]
    public class PrefabBrushEditor :  GridBrushEditor
    {

    }



    [CreateAssetMenu(fileName ="Prefab Brush",menuName ="Brushes/Prefab Brush")]
    [CustomGridBrush(false,true,false,"Prefab Brush")]
    public class PrefabBrush : GridBrush
    {
        public GameObject m_prefab;
        public int m_Z;

        public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
        {
            if(brushTarget.layer == 31)
            {
                return;
            }

            GameObject a_instance = (GameObject)PrefabUtility.InstantiatePrefab(m_prefab);
            if(a_instance!=null)
            {
                Undo.RegisterCreatedObjectUndo((Object)a_instance, "Paint Prefab Brush");
                a_instance.transform.SetParent(brushTarget.transform);
                a_instance.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(new Vector3(position.x, position.y, m_Z) + new Vector3(1.5f, 0.5f, 0.5f)));
            }
        }
    }

}


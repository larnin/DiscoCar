using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DiscoCar
{
    public class TargetFlames : TargetBase
    {
        List<MaterialInfo> m_materials = new List<MaterialInfo>();

        public override List<MaterialInfo> GetMaterials()
        {
            return m_materials;
        }

        public override void UpdateMaterials()
        {

        }

        protected override void CleanMaterials()
        {
            CleanMaterials(m_materials);
        }

        public void AddMaterial(UnityEngine.Object obj, Action<Material, Color> setter, string customID = "")
        {
            for (int i = 0; i < m_materials.Count; i++)
                if (m_materials[i].obj == obj && customID == m_materials[i].customID)
                    return;

            var matInfo = new MaterialInfo();
            matInfo.obj = obj;
            matInfo.setter = setter;
            matInfo.customID = customID;
            m_materials.Add(matInfo);

            Console.Out.WriteLine("TargetFlames : Add object " + obj.name);
        }
    }
}

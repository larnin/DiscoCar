using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DiscoCar
{
    public class MaterialList
    {
        static MaterialList m_instance = new MaterialList();
        public static MaterialList instance { get { return m_instance; } }

        List<Material> m_materials = new List<Material>();
        public List<Material> materials { get { return m_materials; } }

        int m_oldMaterialCount = 0;

        public void Add(Material m)
        {
            if (m_materials.Contains(m))
                return;
            m_materials.Add(m);
        }

        public void Update()
        {
            int matCount = m_materials.Count;

            if(matCount > m_oldMaterialCount && Entry.options.enableLogs)
                Console.Out.WriteLine("DiscoCar: Added " + (matCount - m_oldMaterialCount) + " materials. Total: " + matCount);

            if (matCount != m_oldMaterialCount && EffectBase.instance != null)
                EffectBase.instance.UpdateMaterials(m_materials);

            m_oldMaterialCount = matCount;
        }

        public void Clean()
        {
            int matCount = m_materials.Count;
            m_materials.RemoveAll(x => { return x == null; });
            matCount -= m_materials.Count;

            if (matCount != 0 && Entry.options.enableLogs)
                Console.Out.WriteLine("DiscoCar: Removed " + matCount + " materials. Total: " + m_materials.Count);
        }
    }
}

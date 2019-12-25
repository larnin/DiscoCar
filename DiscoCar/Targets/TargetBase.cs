using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DiscoCar
{
    public abstract class TargetBase
    {
        public class MaterialInfo
        {
            public UnityEngine.Object obj;
            public string customID;
            public Action<Material, Color> setter;
        }

        const float m_timerMaxTime = 1.0f;
        float m_updateTimer = 0;

        public static TargetBase MakeTarget(TargetType type)
        {
            switch(type)
            {
                case TargetType.None:
                    return null;
                case TargetType.Flames:
                    throw new NotImplementedException();
                case TargetType.Car:
                    throw new NotImplementedException();
                case TargetType.Everything:
                    throw new NotImplementedException();
            }

            return null;
        }

        public void Update(float deltaTime)
        {
            m_updateTimer -= deltaTime;
            if(m_updateTimer <= 0)
            {
                UpdateMaterials();
                m_updateTimer = m_timerMaxTime;
            }

            CleanMaterials();
        }

        protected void CleanMaterials(List<MaterialInfo> materials)
        {
            int nbRemoved = 0;
            for(int i = materials.Count - 1; i > 0; i--)
            {
                if (materials[i].obj == null)
                {
                    materials.RemoveAt(i);
                    nbRemoved++;
                }
            }

            if (nbRemoved > 0)
                Console.Out.WriteLine("Cleaned " + nbRemoved + " materials.");
        }

        public abstract void UpdateMaterials();
        protected abstract void CleanMaterials();
        public abstract List<MaterialInfo> GetMaterials();
    }
}

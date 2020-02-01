using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

namespace DiscoCar
{
    public abstract class TargetBase
    {
        static TargetBase m_instance = null;
        public static TargetBase instance
        {
            get { return m_instance; }
            private set { m_instance = value; }
        }

        public static void ChangeTarget(TargetType type)
        {
            switch (type)
            {
                case TargetType.None:
                    instance = null;
                    break;
                case TargetType.Flames:
                    instance = new TargetFlames();
                    break;
                case TargetType.Car:
                    instance = new TargetCar();
                    break;
                case TargetType.Everything:
                    instance = new TargetEverything();
                    break;
                default:
                    instance = null;
                    Console.Out.WriteLine("Invalid target type");
                    break;
            }
        }

        public class MaterialInfo
        {
            public Object obj;
            public string customID;
            public Action<Object, Color> setter;
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
                    return new TargetFlames();
                case TargetType.Car:
                    return new TargetCar();
                case TargetType.Everything:
                    return new TargetEverything();
                default:
                    Console.Out.WriteLine("Invalid target type");
                    break;
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

            if(Entry.effect != null)
                UpdateEffect();
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
        public abstract void UpdateEffect();

        public abstract bool ValidateRenderer(Renderer r);
    }
}

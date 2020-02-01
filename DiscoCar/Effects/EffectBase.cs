using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DiscoCar
{
    using MaterialInfo = DiscoCar.TargetBase.MaterialInfo;

    public abstract class EffectBase
    {
        static EffectBase m_instance;
        public static EffectBase instance
        {
            get { return m_instance; }
            private set { m_instance = value; }
        }

        public static void UpdateEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Random:
                    m_instance = new EffectRandom();
                    break;
                case EffectType.Gradient:
                    m_instance = new EffectRainbow();
                    break;
                case EffectType.RandomGradient:
                    m_instance = null;
                    break;
                default:
                    Console.Out.WriteLine("DiscoCar: Invalid EffectType");
                    m_instance = null;
                    break;
            }

        }

        public static EffectBase MakeEffect(EffectType type)
        {
            switch (type)
            {
                case EffectType.Random:
                    return new EffectRandom();
                case EffectType.Gradient:
                    return new EffectRainbow();
                case EffectType.RandomGradient:
                    break;
                default:
                    break;
            }

            return null;
        }

        public abstract void UpdateMaterials(List<Material> materials);
        public abstract void Update();

        public abstract void Update(List<MaterialInfo> materials);

        public abstract void Clean();

        protected List<int> m_properties = new List<int>();

        public EffectBase()
        {
            string[] targets = new string[] { "_Color", "_Color2", "_EmitColor", "_Emission", "_ReflectColor", "_SpecColor" };

            foreach(var t in targets)
            {
                m_properties.Add(Shader.PropertyToID(t));
            }
        }

    }
}

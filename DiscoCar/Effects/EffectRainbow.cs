using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = System.Object;

namespace DiscoCar
{
    public class EffectRainbow : EffectBase
    {
        class Target
        {
            public bool updated = false;
            public float value = 0;
            public Object obj = null;
            public string customID = "";
        }

        List<Target> m_targets = new List<Target>();

        public override void Clean()
        {
            m_targets.RemoveAll(x => { return !x.updated || x.obj == null; });
        }

        public override void Update(List<TargetBase.MaterialInfo> materials)
        {
            foreach(var m in materials)
            {
                var target = m_targets.Find(x => { return x.obj == m.obj && x.customID == m.customID; });
                if(target == null)
                {
                    m_targets.Add(new Target());
                    target = m_targets[m_targets.Count - 1];
                    target.value = Random.value;
                    target.obj = m.obj;
                    target.customID = m.customID;
                }

                target.updated = true;
                target.value += Time.deltaTime * Entry.options.effectSpeed;
                if (target.value > 1)
                    target.value = 0;

                m.setter(m.obj, Color.HSVToRGB(target.value, 1, 1));
            }
        }

        public override void UpdateMaterials(List<Material> materials)
        {

        }

        public override void Update()
        {

        }
    }
}

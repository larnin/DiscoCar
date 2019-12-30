using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DiscoCar
{
    class EffectRandom : EffectBase
    {
        int m_currentChangeNb = 0;
        int m_lastChangeNb = 0;

        public override void Clean()
        {
            m_lastChangeNb = m_currentChangeNb;
            m_currentChangeNb = 0;
        }

        public override void Update(List<TargetBase.MaterialInfo> materials)
        {
            foreach(var m in materials)
            {
                if (m == null)
                    return;

                bool changeThis = Entry.options.effectSpeed >= 1;
                if(!changeThis)
                {
                    float value = Mathf.Lerp(Time.deltaTime, 1, Entry.options.effectSpeed);
                    changeThis = Random.value <= value;
                }
                if (!changeThis)
                    return;

                var color = new Color(Random.value, Random.value, Random.value);
                m.setter(m.obj, color);
                m_currentChangeNb++;
            }
        }
    }
}

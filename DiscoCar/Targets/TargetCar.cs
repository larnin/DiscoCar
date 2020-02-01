using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using Object = System.Object;

namespace DiscoCar
{
    using MaterialInfo = DiscoCar.TargetBase.MaterialInfo;

    class TargetCar : TargetBase
    {
        List<MaterialInfo> m_materials = new List<MaterialInfo>();

        public override void UpdateMaterials()
        {
            var cars = CarLogic.FindObjectsOfType<CarLogic>();

            foreach (var c in cars)
            {
                var renderers = c.GetComponentsInChildren<Renderer>();
                foreach(var r in renderers)
                {
                    foreach(var m in r.materials)
                    {
                        Action<string> foo = (string id) =>
                        {
                            if (m.HasProperty(id))
                                AddMaterial(m, (Object obj, Color color) =>
                                {
                                    var mat = obj as Material;
                                    if (mat == null)
                                        return;

                                    mat.SetColor(id, color);
                                }, id);
                        };

                        foo("_Color");
                        foo("_Color2");
                        foo("_EmitColor");
                        foo("_Emission");
                        foo("_ReflectColor");
                        foo("_SpecColor");
                    }
                }
            }
        }

        protected override void CleanMaterials()
        {
            CleanMaterials(m_materials);
        }

        public override void UpdateEffect()
        {
            if (Entry.effect == null)
                return;

            Entry.effect.Update(m_materials);
        }

        void AddMaterial(Object obj, Action<Object, Color> setter, string customID = "")
        {
            foreach(var m in m_materials)
            {
                if (m.obj == obj && m.customID == customID)
                    return;
            }

            var mat = new MaterialInfo();
            mat.obj = obj;
            mat.setter = setter;
            mat.customID = customID;

            m_materials.Add(mat);
        }

        public override bool ValidateRenderer(Renderer r)
        {
            return false;
        }
    }
}

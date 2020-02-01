using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Object = System.Object;

namespace DiscoCar
{
    using MaterialInfo = DiscoCar.TargetBase.MaterialInfo;

    public class TargetFlames : TargetBase
    {

        public override void UpdateMaterials()
        {

        }

        protected override void CleanMaterials()
        {
            
        }

        public void AddMaterial(Object obj, Action<Object, Color> setter, string customID = "")
        {
            if (Entry.effect == null)
                return;

            var mat = new MaterialInfo();
            mat.obj = obj;
            mat.setter = setter;
            mat.customID = customID;

            List<MaterialInfo> mats = new List<MaterialInfo>();
            mats.Add(mat);

            Entry.effect.Update(mats);
        }

        public override void UpdateEffect()
        {

        }

        public override bool ValidateRenderer(Renderer r)
        {
            return false;
        }
    }
}

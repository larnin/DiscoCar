using Spectrum.API.Configuration;
using Spectrum.API.GUI.Controls;
using Spectrum.API.GUI.Data;
using Spectrum.API.Interfaces.Systems;
using System.Collections.Generic;

namespace DiscoCar
{
    public enum TargetType
    {
        None,
        Flames,
        Car,
        Everything
    }

    public enum EffectType
    {
        Random,
        Gradient,
        RandomGradient
    }

    public class PluginOptions
    {
        Settings m_config;

        public PluginOptions(IManager manager)
        {
            m_config = new Settings("Config");
            bool mustSave = false;
            var defaultConfig = new Dictionary<string, object>()
            {
                {"DiscoFlames", false },
                {"DiscoOverheat", false },
                {"EffectType", (int)EffectType.Gradient },
                {"TargetType", (int)TargetType.Flames },
                {"EffectSpeed", 1 }
            };
            foreach (var item in defaultConfig)
            {
                if (!m_config.ContainsKey(item.Key))
                {
                    mustSave = true;
                    m_config[item.Key] = item.Value;
                }
            }
            if (mustSave)
                m_config.Save();

            CreateMenus(manager);
        }

        void CreateMenus(IManager manager)
        {
            manager.Menus.AddMenu(MenuDisplayMode.Both, new MenuTree("discocar.main", "DISCO CAR [FF0000](SEIZURE WARNING !)[-]")
        {
            new ListBox<TargetType>(MenuDisplayMode.Both, "discocar.main.target", "DISCO TARGET")
            .WithEntries(new Dictionary<string, TargetType>
                {
                    {"None", TargetType.None },
                    {"Flames", TargetType.Flames },
                    {"Car", TargetType.Car },
                    {"EVERYTHING !!!", TargetType.Everything }
                })
            .WithGetter(()=> {
                return targetType;
            })
            .WithSetter((value) =>{
                targetType = value;
            }),

            new ListBox<EffectType>(MenuDisplayMode.Both, "discocar.main.effect", "DISCO EFFECT")
            .WithEntries(new Dictionary<string, EffectType>
                {
                    {"Random", EffectType.Random },
                    {"Gradient", EffectType.Gradient },
                    {"Randomized gradient", EffectType.RandomGradient },
                })
            .WithGetter(()=> {
                return effectType;
            })
            .WithSetter((value) =>{
                effectType = value;
            }),

            new FloatSlider(MenuDisplayMode.Both, "discocar.main.speed", "DISCO SPEED")
            .WithGetter(()=>{
                return effectSpeed;
            })
            .WithSetter((value) => {
                effectSpeed = value;
            })
        });
        }

        public bool discoFlames
        {
            get
            {
                return m_config.GetItem<bool>("DiscoFlames");
            }
            set
            {
                m_config["DiscoFlames"] = value;
                m_config.Save();
            }
        }

        public bool discoOverheat
        {
            get
            {
                return m_config.GetItem<bool>("DiscoOverheat");
            }
            set
            {
                m_config["DiscoOverheat"] = value;
                m_config.Save();
            }
        }

        public EffectType effectType
        {
            get
            {
                return (EffectType)m_config.GetItem<int>("EffectType");
            }
            set
            {
                m_config["EffectType"] = (int)value;
                m_config.Save();

                Entry.UpdateCurrentEffect();
            }
        }

        public TargetType targetType
        {
            get
            {
                return (TargetType)m_config.GetItem<int>("TargetType");
            }
            set
            {
                m_config["TargetType"] = (int)value;
                m_config.Save();

                Entry.UpdateCurrentTarget();
            }
        }

        public float effectSpeed
        {
            get
            {
                return m_config.GetItem<float>("EffectSpeed");
            }
            set
            {
                m_config["EffectSpeed"] = value;
                m_config.Save();
            }
        }
    }
}

using Spectrum.API.Configuration;
using Spectrum.API.GUI.Controls;
using Spectrum.API.GUI.Data;
using Spectrum.API.Interfaces.Systems;
using System.Collections.Generic;

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
                {"DiscoOverheat", false }
            };
        foreach (var item in defaultConfig)
        {
            if (!m_config.ContainsKey(item.Key))
            {
                mustSave = true;
                m_config[item.Key] = item.Value;
            }
        }
        if(mustSave)
            m_config.Save();

        CreateMenus(manager);
    }

    void CreateMenus(IManager manager)
    {

        manager.Menus.AddMenu(MenuDisplayMode.Both, new MenuTree("discocar.main", "DISCO CAR [FF0000](SEIZURE WARNING !)[-]")
        {
            new CheckBox(MenuDisplayMode.Both, "discocar.main.flames", "DISCO FLAMES")
            .WithGetter(() => {
                return discoFlames;
            })
            .WithSetter((value) => {
                discoFlames = value;
            }),

            new CheckBox(MenuDisplayMode.Both, "discocar.main.overheat", "DISCO OVERHEAT")
            .WithGetter(() => {
                return discoOverheat;
            })
            .WithSetter((value) => {
                discoOverheat = value;
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
}

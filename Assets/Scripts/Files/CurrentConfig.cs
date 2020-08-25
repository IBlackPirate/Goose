using System.Collections.Generic;

// Класс хранящий все текущие настройки
static class CurrentConfig
{
    public static bool IsInitialize { get; private set; }

    public static int Level { get; set; }
    public static bool IsFullScreen { get; set; }
    public static int Resolution { get; set; }
    public static int Quality { get; set; }
    public static float MenuVolume { get; set; }
    public static float GameVolume { get; set; }

    public static string FileName = "CurrentConfig";

    public static void Awake()
    {

        Dictionary<string, string> config = Ini.Load(FileName);


        if (config == null)
        {
            IsInitialize = false;
            return;
        }
        IsInitialize = true;


        Level = int.Parse(config["Level"]);
        IsFullScreen = bool.Parse(config["IsFullScreen"]);
        Resolution = int.Parse(config["Resolution"]);
        Quality = int.Parse(config["Quality"]);
        MenuVolume = float.Parse(config["MenuVolume"]);
        GameVolume = float.Parse(config["GameVolume"]);
    }

    public static void OnQuit()
    {
        var config = new Dictionary<string, string>();
        config["Level"] = Level.ToString();
        config["IsFullScreen"] = IsFullScreen.ToString();
        config["MenuVolume"] = MenuVolume.ToString();
        config["GameVolume"] = GameVolume.ToString();
        config["Quality"] = Quality.ToString();
        config["Resolution"] = Resolution.ToString();
        Ini.Save(FileName, config);
    }
}


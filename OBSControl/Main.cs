using MelonLoader;
using OBSControl;
using Main = OBSControl.Main;

[assembly: MelonInfo(typeof(Main), Insanity.Name, Insanity.Version, Insanity.Author, Insanity.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]

namespace OBSControl;

public static class Insanity
{
    public const string Name = "OBSControl";
    public const string Author = "Animal";
    public const string Version = "1.0.0";
    public const string DownloadLink = "https://github.com/Aniiiiiimal/OBSControl";
}

public class Main : MelonMod
{
    
}
using System;
using MelonLoader;
using OBSControl;
using Main = OBSControl.Main;

[assembly: MelonInfo(typeof(Main), Insanity.Name, Insanity.Version, Insanity.Author, Insanity.DownloadLink)]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonColor(ConsoleColor.DarkCyan)]
[assembly: MelonOptionalDependencies("UIExpansionKit")] // not required, but qol to change settings

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
    private static MelonPreferences_Category category;
    private static int _scenesLoaded;

    public override void OnApplicationStart()
    {
        Logger.Logs = LoggerInstance;
        InitPrefs();
        OBSMenu.LoadIcons();
        /*MelonHandler.LoadFromFile("UserLibs/obs-websocket-dotnet.dll"); need to build for net48 cause melon moment*/

        HarmonyInstance.Patch(
            typeof(RoomManager).GetMethod(nameof(RoomManager
                .Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0)), null,
            typeof(World).GetMethod(nameof(World.WorldJoin)).ToNewHarmonyMethod());
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (_scenesLoaded > 2) return;
        _scenesLoaded++;
        if (_scenesLoaded == 2)
        {
            MelonCoroutines.Start(OBSMenu.OnUiReady());
        }
    }

    public static MelonPreferences_Entry<string> HostIP, HostPassword, WorldLeaveScene, WorldJoinScene;
    public static MelonPreferences_Entry<bool> AutoConnect, SwitchWorldScene;

    private static void InitPrefs()
    {
        category = MelonPreferences.CreateCategory(Insanity.Name, "OBS Control");
        HostIP = category.CreateEntry("HostIP", "ws://localhost:4444", "Host Address");
        HostPassword = category.CreateEntry("HostPassword", "", "Host Password");
        AutoConnect = category.CreateEntry("AutoConnect", false, "Connect when VRChat starts");
        SwitchWorldScene = category.CreateEntry("SwitchWorldScene", false, "Switch scenes when switching worlds");
        WorldJoinScene = category.CreateEntry("WorldJoinScene", "", "Destination scene (leave blank for prior scene)");
        WorldLeaveScene =
            category.CreateEntry("WorldLeaveScene", "", "Switching worlds scene");
    }
}
using ABI.CCK.Components;

namespace OBSControl;

public class World
{
    private static string priorScene;
    public static void WorldJoin(CVRWorld __instance)
    {
        Logger.Msg("Joining world");
        if (OBSMenu.obs is not {IsConnected: true} || !Main.SwitchWorldScene.Value) return;
        // 2am brain aka this might cause issue if there isnt a prior scene idk - still true
        OBSMenu.obs.SetCurrentScene(string.IsNullOrWhiteSpace(Main.WorldJoinScene.Value) ? priorScene : Main.WorldJoinScene.Value);
    }

    public static void WorldLeave()
    {
        Logger.Msg("Leaving world!");
        if (OBSMenu.obs is not {IsConnected: true} || !Main.SwitchWorldScene.Value) return;
        if (string.IsNullOrWhiteSpace(Main.WorldLeaveScene.Value)) return;
        priorScene = OBSMenu.obs.GetCurrentScene().Name;
        OBSMenu.obs.SetCurrentScene(Main.WorldLeaveScene.Value);
    }
}
using VRC.Core;

namespace OBSControl;

public class World
{
    private static string priorScene;
    public static void WorldJoin(ApiWorld __0, ApiWorldInstance __1)
    {
        if (OBSMenu.obs is not {IsConnected: true} || !Main.SwitchWorldScene.Value) return;
        // 2am brain aka this might cause issue if there isnt a prior scene idk
        OBSMenu.obs.SetCurrentScene(string  .IsNullOrWhiteSpace(Main.WorldJoinScene.Value) ? priorScene : Main.WorldJoinScene.Value);
    }

    public static void WorldLeave()
    {
        if (OBSMenu.obs is not {IsConnected: true} || !Main.SwitchWorldScene.Value) return;
        if (string.IsNullOrWhiteSpace(Main.WorldLeaveScene.Value)) return;
        priorScene = OBSMenu.obs.GetCurrentScene().Name;
        OBSMenu.obs.SetCurrentScene(Main.WorldLeaveScene.Value);
    }
}
# OBSControl

A mod for controlling OBS Studio from inside VRChat.\
somewhat ported from a [beat saber mod](https://github.com/Zingabopp/OBSControl) version

## typical disclaimer yada yada
Mods are against VRChat TOS. However, this mod does nothing to make you different from other users\
**If you are afraid of a very unlikely ban risk then I recommend you do not proceed.**

-----

Still here? alright heres what you probably already have but are gonna need

# Prerequisites:
### External:

- [OBS Studio](https://obsproject.com) (27.2.4 or later, preferably)

- [obs-websocket](https://github.com/obsproject/obs-websocket/releases/tag/5.0.0) - OBS plugin to connect to obs through websockets
  - Make sure to also install the `4.9.1-compat` package
  - **This does not work with Streamlabs OBS.**

### VRChat:
- VRChat build 1207+
- MelonLoader 0.5.4 or later
  - Need to install MelonLoader? Click [here](https://melonwiki.xyz/)!

- ReMod.Core - You likely already have it, but if not, get it [from here](https://github.com/RequiDev/ReMod.Core/releases)
  - It does not auto-update, use [ReMod.Core.Updater](https://github.com/PennyBunny/ReMod.Core.Updater) (plugins folder) for that
- This mod, obviously - download from [releases page](https://github.com/Aniiiiiimal/OBSControl/releases)

### Usage:
- Open OBS
- Go to Tools > Websocket server settings (4.x compat) > Set a password you remember
- Set the address and password inside OBSControl Mod Settings
- Go to OBSControl Tab in Quick Menu and connect
- You're good

(Streaming/Recording labels don't update correctly yet, fyi)

### Common Use Case(s):
- Showing another scene while switching worlds (see video for demo)

https://user-images.githubusercontent.com/24845294/180597534-16fd6657-b436-41bb-9f49-258ac41b6b14.mp4

- Starting/Stopping recording without using a desktop overlay/taking off headset
- ~~Switching scenes on the fly~~ (not yet implemented)

More features will come at some point —
I'm open to feature requests for now, create an issue and i'll get to it the next time i do modding stuff

---

# Credits:
[ReMod.Core](https://github.com/RequiDev/ReMod.Core) - UI library

[OBSControl for Beat Saber](https://github.com/Zingabopp/OBSControl) - Original mod idea

i didnt miss having to type these goofy ah readmes lol
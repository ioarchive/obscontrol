# OBSControl

A mod for controlling OBS Studio from inside ChilloutVR.\
somewhat ported from a [beat saber mod](https://github.com/Zingabopp/OBSControl) version

## typical disclaimer
This mod is not affiliated with Alpha Blend Interactive. The mod comes with no warranty. Use at your own risk, as I am not responsible for any misuse.


# Prerequisites:
### External:

- [OBS Studio](https://obsproject.com) (27.2.4 or later, preferably)

- [obs-websocket](https://github.com/obsproject/obs-websocket/releases/tag/5.0.0) - OBS plugin to connect to obs through websockets
  - Make sure to also install the `4.9.1-compat` package
  - **This does not work with Streamlabs OBS.**

### CVR:
- ChilloutVR, of course. This mod was developed on version 2022r165
- MelonLoader 0.5.4 or later
  - Need to install MelonLoader? Click [here](https://melonwiki.xyz/)!

- This mod, obviously - download from [releases page](https://github.com/Aniiiiiimal/OBSControl/releases)

### Usage:
- Open OBS
- Go to Tools > Websocket server settings (4.x compat) > Set a password you remember
- Set the address and password inside OBSControl Mod Settings
- (temporary) Enable connect on startup in Mod Settings
- You're good

### Common Use Case(s):
- Showing another scene while switching worlds (see video for demo)

honestly you probably dont even need this because chillout's world transition is way better and cooler than VRC's

cvr video coming when i add ui

https://user-images.githubusercontent.com/24845294/180597534-16fd6657-b436-41bb-9f49-258ac41b6b14.mp4

- Starting/Stopping recording without using a desktop overlay/taking off headset
- ~~Switching scenes on the fly~~ (not yet implemented)

More features will come at some point —
I'm open to feature requests for now, create an issue and i'll get to it the next time i do modding stuff

---

# Credits:
[OBSControl for Beat Saber](https://github.com/Zingabopp/OBSControl) - Original mod idea

i didnt miss having to type these goofy ah readmes lol
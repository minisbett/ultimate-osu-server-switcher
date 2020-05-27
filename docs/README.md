<a align="right" href="https://discord.gg/DuByk3D">
  <img src="https://discordapp.com/api/guilds/715149105525030932/widget.png"></img>
</a>

<img align="right" src="https://minisbett.github.io/ultimate-osu-server-switcher/images/logo.png">

# Ultimate Osu Server Switcher

The Ultimate Osu Server Switcher is a server switcher that allows you to switch between osu!bancho and a variety of private servers.
You can [add your server](https://minisbett.github.io/ultimate-osu-server-switcher/#how-to-add-a-server) and  [get your server featured](https://minisbett.github.io/ultimate-osu-server-switcher/#get-your-server-featured) in a partnership.

# How it works

The Program fetches all its informations from the data/data.json file, which means its fully expandable even without a software update.

Switching to a different server or even bancho itself is fairly simple. You just choose the server you want to connect to from a list and
click the «Connect» button.

# How to add a server

If you wish to add a server, please follow these steps:

1. Ask the server owner (if not you) if it's okay to add their server to our switcher.
2. Collect the following data:
- The server name
- The server's website url
- The server's ip-address
- The certificate
- The certificate thumbprint (can be grabbed with our thumbprint reader, see releases)
- The server icon (It should be sized 48x48 due to performance)
3. Create an issue with the label "server request" or create a pull request.
we would prefer if you would create an issue rather than creating a pull request.
This will take the work off your hands and help us make our structure work better.

# Get your server featured

To expand the switcher and to gain popularity, partnerships are welcome!

If you and your private server want to help us please DM us on Discord!

> minisbett#0001
>
> Julaaaan#7635

We will offer a feature icon ⭐ before your server name in the switcher and your server will be put to the top of the list.
In return for that we want you to offer our switcher as a second option on your switcher's download site.

More details can be discussed on Discord.

# In-deep explination

The server you are currently playing on is estimated by the ip that is deposited in the hosts file.
Switching a server includes deinstalling all certificates, modfying the hosts file, followed by installing the corresponding certificate
(if you chose to play on a third-party server).

The certificates will be installed on the local machine, rather than just the local user to avoid a warning prompt the user
has to agree with. This improves the switching agility to garantue the best user experience.

# Current servers

> Currently the following servers are listed:
>
> - [osu!bancho](https://osu.ppy.sh/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/bancho.png?raw=true">
> - [Ripple](https://ripple.moe/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ripple.png?raw=true">
> - [EZPPFarm](https://ez-pp.farm/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ezppfarm.png?raw=true"><sup>⭐</sup>
> - [osu!Ainu](https://ainu.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ainu.png?raw=true">
> - [Kurikku](https://kurikku.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/kurikku.png?raw=true">
> - [The Realm](https://theosurealm.tk/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/theosurealm.png?raw=true">
> - [Akatsuki](https://akatsuki.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/akatsuki.png?raw=true">
> - [osu.ppy.sb](https://osu.ppy.sb/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/osu.ppy.sb.png?raw=true">

# GUI

We have a light theme and a dark theme. You can toggle between them in the bottom right corner of the about tab.

![Light theme](https://i.imgur.com/iqvpEpHl.png)
![Dark theme](https://i.imgur.com/ItqHz4p.png)

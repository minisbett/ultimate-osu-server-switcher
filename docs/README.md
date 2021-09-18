# DEPRECATED

Peppy announced a new feature on twitter which makes hosts and certificate handling obsolete. By that, I strongly recommend using the devserver feature instead of a switcher. For that I made a so-called Osu Server Launcher that let's you connect to servers with ease! You can find the repository on my GitHub profile.

Visit the Osu Private Server Community Discord for more informations.

<a href="https://discord.gg/9KfUdHpUA8"><img src="https://discordapp.com/api/guilds/715149105525030932/widget.png"></a>

<a href="https://minisbett.github.io/ultimate-osu-server-switcher"><img width=128 height=128 align="right" src="https://minisbett.github.io/ultimate-osu-server-switcher/images/icon.png"></a>

# Ultimate Osu Server Switcher

The Ultimate Osu Server Switcher is a server switcher that allows you to switch between osu!bancho and a variety of private servers.
You can [add your server](https://minisbett.github.io/ultimate-osu-server-switcher#how-to-add-a-server) and  [get your server featured](https://minisbett.github.io/ultimate-osu-server-switcher/#get-your-server-featured-partnership) in a partnership.

# How it works

The Program fetches all its informations from the data/data.json file, which means its fully expandable even without a software update.

Switching to a different server or even bancho itself is fairly simple. You just choose the server you want to connect to from a list and
click the «Connect» button.

# How to add a server

> **As of the 05/29/2020 every server's owner now needs to be member of our discord server as it is a requirement for our management!**
> **Thank you for your understanding.**

If you wish to add a server, please follow these steps:

1. Ask the server owner (if not you) if it's okay to add their server to our switcher.
2. Collect the following data:
- The server name
- The server's website url
- The server's ip-address
- The certificate
- The discord tag of the server owner (We need to verify that he is on our discord server)
- The certificate thumbprint (can be grabbed with our thumbprint reader, see releases) <br>
  NOTE: The certificate should be base64 encoded (.cer file extension) <br>
  If your certificate has the file extension .crt, use [this tutorial](https://support.comodo.com/index.php?/Knowledgebase/Article/View/361/17/how-do-i-convert-crt-file-into-the-microsoft-cer-format) to convert the .crt certificate to a .cer certificate.
- The server icon (It should be sized 48x48 due to performance)
3. Create an issue **with the label "server request"**. Please do not create a pull request as it would break our workflow. Thanks :)

# Get your server featured (Partnership)

**Currently we do not feature servers as we need to keep this state something special.**

To expand the switcher and to gain popularity, partnerships are welcome!

If you and your private server want to help us please DM us on Discord!

> minisbett#8873
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
has to agree with. This improves the switching agility to garantue the best user experience. Also why not, the hosts changes apply to
the whole computer too.

# Current servers
>
> Currently the following servers are listed:
>
> - [osu!bancho](https://osu.ppy.sh/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/bancho.png?raw=true">
> - [Ripple](https://ripple.moe/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ripple.png?raw=true">
> - [EZPPFarm](https://ez-pp.farm/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ezppfarm.png?raw=true"><sup>⭐</sup>
> - [osu!Ainu](https://ainu.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/ainu.png?raw=true">
> - [Kurikku](https://kurikku.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/kurikku.png?raw=true"><sup>⭐</sup>
> - [Akatsuki](https://akatsuki.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/akatsuki.png?raw=true">
> - [osu!Gatari](https://gatari.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/gatari.png?raw=true"><sup>⭐</sup>
> - [RealistikOsu!](https://ussr.pl/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/realistikosu.png?raw=true">
> - [Enjuu](https://enjuu.click/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/enjuu.png?raw=true">
> - [Kawata](https://kawata.pw/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/kawata.png?raw=true">
> - [Astellia](https://astellia.club/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/astellia.png?raw=true">
> - [Horizon](https://lemres.de/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/horizon.png?raw=true">
> - [Debian](https://debian.moe/)<img width="16" height="16" src="https://github.com/MinisBett/ultimate-osu-server-switcher/blob/master/data/icons/debian.png?raw=true">
> - [Nekosu](https://nekos.cc/)<img width="16" height="16" src="https://nekos.cc/static/logo.png">

# GUI

We have a light theme and a dark theme. You can toggle between them in the bottom right corner of the about tab.

![Light theme](https://minisbett.github.io/ultimate-osu-server-switcher/images/light.png)
![Dark theme](https://minisbett.github.io/ultimate-osu-server-switcher/images/dark.png)

# LabAutoEvent
该项目为LabAPI版本的AutoEvent(小游戏插件)的中文重置版本，因为没有人继续更新AutoEvent而创建, 该项目后续会内置一些有趣而又有创新的小游戏，而各位开发者也可以使用它去开发属于自己的小游戏，而开发很简单，可以继续往下看"Devlop"标签内容
# 安装
本插件完全基于Lab-API写出，无需安装多余的框架，然后确保你已经安装了以下插件和依赖:\
[0Harmony.dll](https://github.com/pardeike/Harmony)\
[ProjectMER.dll](https://github.com/Smer4k/ProjectMER)\
[SecretLabNAudio](https://github.com/Axwabo/SecretLabNAudio)\
然后基于你服务器的系统安装LabAutoEvent.dll:
 - Windows:\
   路径"%appdata%\SCP Secret Laboratory\LabAPI\plugins\global"或指定端口"%appdata%\SCP Secret Laboratory\LabAPI\plugins\{端口号}"\
   将releases里最新版本的LabAutoEvent.dll安装到指定文件夹就行了
 - Liunx(拿Ubuntu举例):\
   路径"home/{运行SL服务器端的用户}/.config/SCP Secret Laboratory/LabAPI/plugins/global"或指定端口"home/{运行SL服务器端的用户}/.config/SCP Secret Laboratory/LabAPI/plugins/{端口号}"\
   将releases里最新版本的LabAutoEvent.dll安装到指定文件夹就行了\
最后启动/重启服务器即可
# 命令:
所有命令都需要命令权限"autoevent.*", 可以查询其他教程去给指定用户组命令权限，这里不多讲\
所有的命令如下:
----------
|命令 |作用 |参数 |\
|au list |查看属于已注册的小游戏 |无 |\
|au start |开启小游戏 |小游戏ID(使用au list可看) |\
|au stop |停止小游戏运行 |无 |\
----------
# 安装音频/地图原理图:
如果你准备了特殊的音频/地图原理图想替换就看下面:
  - 音频(可以看SecretLabNAudio给出的音频要求，需要.ogg后缀名):
    将LabAPI/AutoEvent/Music文件夹里的指定音频替换成你的就行了
  - 地图(确保地图有一个以上的对象名称叫做"SpawnPos"):
    和PMER安装原理图一样到替换掉相同名称的json文件就行了
# Devlop:
现在或许会有开发者想要开发属于自己的小游戏，好好开放大脑就看下面:\
首先需要引入LabAutoEvent.dll和其依赖\
然后新建一个小游戏类，并承接"MiniGameBase"类，并重写基本属性(所有属性注解在下面)\
然后你就可以重写OnDisabled和OnEnabled方法来写小游戏在关闭和启动会执行的代码，也很简单，就想成再写一个插件，然后注册注销事件，最后需要执行游戏结束代码时需要引用父类的OnEnd()方法(也可以重写该方法，原方法只是给玩家变成DD到教程塔表示游戏结束)\
并可以在MiniGameManager类中使用一些扶助方法:\
AddTypeToList , 将一个Type转化为MiniGameBase并保存到列表\
FindMiniGamesFromPath, 从指定路径寻找MiniGameBase并返回List\
StartGame, 开始指定游戏\
StopOrEndGame, 暂停游戏\
AnyGameIsRunning, 是否有游戏正在运行\
关于MiniGameBase类里的属性与方法:\
GameId, 该游戏的唯一ID\
GameName, 该游戏的名称\
GameAuthor, 游戏作者\
GameDescription, 游戏介绍\
GameVersion, 游戏版本\
WillLoad, 游戏是否会在框架加载时自动注册\
MapInfo, 游戏的地图信息,请自行创建MapInfo实例\
MusicInfo, 游戏的音乐信息， 请自行创建MusicInfo实例\
IsRunning, 该游戏是否在运行，默认为false\
PlayerSpawnPositions, 玩家的刷新坐标对象列表\
OnDisabled, 游戏关闭时运行的方法\
OnEnabled, 游戏加载时运行的方法\
OnEnd, 游戏结束时运行的代码\

# Poi

从网络地图（目前仅支持百度地图）的API获取POI

## 程序启动

　　直接在 Visual Studio 中打开 Poi.sln 即可开发和修改代码。

　　编译生成程序后，32 位平台将 Dll\x86 文件夹中所有的 dll 文件复制到 exe 生成的文件夹下；64 位平台将 Dll\x64 文件夹中所有的 dll 文件复制到 exe 生成的文件夹下，已存在的 dll 文件跳过或覆盖都可以。再将 Db\poi.db 复制到 exe 生成的文件夹下，程序即可正常启动。

　　此时程序中并未包含有效的百度地图 API 的校验码。需要在[百度地图开放平台中](http://lbsyun.baidu.com/)注册账户，创建服务端应用，确保能够支持 Place API v2，并选择“sn校验方式”。创建应用后，获取相应的 AK 和 SK。通过程序的“配置密钥”按钮添加 AK 和 SK，或直接用 sqlite 客户端打开 poi.db，在 dictionary 表中添加正确的 AK 和 SK。添加成功后，即可正确运行程序。

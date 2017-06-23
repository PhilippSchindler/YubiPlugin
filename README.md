# YubiPlugin (beta)
YubiPlugin - The easy 2nd factor solution for Keepass


## Intro
YubiPlugin is a plugin for the well-known password manager Keepass 2.x available at http://keepass.info/.
It provides an easy solution to secure Keypass databases using YubiKeys from https://www.yubico.com/store/. 

Correctly only Windows platforms are supported. Linux support is planned, feel free to contribute.

In order to create or unlock a database just proceed as usual, with your YubiKey inserted.

When creating a new Keypass database (or changing the master password of an existing one) YubiPlugin gives you the option to show a restore phrase. This phrase can be used to unlock your database in case you loose your YubiKey and must be stored at a safe place.



## Installation of the YubiPlugin
Follow the steps below to install YubiPlugin and the required libaries from Yubico.

### YubiPlugin
1. Download the ``YubiPlugin.plgx`` file from TODO. 
2. Copy the ``YubiPlugin.plgx`` file into the ``Plugin`` directory of your Keepass installation. Additional help for installing Keepass plugins is available at http://keepass.info/help/v2/plugins.html.

### Yubico Libraries
1. Download the Yubico Personalization Tools (command line interface!) from https://www.yubico.com/support/knowledge-base/categories/articles/yubikey-personalization-tools/ for your platform.
2. Extract the archive.
3. Copy the contents of the ``bin`` folder of the archive to the folder ``Plugins\yubico`` of your Keypass installation. 

Your Keypass directory structure should now look like:

```
Keepass-2.x
├── Plugins
│   ├── yubico
│   │   ├── libjson-0.dll
│   │   ├── libjson-c-2.dll
│   │   ├── libykpers-1-1.dll
│   │   ├── libyubikey-0.dll
│   │   ├── modhex.exe
│   │   ├── ykchalresp.exe
│   │   ├── ykgenerate.exe
│   │   ├── ykinfo.exe
│   │   ├── ykparse.exe
│   │   └── ykpersonalize.exe
│   └── YubiPlugin.plgx
├── Keepass.chm
├── Keepass.exe
├── Keepass.exe.config
│   ...
└── ShInstUtil.exe
```



## Configuration of your YubiKey
In order for YubiPlugin to work correctly with your YubiKey you need to configure your YubiKey first.
You have to configure **slot 2** of your YubiKey in **HMAC-SHA1 challenge-response mode**. This can be accomplished by using Yubico's YubiKey Personalization Tool. You can still use slot 1 for your typical 2-factor authentification with Yubico's OTP.

1. Download an and install Yubico's YubiKey Personalization Tool from https://www.yubico.com/support/knowledge-base/categories/articles/yubikey-personalization-tools/
2. Insert your YubiKey
3. Start Yubico's YubiKey Personalization Tool
4. Click on the tab ``Challenge-Response`` and click the button ``HMAC-SHA1``

![](https://image.ibb.co/nHxPxa/yubikey_config.png)

5. Select ``Configuration Slot 2``.
6. Click the button ``Generate`` to generate a new random key, which will be stored on your YubiKey.
7. [Optional: Select Require user input (button press), 
   this increases security as unlocking your database then requires a physical press of your YubiKey.]

![](https://image.ibb.co/fRP4xa/yubikey_config_02.png)

8. Verify that you followed the steps correctly and click the button ``Write configuration``. Do not safe the configuration logfile.



## Using the plugin
Make sure you have completed the steps "Installation of the YubiPlugin" as well as "Configuration of your YubiKey".
You are now able to use your YubiKey to secure your Keypass databases. Notice that a single Yubikey might be used to secure multiple databases. 

### Creating a new Keypass database
Start Keepass and insert your YubiKey. Proceed as usual to create a new Keypass database. 

![](https://image.ibb.co/gMFoOF/keypass_create_db.png)

When you click the ``OK`` button, YubiPlugin start's its work. [If you have configured the "Require user input (button press)" option of your YubiKey, it starts blicking. Now is the time to press your Yubikey.] YubiPlugin shows a small window with a option to show your recovery phrase. This phrase can be used to unlock your database in case you loose your password and/or YubiKey.

![](https://image.ibb.co/e2aBca/recovery_phrase.png)

### Changing the master password of an existing Keypass database
1. Unlock your database without a YubiKey plugged in. Select ``File`` - ``Change Master Key``. 
2. Insert your YubiKey.
3. Follow the same steps as when creating a new database.

### Common pitfalls
* Make sure to only insert a single Yubikey at a time when using YubiPlugin.
* When unlocking a normal Keypass database - i.e. one that is not secured with a YubiKey as second factor - make sure your YubiKey is NOT inserted.
* Forgetting your password or losing your YubiKey: Unless you have saved your recovery phrase, which is displayed after creating a Keypass database, there is no way to unlock your database.



## Support
Currently the plugin is only tested for Windows. 
In principle the plugin should also work on Linux via Mono. Some code changes are 

If their are any issues, please do not hesitate to contact me or to submit a pull-request. Any contributions are welcome.
The software is developed during my free time. 
If you find it useful you might consider making a donation via Bitcoin or Ethereum.

Bitcoin Donation Address: ``1BnXBLETsZ21m9ad8R1VtuigW1GB6QByXF``  
Ethereum Donation Address: ``0x50D6C8EF6b43FF98e4408B4d633741cf448b52b8``




## Disclaimer
The author of this plugin distributes the software under GPLv3 as is and without any warranties. 
The author is not affilated with Yubico in any way. 

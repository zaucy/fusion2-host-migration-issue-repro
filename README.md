# Fusion Host Migration Repro

* Import fusion 2 <https://doc.photonengine.com/fusion/current/getting-started/sdk-download>
* Configure the fusion app ID on your account

## Repro Steps

NOTE: tested with fusion 2.0.1

1. Open project in either 2 editors or create a build and run it twice
2. Press 'Start Game' in on game instance and wait a little bit
3. Press 'Start Game' in the other game instance
4. Press 'Disconnect' on the first game instance (thats the host)
5. Notice the camera has been despawned and there are no scene game object logs

created for <https://discord.com/channels/87465474098483200/1210410613940166676>

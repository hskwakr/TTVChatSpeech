You need to get the Nuget packages to run.
```
System.IO
Newtonsoft.Json
TwitchLib
Google.Cloud.TextToSpeech.V1
```

Also, you need to change the code in `TwitchBot.cs` to set your twitch token.
Here is about token generator: https://twitchtokengenerator.com/
(If you can set environment variable "TwitchToken", you can use below code.)
```
new ConnectionCredentials("your Twitch ID", Environment.GetEnvironmentVariable("TwitchToken"))
```

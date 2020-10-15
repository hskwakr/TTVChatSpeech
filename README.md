## Dependencies
- Newtonsoft.Json
- Google.Cloud.TextToSpeech.V1
- TwitchLib
- System.IO

Also, you need to change the code in `TwitchBot.cs` to set your twitch token.  
Here is about token generator: https://twitchtokengenerator.com/  
(If you can set environment variable "TwitchToken", you can use below code.)
```
new ConnectionCredentials("your Twitch ID", Environment.GetEnvironmentVariable("TwitchToken"))
```

## Credits and Other Project Licenses
- Newtonsoft.Json  
https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md
- TwitchLib  
https://github.com/TwitchLib/TwitchLib/blob/master/LICENSE
- Google.Cloud.TextToSpeech.V1
https://github.com/googleapis/google-cloud-dotnet/blob/master/LICENSE

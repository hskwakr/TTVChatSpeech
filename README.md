## Dependencies
- Google.Cloud.TextToSpeech.V1
- TwitchLib

Also, you need to change the code in `TwitchChat/TwitchChatInitializer.cs` to set your twitch token.  
Here is about token generator: https://twitchtokengenerator.com/  
(If you can set environment variable "TwitchToken", you can use below code.)
```
new ConnectionCredentials("your Twitch ID", Environment.GetEnvironmentVariable("TwitchToken"))
```

## Credits and Other Project Licenses
- TwitchLib  
https://github.com/TwitchLib/TwitchLib/blob/master/LICENSE

- Google.Cloud.TextToSpeech.V1  
https://github.com/googleapis/google-cloud-dotnet/blob/master/LICENSE

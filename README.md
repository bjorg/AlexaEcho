# Alexa Skill Echo
Simple Alexa Skill that responds to any recognized intent.

### Pre-requisites
The following tools and accounts are required to complete these instructions.

* [Install .NET Core 1.x](https://www.microsoft.com/net/core)
* [Install AWS CLI](https://aws.amazon.com/cli/)
* [Sign-up for an AWS account](https://aws.amazon.com/)

### Create `lambdasharp` Profile
The project uses by default the `lambdasharp` profile. Follow these steps to setup a new profile if need be.

1. Create a `lambdasharp` profile: `aws configure --profile lambdasharp`
2. Configure the profile with the AWS credentials you want to use
3. **NOTE**: AWS Lambda function for Alexa Skills must be hosted in `us-east-1`

### Create IAM role for the AlexaEcxho Lambda function
The AlexaEcxho Lambda function requires an IAM role to access CloudWatchLogs. You can create the `LambdaSharp-AlexaEcho` role via the [AWS Console](https://console.aws.amazon.com/iam/home) or use the executing [AWS CLI](https://aws.amazon.com/cli/) commands.

```shell
aws iam create-role --profile lambdasharp --role-name LambdaSharp-AlexaEcho --assume-role-policy-document file://assets/lambda-role-policy.json
aws iam attach-role-policy --profile lambdasharp --role-name LambdaSharp-AlexaEcho --policy-arn arn:aws:iam::aws:policy/CloudWatchLogsFullAccess
```

### Deploy
1. Restore project dependencies: `dotnet restore`
2. Build project: `dotnet build`
4. Deploy AlexaEcho lambda function: `dotnet lambda deploy-function`

### Copyright & License
* Copyright (c) 2017 Steve Bjorg
* MIT License


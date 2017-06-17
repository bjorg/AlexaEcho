using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AlexaEcho {

    public class Function {

        //--- Methods ---
        public SkillResponse FunctionHandler(SkillRequest skill, ILambdaContext context) {
            switch(skill.Request) {

            // skill was activated without an intent
            case LaunchRequest launch:
                LambdaLogger.Log($"*** INFO: launch\n");
                return Respond("Skill launched.", "Say an intent or say \"quit\" to leave.");

            // skill was activated with an intent
            case IntentRequest intent:
                LambdaLogger.Log($"*** INFO: intent ({intent.Intent.Name})\n");
                return Respond($"Received intent \"{intent.Intent.Name}\".", "Say a command or quit to leave.");

            // skill session ended (no response expected)
            case SessionEndedRequest ended:
                LambdaLogger.Log("*** INFO: session ended\n");
                return ResponseBuilder.Empty();

            // exception reported on previous response (no response expected)
            case SystemExceptionRequest error:
                LambdaLogger.Log("*** INFO: system exception\n");
                LambdaLogger.Log($"*** EXCEPTION: skill request: {JsonConvert.SerializeObject(skill)}\n");
                return ResponseBuilder.Empty();

            // unknown skill received (no response expected)
            default:
                LambdaLogger.Log($"*** WARNING: unrecognized skill request: {JsonConvert.SerializeObject(skill)}\n");
                return ResponseBuilder.Empty();
            }

            // helper methods
            SkillResponse Respond(string text, string reprompt = null) {
                if(reprompt == null) {
                    return ResponseBuilder.Tell(new PlainTextOutputSpeech {
                        Text = text
                    });
                }
                return ResponseBuilder.Ask(new PlainTextOutputSpeech {
                    Text = text
                }, new Reprompt {
                    OutputSpeech = new PlainTextOutputSpeech {
                        Text = reprompt
                    }
                });
            }
        }
    }
}

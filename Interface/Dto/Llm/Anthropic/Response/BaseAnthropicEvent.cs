using System.Text.Json.Serialization;
using Interface.Dto.Llm.Anthropic.Response.Stream;

namespace Interface.Dto.Llm.Anthropic.Response;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(AnthropicMessageStart), "message_start")]
[JsonDerivedType(typeof(AnthropicPing), "ping")]
[JsonDerivedType(typeof(AnthropicContentBlockStart), "content_block_start")]
[JsonDerivedType(typeof(AnthropicContentBlockDelta), "content_block_delta")]
[JsonDerivedType(typeof(AnthropicContentBlockStop), "content_block_stop")]
[JsonDerivedType(typeof(AnthropicMessageDelta), "message_delta")]
[JsonDerivedType(typeof(AnthropicMessageStop), "message_stop")]
[JsonDerivedType(typeof(AnthropicError), "error")]
public abstract class BaseAnthropicEvent
{
    [JsonPropertyName("type")]
    public abstract string Type { get; }
}

/*
START

event: message_start
data: {
    "type":"message_start",
    "message":
    {
        "id":"msg_01GfwdfTpv1Lsmqhpxan8XX8",
        "type":"message",
        "role":"assistant",
        "model":"claude-3-5-sonnet-20241022",
        "content":[],
        "stop_reason":null,
        "stop_sequence":null,
        "usage":
        {
            "input_tokens":26,
            "output_tokens":1
        }
    }
}

event: content_block_start
data: {"type":"content_block_start","index":0,"content_block":{"type":"text","text":""}               }

event: ping
data: {"type": "ping"}

event: content_block_delta
data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"I"}           }

event: content_block_delta
data: {"type":"content_block_delta","index":0,"delta":{"type":"text_delta","text":"'m operational and functioning normally."}     }

event: content_block_stop
data: {"type":"content_block_stop","index":0 }

event: message_delta
data: {"type":"message_delta","delta":{"stop_reason":"end_turn","stop_sequence":null},"usage":{"output_tokens":10}    }

event: message_stop
data: {"type":"message_stop"           }

END
*/
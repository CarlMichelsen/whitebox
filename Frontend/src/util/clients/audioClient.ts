import { BaseClient } from "../baseClient";
import {hostUrl} from "../endpoints";
import {SpeechBlob} from "../../model/audio/speechBlob";
import {SpeechToTextResponse} from "../../model/audio/speechToTextResponse.ts";

export class AudioClient extends BaseClient
{
    public readonly speechToTextPath: string = "api/v1/Speech";

    protected host: string = hostUrl();

    public async toText(blob: SpeechBlob) {
        return await this.request<SpeechToTextResponse>("POST", this.speechToTextPath, blob);
    }
}
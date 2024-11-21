import { v4 as uuidV4 } from 'uuid';
import {AudioClient} from "../clients/audioClient";
import {SpeechBlob} from "../../model/audio/speechBlob.ts";
import {SpeechToTextResponse} from "../../model/audio/speechToTextResponse.ts";
import {ServiceResponse} from "../../model/serviceResponse.ts";

export class SpeechToTextHandler {
    private readonly audioChunks: BlobPart[] = [];
    private readonly audioClient = new AudioClient();

    private identifier: string = uuidV4();
    private intervalIndex: number|null = null;
    private mediaRecorder: MediaRecorder|null = null;
    
    public async start() {
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        this.mediaRecorder = new MediaRecorder(stream);

        this.mediaRecorder.ondataavailable = (event: BlobEvent) => {
            this.audioChunks.push(event.data);
        };

        this.mediaRecorder.onstop = () => {
            const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });
            this.audioChunks.length = 0;

            audioBlob.arrayBuffer().then(buffer => {
                if (this.identifier === null) {
                    return;
                }

                const payload: SpeechBlob = {
                    identifier: this.identifier,
                    data: new Uint8Array(buffer),
                }
                console.log(payload)

                this.audioClient.toText(payload)
                    .then(this.handleTextResponse)
                    .catch(console.error);
            });
        };

        this.intervalIndex = setInterval(() => {
            this.mediaRecorder?.stop()
            this.mediaRecorder?.start()
        }, 1000);
    }
    
    public stop() {
        this.mediaRecorder?.stop();
        this.mediaRecorder = null;

        this.intervalIndex && clearInterval(this.intervalIndex);
        this.intervalIndex = null;

        this.audioChunks.length = 0;
        this.identifier = uuidV4();
    }
    
    private handleTextResponse(res: ServiceResponse<SpeechToTextResponse>) {
        console.log("text-response", res)
    }
}
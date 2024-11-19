import {HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import {hostUrl} from "../endpoints.ts";
import { v4 as uuidV4 } from 'uuid';
import {SpeechBlob} from "../../model/audio/speechBlob.ts";

const getConnection = () => {
    return new HubConnectionBuilder()
        .withUrl(hostUrl()+"/api/v1/speechToTextHub")
        .build();
}

export class AudioCapture
{
    identifier: string|null = null;
    intervalIndex: number|null = null;
    connection: HubConnection|null = null;
    audioChunks: BlobPart[] = [];
    mediaRecorder: MediaRecorder|null = null;

    public async start() {
        if (this.identifier !== null) {
            return;
        }
        
        this.identifier = uuidV4();
        this.connection = getConnection();
        await this.connection.start();
        
        const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        this.mediaRecorder = new MediaRecorder(stream);
        
        this.mediaRecorder.ondataavailable = (event: BlobEvent) => {
            this.audioChunks.push(event.data);
        };

        this.mediaRecorder.onstop = () => {
            const audioBlob = new Blob(this.audioChunks, { type: 'audio/wav' });
            this.audioChunks = [];
            
            if (this.connection?.state === "Connected") {
                audioBlob.arrayBuffer().then(buffer => {
                    if (this.identifier === null) {
                        return;
                    }
                    
                    const payload: SpeechBlob = {
                        identifier: this.identifier,
                        data: new Uint8Array(buffer),
                    }
                    
                    this.connection?.invoke<SpeechBlob>("SendBlob", payload).catch(console.error);
                });
            }
        };

        this.intervalIndex = setInterval(() => {
            this.mediaRecorder?.stop()
            this.mediaRecorder?.start()
        }, 100);

        this.mediaRecorder.start();
    }

    public async stop() {
        this.mediaRecorder?.stop();
        this.mediaRecorder = null;

        this.connection?.stop().then(() => this.connection = null);

        this.intervalIndex && clearInterval(this.intervalIndex);
        this.intervalIndex = null;
        
        this.audioChunks = [];
        this.identifier = null
    }
}


export const audioCapture = async (): Promise<void> => {
    try {
        /*const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
        const audioContext = new window.AudioContext();
        const audioSource = audioContext.createMediaStreamSource(stream);

        await audioContext.audioWorklet.addModule("web-workers/audio-processor.js");

        const audioProcessor = new AudioWorkletNode(audioContext, 'audio-processor');
        audioSource.connect(audioProcessor);
        audioProcessor.connect(audioContext.destination);*/
        
        

        //worker.onerror = console.log
        //worker.onmessage = console.log
        /*setInterval(() => {
            //worker.postMessage(1)
        }, 500)
        
        worker.onmessage = (event: MessageEvent) => {
            console.log("audio-processor", event.data);
        }
        
        worker.onerror = (event: ErrorEvent) => {console.error(event);}
        worker.onmessageerror = (event: MessageEvent) => {console.error(event);}

        console.log(worker);*/
    } catch (err) {
        console.error('Error accessing the microphone', err);
        throw err;
    }
}
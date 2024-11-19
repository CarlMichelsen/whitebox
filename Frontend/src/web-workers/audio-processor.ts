self.onmessage = (event: MessageEvent<Blob>) => {
    self.postMessage(event.data);
};
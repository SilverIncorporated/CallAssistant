window.WSInitialize = (dotNetHelper, url) => {

    console.log("Connecting socket...");
    window.dotNetHelper = dotNetHelper;

    let ws = new WebSocket(`${url}/listener/callassistant`);

    ws.onopen = function (event) {
        if (typeof window.dotNetHelper !== 'undefined') {
            console.log("Websocket connected. Sending to Blazor app...");
            console.log(event);

        } else {
            console.log("Websocket connected but Blazor app not initialized.");
            console.log(event);
        }
    };

    ws.onmessage = function (event) {
        console.log('Call event recieved')
        var eventData = JSON.parse(event.data)

        //add new call event types here!
        if (eventData.event === 'transcription') {
            dotNetHelper.invokeMethodAsync('onMessageReceive', eval('(' + event.data + ')'));
        }
    }
}


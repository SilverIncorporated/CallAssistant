console.log("Setting up socket...");

let dotnethelper = null;
let ws = null;
let url = null;

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function Init(){
    ws = new WebSocket(`${url}/listener/callassistant`);
    ws.onopen = function (event) {
        console.log("Websocket connected. Sending to Blazor app...");
        var event = {
            event: "init"
        }
        ws.send(JSON.stringify(event));

        dotnethelper.invokeMethodAsync('onSocketConnect');
    };
    ws.onmessage = function (event) {

        var eventData = JSON.parse(event.data)

        if (eventData.event === 'transcription') {
            console.log('Transcription received...');
            dotnethelper.invokeMethodAsync('onTranscription', JSON.stringify(eventData.content))
        } else if (eventData.event === 'callFunction') {
            console.log('Function call received...');
            dotnethelper.invokeMethodAsync('onFunctionCall', JSON.stringify(eventData.content))
        }
    }

    sleep(3000);
}

window.WSInitialize = (DotnetHelper, _url) => {
    dotnethelper = DotnetHelper;
    url = _url
    Init();
}

window.WSClose = () => {
    if (ws) {
        ws.close();
        ws = null;
    }
}

window.RegisterFunction = (process) => {
    console.log(`Registering function from process: ${process.name}`);
    var args = JSON.parse(process.arguments.input)
    var props = {}
    for (a in args) {
        var arg = args[a]
        props[arg.name] = {
            type: arg.type.split(',')[0].split('.').slice(-1)[0].toLowerCase()
        }
    }
    var reqd = args.filter(function (a) {
        return a.required
    })
    var required = reqd.map((a) => a.name)
    var func = {
        name: process.nameClean,
        description: process.description,
        parameters: {
            type: "object",
            properties: props
        },
        required: required
    };
    var event = {
        event: "registerFunction",
        content:func
    }

    if (ws.readyState == 2 || ws.readyState == 3 || !ws) {
        Init()
    }
    ws.send(JSON.stringify(event))
    
    
}

window.SendMessage = (message) => {
    console.log(`Sending message: '${message}'`);

    if (ws) {
        var event = {
            event: "echo",
            content: message
        }
        if (ws.readyState == 2 || ws.readyState == 3 || !ws) {
            Init()
        }
        ws.send(JSON.stringify(event))
    } else {
        console.log("Websocket not connected...")
    }
}

window.FunctionComplete = (call) => {
    console.log(`Function complete: '${call}'`);

    console.log(call);

    var result = JSON.parse(call.result);

    if (Object.keys(result) === 0) {
        call.result = '{"result":"success"}'
    }

    var event = {
        event: 'functionReturn',
        content: {
            name: call.name,
            result: call.result
        }
    }
    if (ws.readyState == 2 || ws.readyState == 3 || !ws) {
        Init()
    }
    ws.send(JSON.stringify(event));
}

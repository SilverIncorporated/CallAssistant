console.log("Setting up socket...");

let dotnethelper = null;
let ws = null;

window.WSInitialize = (dotnetHelper, url) => {
    dotnethelper = dotnetHelper;
    ws = new WebSocket(`${url}/listener/callassistant`);

    ws.onopen = function (event) {
        console.log("Websocket connected. Sending to Blazor app...");
        console.log(event);
        var event = {
            event:"clearFunctions"
        }
        ws.send(JSON.stringify(event));
    };
    ws.onmessage = function (msg) {
        console.log(msg)

        if (msg.event === 'transcription') {

        } else if (msg.event === 'callFunction') {

        }
    }
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
        name: process.name,
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
    if (ws) {
        ws.send(JSON.stringify(event))
    }
    
}


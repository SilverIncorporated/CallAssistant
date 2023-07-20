window.LogFunction = (process) => {

    console.log(`Logging process: ${process.name}`);
    console.log(process.arguments)
    console.log(process.inArguments)

    var args = JSON.parse(process.arguments.input)

    var inArgs = JSON.parse(process.inArguments)

    console.log(args)
    console.log(inArgs)

    var props = {}

    for (a in args) {
        var arg = args[a]
        if (arg.hasDefault) {
        }
        props[args[a].name] = {
            type: args[a].type.split(',')[0].split('.').slice(-1)[0].toLowerCase()
            
        }
    }

    console.log(props)

    var required = args.filter(function (a) {
        return a.required
    })

    console.log(required.map((a) => a.name))

    //let inArgs = JSON.parse(process.InArguments);
    //let inParams = {}

    //log(`In Arguments: ${inArgs}`)

    //var func = {
    //    name: process.Name,
    //    description: process.Description,
    //    parameters: {
    //        type: "object",
    //        properties: {

    //        }

    //    }
    //    required: [

    //    ]
    //};
}


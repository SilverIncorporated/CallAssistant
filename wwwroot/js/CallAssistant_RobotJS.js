const robot = UiPathRobot.init();

window.GetProcesses = (dotNetHelper) => {

    console.log('Getting processes...')

    robot.getProcesses().then(result => {
        console.log('Processes returned...');
        window.processes = result;
        dotNetHelper.invokeMethodAsync('onGetProcessReturn',result)
    });

}

window.RunProcess = (dotNetHelper, process, funcCall) => {

    console.log('Starting process ' + process.name + '...');

    let procs = window.processes;

    if (procs.some(p => p.id === process.id)) {

        let proc = procs.filter(p => p.id === process.id)[0];

        let args = JSON.parse(funcCall.arguments);


        if (Object.keys(args).length > 0) {
            proc.start(args).then(result => {
                console.log(process.name + ' finished successfully with args.');
                dotNetHelper.invokeMethodAsync('onProcessFinish', JSON.stringify(process), JSON.stringify(result), JSON.stringify(funcCall));
            });
        } else {
            proc.start().then(result => {
                console.log(process.name + ' finished successfully without args.');
                dotNetHelper.invokeMethodAsync('onProcessFinish', JSON.stringify(process), JSON.stringify(result), JSON.stringify(funcCall));
            });
        }

        

    }
}

window.InstallProcess = (dotNetHelper, process) => {
    console.log('Installing process ' + process.name + '...');

    let procs = window.processes;

    if (procs.some(p => p.id === process.id)) {
        let proc = procs.filter(p => p.id === process.id)[0];

        robot.installProcess(proc.id).then(result => {
            
            dotNetHelper.invokeMethodAsync('onProcessInstall', JSON.stringify(proc), JSON.stringify(result.inputArgumentsSchema))
        });
    }
}

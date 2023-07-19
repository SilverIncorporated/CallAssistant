const robot = UiPathRobot.init();

window.GetProcesses = (dotNetHelper) => {

    console.log('Getting processes...')

    robot.getProcesses().then(result => {
        console.log('Processes returned...');
        window.processes = result;
        dotNetHelper.invokeMethodAsync('onGetProcessReturn', processes);
    });

}

window.RunProcess = (dotNetHelper, process) => {

    console.log('Starting process ' + process.name + '...');

    let procs = window.processes;

    if (procs.some(p => p.id === process.id)) {

        let proc = procs.filter(p => p.id === process.id)[0];

        proc.start().then(result => {
            console.log(process.name + ' finished successfully.');
            dotNetHelper.invokeMethodAsync('onProcessFinish', JSON.stringify(result));
        });

    }
}


window.GetFuelPrice = (dotNetHelper) => {

    console.log('Starting process GetCurrentFuelPrice...');

    let procs = window.processes;

    if (procs.some(p => p.name === 'GetCurrentFuelPrice')) {

        let proc = procs.filter(p => p.name === 'GetCurrentFuelPrice')[0];

        proc.start().then(result => {
            console.log(proc.name + ' finished successfully.');
            dotNetHelper.invokeMethodAsync('onProcessFinish_GetFuelPrice', JSON.stringify(result));
        });

    }
    
}
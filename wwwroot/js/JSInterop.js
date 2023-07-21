window.LogMessage = (message) => {
    console.log(message);
}

function scrollToEnd(textarea) {
    textarea.scrollTop = textarea.scrollHeight;
}

function scrollToTop(textarea) {
    textarea.scrollTop = 0;
}
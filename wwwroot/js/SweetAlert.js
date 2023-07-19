window.SweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire('Success', message, 'success')
    }
    if (type === "error") {
        Swal.fire('Error', message, 'error')
    }
}
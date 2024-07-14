document.addEventListener('DOMContentLoaded', function () {
    if (message) {
        if (messageType === 'success') {
            toastr.success(message);
        } else if (messageType === 'error') {
            toastr.error(message);
        }
    }
});
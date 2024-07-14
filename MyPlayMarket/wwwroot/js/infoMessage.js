document.addEventListener('DOMContentLoaded', function () {
    if (message) {
        toastr.option.showDuration = 5000;
        if (messageType === 'success') {
            toastr.success(message);
        } else if (messageType === 'error') {
            toastr.error(message);
        }
    }
});
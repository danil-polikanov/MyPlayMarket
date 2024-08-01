document.addEventListener('DOMContentLoaded', function () {
    if (typeof message !== 'undefined' && message) {
        toastr.options.timeOut = 7000;
        if (messageType === 'success') {
            toastr.success(message);
        } else {
            toastr.error(message);
        }
    }
});
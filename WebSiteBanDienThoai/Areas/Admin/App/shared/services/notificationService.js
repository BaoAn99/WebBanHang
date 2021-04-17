(function (app) {
    app.factory('notificationService', notificationService);

    function notificationService() {
        function displaySuccess(message) {
            $.niftyNoty({
                type: 'success',
                message: message,
                container: 'floating',
                timer: 3000
            });
        }

        function displayError(error) {
            if (Array.isArray(error)) {
                error.each(function (err) {
                    $.niftyNoty({
                        type: 'danger',
                        message: err,
                        container: 'floating',
                        timer: 3000
                    });
                });
            }
            else {
                $.niftyNoty({
                    type: 'danger',
                    message: error,
                    container: 'floating',
                    timer: 3000
                });
            }
        }

        function displayWarning(message) {
            $.niftyNoty({
                type: 'warning',
                message: message,
                container: 'floating',
                timer: 3000
            });
        }
        function displayInfo(message) {
            $.niftyNoty({
                type: 'info',
                message: message,
                container: 'floating',
                timer: 3000
            });
        }

        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        }
    }
})(angular.module('AdmShopMobile.common'));
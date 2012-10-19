define(['knockout'], function (ko) {
    return function applicationViewModel() {
        var self = this;
        self.healthData = ko.observableArray([]);
        
        self.name = ko.observable('Health check');

        self.initialize = function() {
            self.updateData();
        };
        
        self.updateData = function() {
            $.getJSON("api/HealthCheck", '', function(data) {
                var healthData = ko.utils.arrayMap(data, function(healthData) {
                    return {
                        state: healthData.Status,
                        stateMessage: healthData.StateMessage,
                        systemName: healthData.SystemName,
                        exceptionMessage: healthData.ExceptionMessage,
                        methodName: healthData.MethodName,
                        image: healthData.StateImage,
                        isOk: function () { return healthData.Status === 'OK'; },
                        isError: function () { return healthData.Status === 'Critical'; },
                        isWarning: function () { return healthData.Status === 'TryingToRestablish'; }
                    };
                });
                self.healthData(healthData);
            });
        };
        
        self.initialize();
    };
});

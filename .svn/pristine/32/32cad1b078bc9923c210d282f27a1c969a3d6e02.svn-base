define(['jquery',
        'knockout',
        'health/viewModel',
        'bootstrap',
        'domReady!'], function ($, ko, applicationViewModel) {
            var run = function () {
                ko.applyBindings(new applicationViewModel());
                console.log("Application started...");
            };

            return {
                run: run
            }
        });
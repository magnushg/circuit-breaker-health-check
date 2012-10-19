require.config({
    paths: {
        "jquery": "jquery-1.8.2.min",
        "bootstrap": "bootstrap.min",
        "knockout": "knockout-2.1.0"
    },
    shim: {
        "bootstrap": ["jquery"]
    }
});

require(['health/app'], function (app) {
    app.run();
});
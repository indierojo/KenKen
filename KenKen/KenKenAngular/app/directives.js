'use strict';

var kenkenDirectives = angular.module('kenkenDirectives', []);

kenkenDirectives.directive('ngFocusWhenSelected', function ($timeout) {
    return function (scope, element) {
        // 1 ms timeout required or the text within is not selected
        $timeout(function () {
            element[0].select();
        }, 1);
    };
});
kenkenDirectives.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
});